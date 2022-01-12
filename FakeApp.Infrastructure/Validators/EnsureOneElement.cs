using System.Collections;
using System.ComponentModel.DataAnnotations;



namespace FakeApp.Infrastructure.Validators
{
    /// <summary>
    /// Валидатор, проверяющий на присутствие хотя бы одного элемента в коллекции
    /// </summary>
    public class EnsureOneElement : ValidationAttribute
    {
        public EnsureOneElement()
        {
            ErrorMessage = "There must be at least one element";
        }
        

        public override bool IsValid(object value)
        {
            if (value is IList list)
            {
                return list.Count > 0;
            }
            
            return false;
        }
    }
}