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
            context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
            _search = String.Empty;
        }

        #region Переменные и свойства
        private LopushokEntities context;
        private string _search;
        public string Search { get { return _search; } set { _search = value; OnPropertyChanged(); OnPropertyChanged(nameof(Materials)); } }
        public List<MaterialType> Materials => context.MaterialType
            .AsNoTracking()
            .AsEnumerable()
            .Select(s => new MaterialType()
            {
                ID = s.ID,
                Material = context.Material.AsNoTracking().Where(x => x.MaterialType.ID == s.ID && x.Title.Contains(Search)).ToList(),
                Title = s.Title,
            }).ToList();
        public Material Selected { get; set; }

        #endregion
    }
}
