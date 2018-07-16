using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaWindowsForm
{
    public partial class Form1 : Form
    {
        DB_A3E238_PersonasDBEntities db = new DB_A3E238_PersonasDBEntities();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            resetearCamposAgregar();
            actualizarGrid();

        }
        private void actualizarGrid()
        {
            this.personasTableAdapter.Fill(this.dB_A3E238_PersonasDBDataSet.Personas);

        }

        private void resetearCamposAgregar()
        {
            txt_cod.Text = txt_nom.Text = txt_ap.Text = txt_edad.Text = "";
            txt_fecha.Text = DateTime.Today.Date.ToShortDateString().ToString();

            btn_agregar.Text = "Guardar";

        }
        private void button4_Click(object sender, EventArgs e)
        {
            resetearCamposAgregar();

        }



       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1.CurrentRow.Index != -1)
            {
                btn_agregar.Text = "Actualizar";
                Personas persona = new Personas();
                int cod = Convert.ToInt32(dataGridView1.CurrentRow.Cells["cod"].Value.ToString());

                try
                {

                    persona = db.Personas.Find(cod);

                    txt_cod.Text = persona.cod.ToString();
                    txt_nom.Text = persona.nombre.ToString();
                    txt_ap.Text = persona.apellido.ToString();
                    txt_edad.Text = persona.edad.ToString();
                    txt_fecha.Text = persona.fecha_alta.Value.ToShortDateString();
                }
                catch (Exception ex) { }
            }
        }

        private void btn_borrar_Click(object sender, EventArgs e)
        {
            long cod = long.Parse(txt_cod.Text.ToString() );

            db.Personas.Remove(db.Personas.Find(cod));
            db.SaveChanges();
            actualizarGrid();
            resetearCamposAgregar();
        }

        private void btn_agregar_Click_1(object sender, EventArgs e)
        {
            //------------Agregar persona

            Personas persona = new Personas();
            if (btn_agregar.Text == "Actualizar")
            {
                persona.cod = long.Parse(txt_cod.Text.ToString());
            }
            try
            {

                persona.nombre = txt_nom.Text.ToString();
                persona.apellido = txt_ap.Text.ToString();
                persona.edad = Int32.Parse(txt_edad.Text.ToString());
                persona.fecha_alta = DateTime.Today.Date;

                db.Personas.Add(persona);
                db.SaveChanges();

                resetearCamposAgregar();
                actualizarGrid();
            }
            catch (Exception ex)
            {
                btn_agregar.Text = "error";
            }

        }
    }
}
