using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace crudCJ301116X
{
    internal class Pedido
    {
        private int _id;
        private decimal _valor;
        private int _quantidade;
        private int _idpizza;
        private string _sabor;



        public Pedido(decimal valor, int quantidade, int idpizza)
        {
            _valor = valor;
            _quantidade = quantidade;
            _idpizza = idpizza;

        }

        public Pedido(int id, decimal valor, int quantidade, int idpizza) :
            this(valor, quantidade, idpizza)
        {
            _id = id;

        }

        public Pedido(decimal valor, int quantidade, string sabor)
        {
            _valor = valor;
            _quantidade = quantidade;
            Sabor = sabor;

        }

        public Pedido(int id, decimal valor, int quantidade, string sabor) :
            this(valor, quantidade, sabor)
        {
            _id = id;

        }

        //Validação do Id

        public int Id
        {
            get { return _id; }
        }

        //Validação do Valor.
        public decimal Valor
        {
            get { return _valor; }

        }

        //Validação da quantidade.
        public int Quantidade
        {
            get { return _quantidade; }

        }

        public int Idpizza
        {
            get { return _idpizza; }
        }

        //Validação do Sabor.
        public string Sabor
        {
            get { return _sabor; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("O sabor informado não é um texto válido");
                _sabor = value;
            }
        }


    }
}
