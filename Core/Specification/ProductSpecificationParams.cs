using System;

namespace Core.Specification;

public class ProductSpecificationParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 5;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }
    
    private List<string> _options = [];
    public List<string> Options
    {
        get => _options; //options = M%Xanh%Do%S
        set
        {
            _options = value.SelectMany(x => x.Split('%', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

}
