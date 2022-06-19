using System.Reflection;
using ARS.Common.Attributes;
using ARS.Common.Bases;
using ARS.Common.Helpers;
using ARS.Common.Interfaces.DataTypes;

// ReSharper disable SuspiciousTypeConversion.Global

namespace ARS.Web.Components;

public partial class AutoTable<T> where T : BaseData
{
    private readonly string _fitStyle = "width: 1%;white-space: nowrap;";

    private int _lastRefreshTick = 0;

    private bool _searchable;

    private string? _searchText;
    private bool _selectable;
    public BaseCollection<T> Collection { get; set; }
    private List<T>? SourceItems { get; set; }
    private List<T>? Items { get; set; }

    [Parameter]
    public string? Title { get; set; }

    public string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            FilterAll();
        }
    }

    private bool _selectedAll;

    private List<string>? Headers { get; set; }

    //Pager
    public bool PagerEnabled => AllItemsCount > ItemsPerPage + 1;
    public int CurrentPage { get; set; } = 1;
    public int PageCount => (int) Math.Ceiling(AllItemsCount / (double) ItemsPerPage);
    public int FirstItem => CurrentPage * ItemsPerPage - ItemsPerPage;

    public int LastItem
    {
        get
        {
            var last = CurrentPage * ItemsPerPage;
            if (last >= AllItemsCount)
            {
                last = AllItemsCount;
            }

            return last;
        }
    }

    public int ItemsPerPage { get; set; } = 10;
    public int AllItemsCount => Items?.Count ?? 0;

    private List<T>? ShowItems
    {
        get
        {
            try
            {
                if (PagerEnabled)
                {
                    var range = ItemsPerPage;
                    var itemDiff = LastItem - FirstItem;
                    if (ItemsPerPage > itemDiff)
                    {
                        range = itemDiff;
                    }

                    return Items?.GetRange(FirstItem, range);
                }
            }
            catch (Exception)
            {
                CurrentPage = 1;
            }

            return Items;
        }
    }

    private void AddItem()
    {
        var instance = Activator.CreateInstance<T>();
        
        AutoDialogService.Open(instance);
    }

    [Parameter]
    public Action<T> EditAction { get; set; }
    
    private void EditItem(T item)
    {
        AutoDialogService.Open(item);
    }

    private void OpenDetails(T item)
    {
        
    }

    public AutoTable()
    {
        //Initialize default actions
        
        RemoveAction = data =>
        {
            Collection?.Delete(data);
            Items?.Remove(data);
        };
        
        EditAction = EditItem;

        DetailsAction = OpenDetails;
    }

    [Parameter]
    public Action<T> RemoveAction { get; set; }

    [Parameter]
    public Action<T> DetailsAction { get; set; }
    
    [Parameter]
    public bool CanAdd { get; set; } = true;

    private bool CannotRefresh => _lastRefreshTick > Environment.TickCount;
    
    public bool NoItems => Items?.Count == 0;

    protected override async Task OnInitializedAsync()
    {
        var interfaces = typeof(T).GetInterfaces();
        _searchable = interfaces.Contains(typeof(ISearchable));
        _selectable = interfaces.Contains(typeof(ISelectable));
        Collection = new BaseCollection<T>(Database);
        
        Headers = GetProperties().Select(p => p.GetCustomAttribute<AutoOptions>()!.DisplayName).ToList();

        ItemAddedService.Listen<T>().Subscribe(data =>
        {
            try
            {
                var itemIndex = SourceItems?.FindIndex(i => i.Id == data.Id);
                if (itemIndex != null)
                {
                    SourceItems![(int) itemIndex] = data;
                }
                else
                {
                    SourceItems?.Add(data);
                }
            }
            catch (Exception)
            {
                SourceItems?.Add(data);
            }
            
            StateHasChanged();
        });
        
        await LoadItems();
    }

    public bool Loading => SourceItems == null || Items == null;

    private async Task LoadItems()
    {
        using var timer = new StopTimer("AutoTable.LoadItems");
        
        SourceItems = await Collection.All();

        Items = SourceItems;
        
        StateHasChanged();
    }

    private List<PropertyInfo> GetProperties()
    {
        var properties = new List<PropertyInfo>(typeof(T).GetProperties().Where(p => p.PropertyType.IsPublic));

        var tableProperties = properties.Where(p =>
        {
            var attr = p.GetCustomAttribute<AutoOptions>();
            return attr is {Table: true};
        }).OrderBy(p => p.GetCustomAttribute<AutoOptions>()!.Order).ToList();

        return tableProperties;
    }

    private List<string> GetStringValues(T item)
    {
        var values = GetProperties().Select(property => property.GetValue(item)?.ToString() ?? "NoValue").ToList();
        return values;
    }

    private void SetAllSelected(bool value)
    {
        _selectedAll = value;
        if (ShowItems == null)
        {
            return;
        }

        foreach (var selectable in ShowItems.Cast<ISelectable?>()) selectable!.Selected = value;

        StateHasChanged();
    }

    private static bool GetSelected(T item)
    {
        if(item is ISelectable selectable)
        {
            return selectable.Selected;
        }

        return false;
    }

    private static void SetSelected(T item, bool value)
    {
        if(item is ISelectable selectable)
        {
            selectable.Selected = value;
        }
    }

    private void FilterAll()
    {
        using var timer = new StopTimer("AutoTable.FilterAll");
        Items = SourceItems?.Where(Filter).ToList();
    }

    private bool Filter(T arg)
    {
        if (!_searchable || string.IsNullOrEmpty(_searchText))
        {
            return true;
        }

        if (arg is ISearchable searchable)
        {
            return searchable.SearchString.RemoveDiacritics()
                .Contains(_searchText.RemoveDiacritics(), StringComparison.OrdinalIgnoreCase);
        }

        return true;
    }

    private void PreviousPage()
    {
        _selectedAll = false;
        if (CurrentPage > 1)
        {
            CurrentPage--;
        }
    }

    private void NextPage()
    {
        _selectedAll = false;
        if (CurrentPage == PageCount)
        {
            return;
        }

        CurrentPage++;
    }

    private async Task Refresh()
    {
        if (CannotRefresh)
        {
            return;
        }

        _lastRefreshTick = Environment.TickCount + 5000;
        SourceItems = null;
        await Task.Delay(500);
        await LoadItems();
    }
}