using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ROMProjetos.Models.Base
{
    public class VerificaErros
    {
        public ObjetoRetorno<T> Verifica<T>() where T : class
        {
            var objetoRetorno = new ObjetoRetorno<T>();
            var resultados = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(this, new ValidationContext(this, null, null), resultados, true);

            if (!isvalid)
            {
                foreach (var item in resultados)
                {
                    foreach (var r in item.MemberNames)
                        objetoRetorno.AdicionaUmErro(typeof(T).Name + "." + r, item.ErrorMessage);
                }
                objetoRetorno.TemErro = true;
            }
            return objetoRetorno;
        }
    }
}