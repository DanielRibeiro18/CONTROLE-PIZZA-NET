using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace crudCJ301116X
{
    public partial class Pedidos : Form
    {
        private int Id;


        private void AtualizarCardapio()
        {
            ltvPedidoPizza.Items.Clear();
            DAO Dao = new DAO();

            List<Pizza> cardapio = Dao.ListarTodasPizzas();

            if (cardapio.Count > 0)
            {
                foreach (var car in cardapio)
                {
                    if (car.Disponibilidade == "SIM")
                    {
                        ListViewItem lv = new ListViewItem(car.Id.ToString());
                        lv.SubItems.Add(car.Sabor);
                        lv.SubItems.Add(car.Tamanho);
                        lv.SubItems.Add(car.NumeroFatia);
                        lv.SubItems.Add(car.Preco.ToString());
                        lv.SubItems.Add(car.Imagem);
                        ltvPedidoPizza.Items.Add(lv);
                    }
                }
            }
        }


        private void AtualizarListViewFinal()
        {
            ltvPedidoFinal.Items.Clear();
            DAO Dao = new DAO();

            List<PedidoFinal> pedidofinal = Dao.ListarTodosPedidosFinais();

            if (pedidofinal.Count > 0)
            {
                foreach (var pf in pedidofinal)
                {
                    ListViewItem lv = new ListViewItem(pf.Id.ToString());
                    lv.SubItems.Add(pf.Quantidade.ToString());
                    lv.SubItems.Add(pf.ValorFinal.ToString());
                    lv.SubItems.Add(pf.MetodoPag);
                    lv.SubItems.Add(pf.Troco.ToString());
                    lv.SubItems.Add(pf.Date.ToString("dd/MM/yyyy"));
                    ltvPedidoFinal.Items.Add(lv);
                }
            }
        }

        private void AtualizarListView()
        {
            ltvPedido.Items.Clear();
            DAO Dao = new DAO();

            List<Pedido> pedido = Dao.ListarTodosPedidos();
            if (pedido.Count > 0)
            {
                foreach (var ped in pedido)
                {
                    ListViewItem lv = new ListViewItem(ped.Id.ToString());
                    lv.SubItems.Add(ped.Valor.ToString());
                    lv.SubItems.Add(ped.Quantidade.ToString());
                    lv.SubItems.Add(ped.Sabor);

                    ltvPedido.Items.Add(lv);
                }
            }
        }

        private void LimparCampos()
        {
            txbID.Clear();
            txbValorUnit.Clear();
            txbQuant.Clear();
            txbIdPizza.Clear();
            ptbPizza.Image = null;

        }

        public Pedidos()
        {
            InitializeComponent();
        }

        private void btnCardapio_Click(object sender, EventArgs e)
        {
            pnlCardapio.Visible = true;
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            pnlCardapio.Visible = false;
        }

        private void ltvPedidoPizza_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Pedidos_Load(object sender, EventArgs e)
        {
            try
            {
                AtualizarListView();
                AtualizarListViewFinal();
                AtualizarCardapio();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "OCORREU UM ERRO AO CARREGAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void pnlCardapio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Pedido pedidoLido;

                decimal valor = ValorUnit(decimal.Parse(txbValorUnit.Text));

                pedidoLido = new Pedido(valor, int.Parse(txbQuant.Text), int.Parse(txbIdPizza.Text));
                DAO Dao = new DAO();
                Dao.InserirNovoPedido(pedidoLido);


                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO AO INSERIR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AtualizarListView();
            LimparCampos();

        }


        private void btnEditar_Click(object sender, EventArgs e)
        {


        }

        private decimal ValorUnit(decimal total = 0m)
        {

            int quant = int.Parse(txbQuant.Text);

            decimal preco = decimal.Parse(txbValorUnit.Text);

            total += ((quant - 1) * preco);


            return total;
        }

        private void ValorTotal()
        {
            decimal total = 0m;
            for (int i = 0; i < ltvPedido.Items.Count; i++)

            {
                total += decimal.Parse(ltvPedido.Items[i].SubItems[1].Text); ;
            }
            txbTotalaPagar.Text = total.ToString("N");

        }
        private void QuantItens()
        {
            decimal total = 0;
            for (int i = 0; i < ltvPedido.Items.Count; i++)

            {
                total += int.Parse(ltvPedido.Items[i].SubItems[2].Text); ;
            }
            txbQuantFinal.Text = total.ToString();

        }

        private void CriarTroco()
        {

            if (txbTotalPago.Text == string.Empty)
            {
                MessageBox.Show("Tente novamente! Não há entrada de caixa.");
                txbTroco.Clear();
                return;
            }

            else
            {
                decimal total = 0m;
                decimal pago = decimal.Parse(txbTotalaPagar.Text);

                decimal pagar = decimal.Parse(txbTotalPago.Text);

                total += (pagar - pago);

                txbTroco.Text = total.ToString("N");
            }

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Tem certeza que deseja editar?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {

                try
                {
                    Pedido pedidoEditado;

                    decimal valor = ValorUnit(decimal.Parse(txbValorUnit.Text));

                    pedidoEditado = new Pedido(Id, valor, int.Parse(txbQuant.Text), int.Parse(txbIdPizza.Text));
                    DAO Dao = new DAO();

                    

                    Dao.EditarPedido(pedidoEditado);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERRO AO EDITAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            AtualizarListView();
            LimparCampos();
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            DAO Dao = new DAO();

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja excluir o pedido?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    
                    Dao.ExcluirPedido(Id);
                }
                catch (Exception)
                {
                    throw new Exception("ERRO AO EXCLUIR");
                }
            }

            AtualizarListView();
            LimparCampos();
        }


        private void btnRetornarPedido_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ltvPedido_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index;
            try
            {   

                index = ltvPedido.FocusedItem.Index;
                Id = int.Parse(ltvPedido.Items[index].SubItems[0].Text);
                txbID.Text = ltvPedido.Items[index].SubItems[0].Text;
                txbIdPizza.Text = ltvPedidoPizza.Items[index].SubItems[0].Text;
                txbIdPedido.Text = ltvPedido.Items[index].SubItems[0].Text;
                txbValorUnit.Text = ltvPedidoPizza.Items[index].SubItems[4].Text;
                txbQuant.Text = ltvPedido.Items[index].SubItems[2].Text;
                

                ValorTotal();
                QuantItens();



            }
            catch (Exception)
            {
                MessageBox.Show("Você precisa selecionar uma linha", "Erro");
            }
        }

        private void ltvPedidoPizza_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ltvPedidoPizza_SelectedIndexChanged_2(object sender, EventArgs e)
        {
           
        }

        private void ltvPedidoPizza_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index;
            try
            {
                index = ltvPedidoPizza.FocusedItem.Index;
                Id = int.Parse(ltvPedidoPizza.Items[index].SubItems[0].Text);
                txbIdPizza.Text = ltvPedidoPizza.Items[index].SubItems[0].Text;
                txbValorUnit.Text = ltvPedidoPizza.Items[index].SubItems[4].Text;
                ptbPizza.ImageLocation = ltvPedidoPizza.Items[index].SubItems[5].Text;

            }
            catch (Exception)
            {
                MessageBox.Show("Você precisa selecionar uma linha", "Erro");
            }
            txbQuant.Focus();
            pnlCardapio.Visible = false;
        }

        private void rdbPix_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            pnlFinalizar.Visible = true;
            dtpPedido.MaxDate = DateTime.Now;
            rdbDinheiro.Checked = true;
        }

        private void btnRetornarFinal_Click(object sender, EventArgs e)
        {
            DAO Dao = new DAO();
            Dao.RemoverPedidoAtual();

            AtualizarListViewFinal();
            AtualizarListView();

            pnlFinalizar.Visible = false;
        }

        private void txbTotalPago_TextChanged(object sender, EventArgs e)
        {
            CriarTroco();
        }

        private void btnInserirFim_Click(object sender, EventArgs e)
        {
            try
            {
                PedidoFinal pedidofinalLido;


                if (rdbPix.Checked)
                {
                    pedidofinalLido = new PedidoFinal(int.Parse(txbQuantFinal.Text), decimal.Parse(txbTotalaPagar.Text), "PIX", decimal.Parse(txbTroco.Text), DateTime.Parse(dtpPedido.Text));
                    DAO Dao = new DAO();
                    Dao.InserirFinal(pedidofinalLido);
                }
                if (rdbCartao.Checked)
                {
                    pedidofinalLido = new PedidoFinal(int.Parse(txbQuantFinal.Text), decimal.Parse(txbTotalaPagar.Text), "CARTAO", decimal.Parse(txbTroco.Text), DateTime.Parse(dtpPedido.Text));
                    DAO Dao = new DAO();
                    Dao.InserirFinal(pedidofinalLido);
                }
                if (rdbDinheiro.Checked)
                {
                    pedidofinalLido = new PedidoFinal(int.Parse(txbQuantFinal.Text), decimal.Parse(txbTotalaPagar.Text), "DINHEIRO", decimal.Parse(txbTroco.Text), DateTime.Parse(dtpPedido.Text));
                    DAO Dao = new DAO();
                    Dao.InserirFinal(pedidofinalLido);
                }

                Relatorio rel;
                TimeSpan horario = DateTime.Now.TimeOfDay;

                if (rdbPix.Checked)
                {
                    rel = new Relatorio(int.Parse(txbQuantFinal.Text), decimal.Parse(txbTotalaPagar.Text), decimal.Parse(txbTotalPago.Text), "PIX", decimal.Parse(txbTroco.Text), DateTime.Parse(dtpPedido.Text), horario);
                    DAO Dao = new DAO();
                    Dao.InserirRelatorio(rel);
                }
                if (rdbCartao.Checked)
                {
                    rel = new Relatorio(int.Parse(txbQuantFinal.Text), decimal.Parse(txbTotalaPagar.Text), decimal.Parse(txbTotalPago.Text), "CARTAO", decimal.Parse(txbTroco.Text), DateTime.Parse(dtpPedido.Text), horario);
                    DAO Dao = new DAO();
                    Dao.InserirRelatorio(rel);
                }
                if (rdbDinheiro.Checked)
                {
                    rel = new Relatorio(int.Parse(txbQuantFinal.Text), decimal.Parse(txbTotalaPagar.Text), decimal.Parse(txbTotalPago.Text), "DINHEIRO", decimal.Parse(txbTroco.Text), DateTime.Parse(dtpPedido.Text), horario);
                    DAO Dao = new DAO();
                    Dao.InserirRelatorio(rel);
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO AO INSERIR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AtualizarListViewFinal();
            LimparCampos();
        }

        private void txbQuantTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void ptbPizza_Click(object sender, EventArgs e)
        {

        }

        private void txbQuantFinal_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnExcluirFim_Click(object sender, EventArgs e)
        {
            DAO Dao = new DAO();

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja excluir o pedido final?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    
                    Dao.ExcluirFim(Id);
                }
                catch (Exception)
                {
                    throw new Exception("ERRO AO EXCLUIR");
                }
            }

            AtualizarListViewFinal();
            LimparCampos();
        }

        private void ltvPedidoFinal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index;
            try
            {
                index = ltvPedidoFinal.FocusedItem.Index;
                Id = int.Parse(ltvPedidoFinal.Items[index].SubItems[0].Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Você precisa selecionar uma linha", "Erro");
            }
        }

        private void ltvPedidoFinal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ltvPedido_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ltvPedido_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dtpPedido_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txbValorUnit_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlFinalizar_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
