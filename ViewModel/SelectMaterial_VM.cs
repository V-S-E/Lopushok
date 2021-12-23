using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class SelectMaterial_VM : Notify
    {
        public SelectMaterial_VM()
        {
            context = new LopushokEntities();
        }

        #region Переменные и свойства
        private LopushokEntities context;
        private string _search;
        public string Search { get { return _search; } set { _search = value; OnPropertyChanged(); OnPropertyChanged(nameof(Materials)); } }
        public List<MaterialType> Materials => context.MaterialType.Include(x=>x.Material).OrderBy(x => x.Title).AsNoTracking().ToList();
        public Material Selected { get; set; }
        #endregion
    }
}
