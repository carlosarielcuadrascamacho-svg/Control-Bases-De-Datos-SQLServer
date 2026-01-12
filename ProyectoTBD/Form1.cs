using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SQLConn;

namespace ProyectoTBD
{
    public partial class Form1 : Form
    {
        int iNumeroDeRenglones = 1;
        string User = String.Empty;
        string Password = String.Empty;
        public Form1(string user, string password)
        {
            InitializeComponent();

            User = user; 
            Password = password;
        }
        
        

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void cbBasesDeDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RecargarElNombreDeLasTablas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Algo salio mal al cargar el nombre de las tablas {ex} ");
            }
        }

        public void RecargarElNombreDeLasTablas()
        {
            try
            {
                // Limpiar los elementos anteriores en el ComboBox de tablas.
                cbTablas.Items.Clear();

                // Crear un nuevo DataTable para almacenar los resultados de la consulta.
                DataTable table = new DataTable();

                // Crear una instancia de la clase SQLConexion para realizar la consulta.
                sqlconect sqlconect = new sqlconect(User, Password);

                // Obtener el nombre de la base de datos seleccionada en el ComboBox.
                string bdSelect = cbBasesDeDatos.SelectedItem.ToString();

                // Limpiar el ComboBox de tablas antes de cargar los nuevos datos.
                cbTablas.Items.Clear();

                // Llamar al método para obtener las tablas de la base de datos seleccionada.
                if (sqlconect.TablasDeLaBaseDeDatos(ref table, bdSelect))
                {
                    // Si la operación es exitosa, recorrer cada fila del DataTable.
                    foreach (DataRow row in table.Rows)
                    {
                        // Agregar el nombre de cada tabla al ComboBox de tablas.
                        cbTablas.Items.Add(row[0].ToString());
                    }
                }
                else
                {
                    // Si la operación falla, mostrar un mensaje de error.
                    MessageBox.Show($"No se pudieron obtener las tablas de la base de datos: {bdSelect}.\nError: {sqlconect.sElError}");
                }
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción durante el proceso, mostrar un mensaje de error.
                MessageBox.Show($"Algo salió mal al seleccionar la base de datos.\nError: {ex.Message}");
            }
        }


        private void cbTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RecargarDataGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Algo salio mal al rellenar la Data Grid View {ex} ");
            }
        }


        public void RecargarDataGrid()
        {
            try
            {
                // Limpiar todas las filas existentes en el DataGridView antes de llenarlo con nuevos datos.
                dataGridView1.Rows.Clear();

                // Obtener la base de datos y la tabla seleccionada en los ComboBoxes.
                string bdSelect = cbBasesDeDatos.Text.ToString();
                string tablaSeleccionada = cbTablas.Text.ToString();

                // Crear un DataTable para almacenar la información de las columnas y constraints de la tabla seleccionada.
                DataTable table = new DataTable();

                // Crear una instancia de la clase SQLConexion para realizar la consulta.
                sqlconect conexion = new sqlconect(User, Password);

                // Llamar al método que obtiene la información de las columnas y constraints de la tabla seleccionada.
                if (conexion.InformacionDeLasTablas(ref table, bdSelect, tablaSeleccionada))
                {
                    // Recorrer cada fila del DataTable para agregarla al DataGridView.
                    foreach (DataRow row in table.Rows)
                    {
                        // Variables para almacenar los valores de cada columna.
                        string longitudMaxima = "N/A";
                        string constraint = "NINGUNO";

                        // Verificar si la longitud máxima de la columna no es DBNull y asignarla a la variable.
                        if (row["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
                        {
                            longitudMaxima = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        }

                        // Verificar si el tipo de constraint no es DBNull y asignarlo a la variable.
                        if (row["CONSTRAINT_TYPE"] != DBNull.Value)
                        {
                            constraint = row["CONSTRAINT_TYPE"].ToString();
                        }

                        // Añadir una nueva fila al DataGridView con los datos de la columna.
                        dataGridView1.Rows.Add(
                            row["COLUMN_NAME"].ToString(),  // Campo (Nombre de la columna)
                            row["DATA_TYPE"].ToString(),    // TDato (Tipo de dato)
                            longitudMaxima,                 // Longitud Maxima
                            row["IS_NULLABLE"].ToString() == "NO", // Nulos (CheckBox) - "YES" indica que puede ser NULL
                            constraint                      // Constraint
                        );
                    }
                }
                else
                {
                    // Mostrar un mensaje de error si la obtención de la información falla.
                    MessageBox.Show($"Error al obtener la información de la tabla: {tablaSeleccionada}.\nError: {conexion.sElError}");
                }

                // Actualizar el número de renglones en el DataGridView.
                iNumeroDeRenglones = dataGridView1.Rows.Count;
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error en caso de que ocurra una excepción durante el proceso.
                MessageBox.Show($"Algo salió mal al seleccionar la tabla.\nError: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void CrearNuevaTabla_Click(object sender, EventArgs e)
        {
            // Al dar click en este botón se creará una nueva tabla dentro de la base de datos seleccionada
            // Utilizará las columnas rellenadas en el DataGridView
            try
            {
                // Obtener el nombre de la nueva tabla desde el ComboBox
                string nombreTabla = cbTablas.Text;
                string bd = cbBasesDeDatos.Text;

                // Verificar si el nombre de la tabla no está vacío
                if (string.IsNullOrEmpty(nombreTabla))
                {
                    // Mostrar un mensaje si no se ha ingresado un nombre para la tabla
                    MessageBox.Show("Por favor, ingresa un nombre para la tabla.");
                    return;
                }

                // Recopilar los datos del DataGridView
                List<string> campos = new List<string>();
                List<string> tiposDatos = new List<string>();
                List<string> longitudes = new List<string>();
                List<bool> noNulleables = new List<bool>();
                List<string> constraints = new List<string>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;  // Omitir la fila nueva del DataGridView

                    // Obtener los valores de cada celda
                    string campo = row.Cells["Campo"].Value?.ToString();
                    string tipoDato = row.Cells["TDato"].Value?.ToString();
                    string longitud = row.Cells["LMax"].Value?.ToString();
                    bool noNulleable = Convert.ToBoolean(row.Cells["NoNull"].Value);
                    string constraint = row.Cells["Const"].Value?.ToString();

                    // Validar si los datos están completos
                    if (string.IsNullOrEmpty(campo) || string.IsNullOrEmpty(tipoDato))
                    {
                        // Mostrar un mensaje si algún dato requerido está vacío
                        MessageBox.Show("Por favor, completa todos los campos antes de crear la tabla.");
                        return;
                    }

                    // Añadir los datos a las listas
                    campos.Add(campo);
                    tiposDatos.Add(tipoDato);
                    longitudes.Add(longitud);
                    noNulleables.Add(noNulleable);
                    constraints.Add(constraint);
                }

                // Enviar los datos a la biblioteca para crear la tabla
                sqlconect conexion = new sqlconect(User, Password);
                if (conexion.CrearTabla(nombreTabla, bd, campos, tiposDatos, longitudes, noNulleables, constraints))
                {
                    // Mostrar mensaje si la tabla fue creada exitosamente
                    MessageBox.Show("Tabla creada exitosamente.");
                }
                else
                {
                    // Mostrar mensaje de error si no se pudo crear la tabla
                    MessageBox.Show($"Error al crear la tabla: {conexion.sElError}");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Algo salio mal al crear la tabla \n {ex}");

            }
            RecargarElNombreDeLasTablas();
        }


        private void AgregarNuevoCampo_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el nombre de la nueva tabla desde el ComboBox
                string nombreTabla = cbTablas.Text;
                string bd = cbBasesDeDatos.Text;
                int i = 0;
                // Listas para almacenar los datos de los campos
                List<string> campos = new List<string>();
                List<string> tiposDatos = new List<string>();
                List<string> longitudes = new List<string>();
                List<bool> noNulleables = new List<bool>();
                List<string> constraints = new List<string>();

                // Recopilar los datos del DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;  // Omitir la fila nueva del DataGridView
                    if (i >= iNumeroDeRenglones-1)
                    {
                        // Obtener los valores de cada celda
                        string campo = row.Cells["Campo"].Value?.ToString();
                        string tipoDato = row.Cells["TDato"].Value?.ToString();
                        string longitud = row.Cells["LMax"].Value?.ToString();
                        bool noNulleable = Convert.ToBoolean(row.Cells["NoNull"].Value);
                        string constraint = row.Cells["Const"].Value?.ToString();

                        // Validar si los datos están completos
                        if (string.IsNullOrEmpty(campo) || string.IsNullOrEmpty(tipoDato))
                        {
                            MessageBox.Show("Por favor, completa todos los campos antes de agregar el campo.");
                            return;
                        }

                        // Añadir los datos a las listas
                        campos.Add(campo);
                        tiposDatos.Add(tipoDato);
                        longitudes.Add(longitud);
                        noNulleables.Add(noNulleable);
                        constraints.Add(constraint);
                    }
                    i++;
                }
                // Enviar los datos a la biblioteca para agregar el nuevo campo
                sqlconect conexion = new sqlconect(User, Password);
                if (conexion.AñadirCampo(nombreTabla, bd, campos, tiposDatos, longitudes, noNulleables, constraints))
                {
                    MessageBox.Show("Campo agregado exitosamente.");
                }
                else
                {
                    MessageBox.Show($"Error al agregar el campo: {conexion.sElError}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Algo salió mal al agregar un nuevo campo a la tabla: {ex.Message}");
            }

            RecargarDataGrid();
        }

    private void EliminarCampo_Click(object sender, EventArgs e)
        {
            string sTablaSeleccionada = cbTablas.Text;
            string sBDSeleccionada = cbBasesDeDatos.Text;
            try
            {
                if (sBDSeleccionada == string.Empty)
                {

                    MessageBox.Show("Seleccione una Base de Datos");

                }
                else if (sTablaSeleccionada == string.Empty)
                {

                    MessageBox.Show("Seleccione una tabla");

                }
                else if (dataGridView1.SelectedRows.Count == 0)
                {

                    MessageBox.Show("Seleccione un renglon completo por favor");

                }
                else
                {

                    string ncamp = dataGridView1.CurrentCell.Value?.ToString();

                    sqlconect con = new sqlconect(User, Password);

                    if (con.EliminarCampo(sBDSeleccionada, sTablaSeleccionada, ncamp))
                    {
                        MessageBox.Show($"El campo {ncamp} a sido eliminado");
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                    else
                    {
                        MessageBox.Show($"Ocurrio un problema y no se pudo eliminar el campo \n {con.sElError}");
                    }
                }
            }
            catch( Exception ex ) 
            {
                MessageBox.Show($"Algo salio mal al eleminiar un camoo de la tabla \n {ex}");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                // Limpiar el ComboBox antes de intentar rellenarlo.
                cbBasesDeDatos.Items.Clear();

                // Crear una tabla para recibir los datos de la consulta.
                DataTable tabla = new DataTable();

                // Crear una instancia de la clase SQLConexion.
                sqlconect sQLConexion = new sqlconect(User, Password);

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
    }
}
