using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Interfaces
{
    public abstract class ValidatorBase : IDataErrorInfo
    {
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (String.IsNullOrEmpty(columnName)) throw new Exception("Свойство не указано");

                string error = String.Empty;
                foreach(var r in ValidList)
                {
                    if (r._name == columnName && r._check()) { error = r._message; break; }
                }

                /*if (string.IsNullOrEmpty(columnName))
                {
                    throw new NotSupportedException("Имя свойства не указано");
                }
                var error = string.Empty;
                var results = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(GetValue(columnName), new ValidationContext(this) { MemberName = columnName }, results);
                if (!result)
                {
                    var validationResult = results.First();
                    error = validationResult.ErrorMessage;
                }
                return error;*/
                return error;
            }
        }

        protected List<ValidItem> ValidList = new List<ValidItem>();

        public string Validation()
        {
            //StringBuilder errors = new StringBuilder();
            var errors = new List<string>();

            foreach(var r in ValidList)
            {
                if (r._check()) errors.Add(r._message);
            }
            return string.Join("\n\n",errors);
        }

        /*private object GetValue(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);
            return property.GetValue(this);
        }*/
        string IDataErrorInfo.Error => throw new NotSupportedException("IDataErrorInfo.Error is not supported, use IDataErrorInfo.this[propertyName] instead.");
        /*public string Validation()
        {
            string errors = string.Empty;
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, context,results,true))
            {
                errors = string.Join("\n",results.Select(x=>x.ErrorMessage));
            }
            return errors;
        }*/

    }

    public class ValidItem
    {
        public string _message;
        public string _name;

        public delegate bool check_d(object[] param = null);
        public check_d _check;
    }
}
