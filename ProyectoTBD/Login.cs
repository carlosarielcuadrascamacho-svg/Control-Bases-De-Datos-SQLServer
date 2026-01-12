using SQLConn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace ProyectoTBD
{
    public partial class Login : Form
    {
        
        List<Image> images = new List<Image>();
        string[] location = new string[25];
        public string user;
        public string pass;

        public Login()
        {
            InitializeComponent();

            try
            {
                location[0] = @"animation\textbox_user_1.jpg";
                location[1] = @"animation\textbox_user_2.jpg";
                location[2] = @"animation\textbox_user_3.jpg";
                location[3] = @"animation\textbox_user_4.jpg";
                location[4] = @"animation\textbox_user_5.jpg";
                location[5] = @"animation\textbox_user_6.jpg";
                location[6] = @"animation\textbox_user_7.jpg";
                location[7] = @"animation\textbox_user_8.jpg";
                location[8] = @"animation\textbox_user_9.jpg";
                location[9] = @"animation\textbox_user_10.jpg";
                location[10] = @"animation\textbox_user_11.jpg";
                location[11] = @"animation\textbox_user_12.jpg";
                location[12] = @"animation\textbox_user_13.jpg";
                location[13] = @"animation\textbox_user_14.jpg";
                location[14] = @"animation\textbox_user_15.jpg";
                location[15] = @"animation\textbox_user_16.jpg";
                location[16] = @"animation\textbox_user_17.jpg";
                location[17] = @"animation\textbox_user_18.jpg";
                location[18] = @"animation\textbox_user_19.jpg";
                location[19] = @"animation\textbox_user_20.jpg";
                location[20] = @"animation\textbox_user_21.jpg";
                location[21] = @"animation\textbox_user_22.jpg";
                location[22] = @"animation\textbox_user_23.jpg";
                location[23] = @"animation\textbox_user_24.jpg";
                diapo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar imágenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void diapo()
        {
            try
            {
                for (int i = 0; i < 23; i++)
                {
                    Bitmap bmp = new Bitmap(location[i]);
                    images.Add(bmp);
                }
                images.Add(Properties.Resources.textbox_user_24); // Imagen adicional del recurso
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Error en las rutas de las imágenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al cargar las imágenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            tbPass.UseSystemPasswordChar = true;
        }
        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            User();
        }

        private void User()
        {
            try
            {
                if (tbUser.Text.Length > 0 && tbUser.Text.Length <= 24)
                {
                    pictureBox1.Image = images[tbUser.Text.Length - 1];
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (tbUser.Text.Length <= 0)
                    pictureBox1.Image = Properties.Resources.debut;
                else
                    pictureBox1.Image = images[22];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Índice fuera de rango al acceder a las imágenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbUser.Text.Length > 0)
                    pictureBox1.Image = images[tbUser.Text.Length - 1];
                else
                    pictureBox1.Image = Properties.Resources.debut;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Índice fuera de rango al acceder a las imágenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbPass_Click(object sender, EventArgs e)
        {
            password();
        }



        public void password()
        {
            try
            {
                if (!checkBox1.Checked)
                {
                    Bitmap bm = new Bitmap(@"animation\textbox_password.png");
                    pictureBox1.Image = bm;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar imagen de la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    tbPass.UseSystemPasswordChar = false;
                    Bitmap bm = new Bitmap(@"animation\textbox_passwordObserved.jpg");
                    pictureBox1.Image = bm;
                }
                else
                {
                    tbPass.UseSystemPasswordChar = true;
                    Bitmap bm = new Bitmap(@"animation\textbox_password.png");
                    pictureBox1.Image = bm;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar imagen de la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validación básica de entrada
            if (string.IsNullOrWhiteSpace(tbUser.Text) || string.IsNullOrWhiteSpace(tbPass.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario y una contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            user = tbUser.Text;
            pass = tbPass.Text;

            try
            {

                sqlconect con = new sqlconect(user, pass);

                if (con.IniciarSesion(user,pass))
                {
                    // Abrir la ventana principal del sistema como modal
                    using (Form1 mainWindow = new Form1(user, pass))
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
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error de SQL al intentar iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void tbPass_Enter(object sender, EventArgs e)
        {
            password();
        }

        private void tbUser_Enter(object sender, EventArgs e)
        {
            User();
        }

        private void tbPass_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.debut;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Abrir la ventana principal del sistema como modal
            using (Register mainWindow = new Register())
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
    }
}