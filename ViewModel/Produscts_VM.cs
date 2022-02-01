using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Interfaces;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class Products_VM : Notify
    {
        public Products_VM()
        {
            // Инициализация контекста
            context = new LopushokEntities();

            // Инициализация свойства переключателя
            PageS = new PageSeparator(10, context.Product.Count());

            // Инициализация списка сортировки
            _sortList = new List<SortType<Product>> {
                new SortType<Product>(1, "По возрастанию",(x)=> x.OrderBy(p=>p.Title)),
                new SortType<Product>(2, "По убыванию", (x)=>x.OrderByDescending(p=>p.Title)) };

            // Инициализация списка типов продукции
            _listType = new List<ProductType>();
            _listType.Add(new ProductType { ID = 0, Title = "--Нет--" });
            _listType.AddRange(context.ProductType.AsNoTracking().ToList());

            // Инициализация базовой сортировки
            _selectedSort = _sortList[0];

            // Инициализация строки поиска
            _searchText = "";

            // Инициализация начального значения фильтра типа продукции
            _selectedType = ListType[0];
        }

        #region Переменные и свойства
        private LopushokEntities context;
        public LopushokEntities Context { get { return context; } }

        private Product _isSelected;
        public Product IsSelected { get { return _isSelected; } set { _isSelected = value; OnPropertyChanged(); } }



        private List<SortType<Product>> _sortList;
        public List<SortType<Product>> SortList { get { return _sortList; } set { _sortList = value; OnPropertyChanged(); } }

        private SortType<Product> _selectedSort;
        public SortType<Product> SelectedSort
        {
            get { return _selectedSort; }
            set
            {
                _selectedSort = value;
                SelectPage = 1;
                OnPropertyChanged(nameof(ProductList));
                OnPropertyChanged();
            }
        }

        private List<ProductType> _listType;
        public List<ProductType> ListType
        {
            get
            {
                return _listType;
            }
        }
        private ProductType _selectedType;
        public ProductType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                SelectPage = 1;
                OnPropertyChanged(nameof(ProductList));
                OnPropertyChanged();
            }
        }

        public List<Product> ProductList => context.Product
                .Where(x => x.Title.Contains(SearchText)// SqlFunctions.PatIndex("%"+SearchText.ToLower()+"%",x.Title.ToLower())>0
                && (SelectedType.ID == 0) || x.ProductTypeID == SelectedType.ID)
                .OrderByWith(SelectedSort)
                .AsNoTracking()
                .Skip(PageS.SkipCount())
                .Take(PageS.QuantityPerPage).ToList();

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                SelectPage = 1;
                OnPropertyChanged(nameof(ProductList));
                OnPropertyChanged();
            }
        }

        private PageSeparator _pageSeparator;
        public PageSeparator PageS { get { return _pageSeparator; } set { _pageSeparator = value; } }
        public int SelectPage
        {
            get { return PageS.CurrentPage; }
            set { PageS.CurrentPage = value; OnPropertyChanged(nameof(ProductList)); OnPropertyChanged(); }
        }
        #endregion

        #region Методы
        #endregion
    }

    public class SortType<T>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public delegate IQueryable<T> Order(IQueryable<T> ts);
        public Order del;
        public SortType(int ID, string Name, Order d)
        {
            this.ID = ID;
            this.Name = Name;
            this.del = d;
        }
    }
}
