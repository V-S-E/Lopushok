using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Interfaces
{
    public class PageSeparator : Notify
    {
        public PageSeparator(int quantityPerPage, int totalRecords)
        {
            QuantityPerPage = quantityPerPage;
            TotalRecords = totalRecords;
            NumberOfPages = (int)((TotalRecords / quantityPerPage)+0.5);
            PagesArray = new ObservableCollection<int>();
            for (int i=0; i<NumberOfPages; i++)
            {
                PagesArray.Add(i+1);
            }
            CurrentPage = 1;
        }

        
        private int _currentPage;
        public int CurrentPage { get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(); } }
        public ObservableCollection<int> PagesArray { get; private set; }
        public int NumberOfPages { get; private set; }
        public int QuantityPerPage { get; private set; }
        public int TotalRecords { get; private set; }
        public int SkipCount()
        {
            return (CurrentPage-1) * QuantityPerPage;
        }
    }
}
