using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crudCJ301116X
{
    public partial class TelaMenu : Form
    {
        public TelaMenu(string admin)
        {
            InitializeComponent();

            if (admin == "NAO")
            {
                CadastroToolStripMenuItem.Visible = false;
                RelatoriotoolStripMenuItem1.Visible = false;
            }
                
                
        }

        private void nOVOUSUÁRIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            CrudCadastro cadastro = new CrudCadastro();
            cadastro.ShowDialog();
            this.Visible = true;
        }

        private void cADASTROToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pIZZAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            CrudPizza pizza = new CrudPizza();
            pizza.ShowDialog();
            this.Visible = true;
        }

        private void nOVOPEDIDOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Pedidos ped = new Pedidos();
            ped.ShowDialog();
            this.Visible = true;
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Tem certeza que deseja realizar logout?", "LOGOUT", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("REALIZANDO LOGOUT, OBRIGADO POR UTILIZAR O SISTEMA!");
                Close();
            }
        }

        private void RelPedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void RelPedidostoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string nomeArquivo = @"C:\Users\Dan\Desktop\CONTROLE-PIZZA-NET\crudCJ301116X\RelatórioPedidos\Pedidos.pdf";
            FileStream arquivoPDF = new FileStream(nomeArquivo, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPDF);

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"C:\Users\Dan\Desktop\CONTROLE-PIZZA-NET\crudCJ301116X\Imagensprojeto\LOGOS\LogoPIZZA.png");
            logo.ScaleToFit(200f, 150f);
            logo.Alignment = Element.ALIGN_CENTER;


            DAO Dao = new DAO();
            List<Relatorio> listaDeRelatorio = Dao.ListarRelatorio();

            var pedidosPorData = listaDeRelatorio.GroupBy(relatorio => relatorio.Data).OrderBy(grupo => grupo.Key);

            doc.Open();

            bool primeiroGrupo = true;

            foreach (var grupoPedidos in pedidosPorData)
            {
                DateTime dataPedido = grupoPedidos.Key;

                if (primeiroGrupo)
                {
                    Paragraph paragrafo = new Paragraph("CJ301116X", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 16, (int)System.Drawing.FontStyle.Bold));
                    paragrafo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                    doc.Add(paragrafo);
                    primeiroGrupo = false;
                }

                Paragraph paragrafo2 = new Paragraph("RELATÓRIO DO DIA " + dataPedido.ToString("dd/MM/yyyy"), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold));
                paragrafo2.Alignment = Element.ALIGN_CENTER;

                PdfPTable tabelaPedidos = new PdfPTable(6);
                tabelaPedidos.SpacingBefore = 20f;

                tabelaPedidos.AddCell("Quantidade de Pizzas");
                tabelaPedidos.AddCell("Valor Total");
                tabelaPedidos.AddCell("Total Pago");
                tabelaPedidos.AddCell("Método de Pagamento");
                tabelaPedidos.AddCell("Troco");
                tabelaPedidos.AddCell("Horário");

                foreach (Relatorio relatorio in grupoPedidos)
                {
                    tabelaPedidos.AddCell(relatorio.Quantidade.ToString());
                    tabelaPedidos.AddCell(relatorio.ValorFinal.ToString());
                    tabelaPedidos.AddCell(relatorio.TotalPago.ToString());
                    tabelaPedidos.AddCell(relatorio.MetodoPag);
                    tabelaPedidos.AddCell(relatorio.Troco.ToString());
                    tabelaPedidos.AddCell(relatorio.Horario.ToString(@"hh\:mm"));
                }

                doc.Add(tabelaPedidos);
                doc.Add(paragrafo2);
            }

            DateTime dataHoraGeracao = DateTime.Now;
            Paragraph paragrafo3 = new Paragraph("Relatório gerado dia " + dataHoraGeracao.ToString("dd/MM") + " às " + dataHoraGeracao.ToString("HH:mm:ss"), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 15, iTextSharp.text.Font.BOLD));
            paragrafo3.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragrafo3);


            doc.Close();

            DialogResult resultado = MessageBox.Show("Deseja abrir o relatório de pedidos?", "Relatório", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
                System.Diagnostics.Process.Start(@"C:\Users\Dan\Desktop\CONTROLE-PIZZA-NET\crudCJ301116X\RelatórioPedidos\Pedidos.pdf");
        }
    }
}
