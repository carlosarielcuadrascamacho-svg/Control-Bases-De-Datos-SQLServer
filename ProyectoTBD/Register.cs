using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SQLConn;
using System.Data.SqlClient;

namespace ProyectoTBD
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.Size = new Size(293, 481);
            
            toolTip1.SetToolTip(txtPassword, "La contraseña debe tener al menos:\n- 8 caracteres,\n- 1 letra mayúscula,\n- 1 número,\n- 1 carácter especial.");

            try
            {
                // Limpiar el ComboBox antes de intentar rellenarlo.
                cbBasesDeDatos.Items.Clear();

                // Crear una tabla para recibir los datos de la consulta.
                DataTable tabla = new DataTable();

                string sa = "sa";
                string pass = "CarlosAriel";

                // Crear una instancia de la clase SQLConexion.
                sqlconect sQLConexion = new sqlconect(sa,pass);

                // Verificar si la operación fue exitosa.
                bool result = sQLConexion.BasesDatosServer(ref tabla);

                if (!result)
                {
                    throw new Exception("Error al obtener las bases de datos desde el servidor.");
                }

                // Rellenar el ComboBox con las bases de datos válidas.
                foreach (DataRow row in tabla.Rows)
                {
                    string dbName = Convert.ToString(row["name"]);

                    // Excluir bases de datos del sistema.
                    if (dbName != "master" && dbName != "model" && dbName != "msdb" && dbName != "tempdb")
                    {
                        cbBasesDeDatos.Items.Add(dbName);
                        clbBasesDeDatos.Items.Add(dbName);
                    }
                }

                if (cbBasesDeDatos.Items.Count == 0)
                {
                    MessageBox.Show("No se encontraron bases de datos válidas.");
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error de SQL al intentar conectarse a la base de datos: {sqlEx.Message}", "Error SQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al conectarse a la base de datos: {ex.Message}", "Error General");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if(txtUser.Text == string.Empty)
            {
                MessageBox.Show("ESCRIBE UN USUARIO");
                return;
            }
            if(txtPassword.Text == string.Empty)
            {
                MessageBox.Show("ESCRIBE UNA CONTRASEÑA");
                return;
            }
            if (txtConfirmPassword.Text == string.Empty)
            {
                MessageBox.Show("VALIDA TU CONTRASEÑA");
                return;
            }

            if (!ValidarContraseña(password))
            {
                lblMensaje.Text = "La contraseña debe tener al menos 8 caracteres, una mayúscula, un número y un carácter especial.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            if (password != confirmPassword)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            lblMensaje.Text = "Contraseña válida y coincidente.";
            lblMensaje.ForeColor = Color.Green;
            sqlconect con = new sqlconect("sa","CarlosAriel");
            if (con.CrearNuevoUsuario(CrearQuery()))
            {
                MessageBox.Show($"El usuario {txtUser.Text} se agrego correctamente");
            }

        }

        private string CrearQuery()
        {
            // Obtener el nombre de usuario, contraseña, base de datos y idioma
            string userName = txtUser.Text.Trim();
            string password = txtPassword.Text;
            string defaultDatabase;
            string defaultLanguage;
            if (cbBasesDeDatos.Text.ToString() == string.Empty)
            {
                defaultDatabase = "master";
            }
            else
            {
               defaultDatabase = cbBasesDeDatos.SelectedItem.ToString(); // Base de datos predeterminada
            }

            if (cbIdiomas.Text.ToString() == string.Empty) // Idioma predeterminado) == string.Empty)
            {
                defaultLanguage = "Español";
            }
            else
            {
                defaultLanguage = cbIdiomas.SelectedItem.ToString(); // Idioma predeterminado
            }
            

            // Iniciar la construcción del query
            StringBuilder queryBuilder = new StringBuilder();

            // Crear el login
            queryBuilder.AppendLine($@"
                                    CREATE LOGIN [{userName}]
                                    WITH PASSWORD = '{password}', 
                                    DEFAULT_DATABASE = [{defaultDatabase}],
                                    DEFAULT_LANGUAGE = [{defaultLanguage}];");

            // Añadir la creación del usuario y roles para cada base de datos seleccionada
            foreach (var item in clbBasesDeDatos.CheckedItems)
            {
                string databaseName = item.ToString();

                // Agregar al query
                queryBuilder.AppendLine($@"
                                    USE [{databaseName}];
                                    CREATE USER [{userName}] FOR LOGIN [{userName}];
                                    ALTER ROLE db_datareader ADD MEMBER [{userName}];
                                    ALTER ROLE db_datawriter ADD MEMBER [{userName}];
                                    ALTER ROLE db_owner ADD MEMBER [{userName}];");
            }

            return queryBuilder.ToString(); // Devuelve el query completo
        }


        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
           

            if (ValidarContraseña(txtPassword.Text))
            {
                lblMensaje.Text = "Contraseña cumple con los requisitos.";
                // Mensaje del ToolTip que se mostrará cada vez que se escriba un carácter
                string mensaje = "La contraseña cumple con todos los requisitos";

                // Forzar la aparición del ToolTip
                toolTip1.Show(mensaje, txtPassword, 150, 0, 3000); // Ubicación y duración (3 segundos)
                lblMensaje.ForeColor = Color.Green;
            }
            else
            {
                lblMensaje.Text = "Contraseña no cumple con los requisitos.";
                // Mensaje del ToolTip que se mostrará cada vez que se escriba un carácter
                string mensaje = "La contraseña debe tener al menos:\n- 8 caracteres,\n- 1 letra mayúscula,\n- 1 número,\n- 1 carácter especial.";

                // Forzar la aparición del ToolTip
                toolTip1.Show(mensaje, txtPassword, 150, -50, 3000); // Ubicación y duración (3 segundos)
                lblMensaje.ForeColor = Color.Red;
            }

        }
        public bool ValidarContraseña(string password)
        {
            // Expresión regular para validar los requisitos
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Abrir la ventana principal del sistema como modal
            using (Login mainWindow = new Login())
            {
                this.Hide();  // Ocultar el formulario de login
                var result = mainWindow.ShowDialog();  // Mostrar la ventana principal como modal

                // Si se cierra la ventana principal, cerramos el formulario de login también
                if (result == DialogResult.OK || result == DialogResult.Cancel)
                {
                    this.Close();  // Cerrar el formulario de login
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Size = new Size(577, 475);
        }
    }
}
