using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudCJ301116X
{
    internal class Relatorio
    {
        private int _id;
        private int _quantidade;
        private decimal _valorfinal;
        private decimal _totalpago;
        private string _metodopag;
        private decimal _troco;
        private DateTime _data;
        private TimeSpan _horario;

        public Relatorio(int quantidade, decimal valorfinal, decimal totalpago, string metodopag, decimal troco, DateTime data, TimeSpan horario)
        {
            _quantidade = quantidade;
            _valorfinal = valorfinal;
            _totalpago = totalpago;
            _metodopag = metodopag;
            _troco = troco;
            _data = data;
            _horario = horario;
        }

        public Relatorio(int id, int quantidade, decimal valorfinal, decimal totalpago, string metodopag, decimal troco, DateTime data, TimeSpan horario)
            : this(quantidade, valorfinal, totalpago, metodopag, troco, data, horario)
        {
            _id = id;
        }

        //Validação do Id
        public int Id
        {
            get { return _id; }
        }

        //Validação da Quantidade.
        public int Quantidade
        {
            get { return _quantidade; }
        }

        //Validação do Valor final.
        public decimal ValorFinal
        {
            get { return _valorfinal; }
        }

        //Validação do Total pago.
        public decimal TotalPago
        {
            get { return _totalpago; }
        }

        //Validação do Método de Pagamento.
        public string MetodoPag
        {
            get { return _metodopag; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("O método de pagamento informado não é um texto válido");
                _metodopag = value;
            }
        }

        //Validação do Troco.
        public decimal Troco
        {
            get { return _troco; }
        }

        //Validação da Data.
        public DateTime Data
        {
            get { return _data; }
        }

        //Validação do Horário.
        public TimeSpan Horario
        {
            get { return _horario; }
        }
    }
}
