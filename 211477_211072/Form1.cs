
namespace _211477_211072
{
    public partial class txtCódigo : Form
    {
 

        public txtCódigo()
        {
            InitializeComponent();
        }
        private void txtCódigo_Load(object sender, EventArgs e)
        {

            dgvDespesas.Columns.Insert(0, new DataGridViewCheckBoxColumn());
            dgvDespesas.Columns.Insert(1, new DataGridViewTextBoxColumn());
            dgvDespesas.Columns.Insert(2, new DataGridViewTextBoxColumn());
            dgvDespesas.Columns.Insert(3, new DataGridViewTextBoxColumn());
            dgvDespesas.Columns.Insert(4, new DataGridViewTextBoxColumn());
            dgvDespesas.Columns.Insert(5, new DataGridViewTextBoxColumn());
            dgvDespesas.Columns.Insert(6, new DataGridViewTextBoxColumn());


            dgvDespesas.Columns[0].Name = "OK";
            dgvDespesas.Columns[1].Name = "ID";
            dgvDespesas.Columns[2].Name = "Codigo de Barras";
            dgvDespesas.Columns[3].Name = "Produto";
            dgvDespesas.Columns[4].Name = "Valor Compra";
            dgvDespesas.Columns[5].Name = "Valor Venda";
            dgvDespesas.Columns[6].Name = "Estoque";

            dgvDespesas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDespesas.ReadOnly = true;
            dgvDespesas.AllowUserToAddRows = false;
            dgvDespesas.AllowUserToDeleteRows = false;
            dgvDespesas.AllowUserToOrderColumns = true;

            dgvDespesas.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDespesas.Columns[4].DefaultCellStyle.Format = "###,###,##0.00";

            dgvDespesas.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDespesas.Columns[5].DefaultCellStyle.Format = "###,###,##0.00";

        }

        double total;
        double selecao;

        public object Code128Rendering { get; private set; }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            dgvDespesas.RowCount = 0;

            ofdArquivo.FileName = "";
            ofdArquivo.ShowDialog();

            StreamReader arquivo = File.OpenText(ofdArquivo.FileName);

            String linha;
            while ((linha = arquivo.ReadLine()) != null)
            {
                string[] dados = linha.Split(';');
                string id = dados[0];
                string codigo = dados[1];
                string descricao = dados[2];
                double valor_compra = double.Parse(dados[3]);
                double valor_venda = double.Parse(dados[4]);
                int estoque = int.Parse(dados[5]);

                dgvDespesas.Rows.Add(false, id, codigo, descricao, valor_compra, valor_venda, estoque);
            }        
            foreach (DataGridViewRow dgvlinha in dgvDespesas.Rows)
            if (Convert.ToDouble(dgvlinha.Cells[4].Value) > Convert.ToDouble(dgvlinha.Cells[5].Value))
                {
                    dgvlinha.Cells[5].Style.BackColor = Color.Red;
                }
        }


        private void btnMarcar_Click(object sender, EventArgs e)
        {
            selecao = 0;
            foreach (DataGridViewRow linha in dgvDespesas.Rows)
            {
                linha.Cells[0].Value = true;
                selecao += Convert.ToDouble(linha.Cells[5].Value);
            }
            lblSelecao.Text = selecao.ToString("C");
        }

        private void btnDesmarcar_Click(object sender, EventArgs e)
        {
            selecao = 0;
            foreach (DataGridViewRow linha in dgvDespesas.Rows)
                linha.Cells[0].Value = false;
            lblSelecao.Text = selecao.ToString("C");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvDespesas.Rows.Count > 0)
                dgvDespesas.Rows.RemoveAt(dgvDespesas.CurrentCell.RowIndex);
            MessageBox.Show(" Excluido com Sucesso", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnJuros_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow linha in dgvDespesas.Rows)
            {
                if (Convert.ToBoolean(linha.Cells[0].Value) == true)
                {
                    double valor = Convert.ToDouble(linha.Cells[5].Value);
                    linha.Cells[5].Value = valor + valor * 10 / 100;
                }
                if (Convert.ToDouble(linha.Cells[5].Value) > Convert.ToDouble(linha.Cells[4].Value))
                {
                    linha.Cells[5].Style.BackColor = Color.Empty;
                }
            }
        }

        private void dgvDespesas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDespesas.Rows.Count > 0 && e.ColumnIndex == 0)
            {
                dgvDespesas.CurrentRow.Cells[0].Value = true;
                selecao += Convert.ToDouble(dgvDespesas.CurrentRow.Cells[5].Value);
            }
            else
            {
                dgvDespesas.CurrentRow.Cells[0].Value = false;
                selecao -= Convert.ToDouble(dgvDespesas.CurrentRow.Cells[5].Value);
            }
            lblSelecao.Text = selecao.ToString("C");
        }
    }
}