using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ROMProjetos.Models.Base
{
    public struct Erro
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }

    public class VerificaErros
    {
        private readonly List<Erro> _erros;

        public VerificaErros()
        {
            _erros = new List<Erro>();
        }

        public List<Erro> Erros()
        {
            return _erros;
        }

        public void AdicionaErro(string mensagem)
        {
            AdicionaErro("generic_property", mensagem);
        }

        public void AdicionaErro(string propriedade, string mensagem)
        {
            _erros.Add(new Erro
                           {
                               Chave = propriedade,
                               Valor = mensagem
                           });
        }

        public virtual bool Validar()
        {
            var resultados = new List<ValidationResult>();
            var eValido = Validator.TryValidateObject(this, new ValidationContext(this, null, null), resultados, true);

            if (eValido) return true;

            foreach (var validacaoResultado in resultados)
            {
                AdicionaErro(validacaoResultado.ErrorMessage);

                var itemComplexo = validacaoResultado as ComplexValidationResult;
                if (itemComplexo == null) continue;
                foreach (var result in itemComplexo.Results)
                    AdicionaErro(result.ErrorMessage);
            }

            return false;
        }
    }

    public class ComplexValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results = new List<ValidationResult>();

        public ComplexValidationResult(string errorMessage) : base(errorMessage) { }

        public ComplexValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }

        protected ComplexValidationResult(ValidationResult validationResult) : base(validationResult) { }

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return _results;
            }
        }

        public void AddResult(ValidationResult validationResult)
        {
            _results.Add(validationResult);
        }
    }
}