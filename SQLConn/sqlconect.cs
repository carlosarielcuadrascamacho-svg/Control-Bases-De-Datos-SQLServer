using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;

namespace SQLConn
{
    public class sqlconect
    {
        //Variable para almacenar mensajes de error.
        public String sElError = String.Empty;
        public string user = String.Empty;
        public string password = String.Empty;
        //Constructor por defecto de la clase SQLConexion.
        public sqlconect(string User, string Password)
        {
            user = User;
            password = Password;
        }

        //Método para conectar al servidor SQL y devolver el objeto SqlConnection.
        public SqlConnection ConectarAlServer()
        {
            SqlConnection sqlConnection = null;
            //Cadena de conexión al servidor SQL. Usa autenticación de Windows.
            string sCadenaDeConexion = $"Data Source = localhost; Initial Catalog = Master; User ID = {user}; Password = {password}";
            
            try
            {
                //Crear una nueva conexión con la cadena de conexión especificada.
                sqlConnection = new SqlConnection(sCadenaDeConexion);
                //Abrir la conexión.
                sqlConnection.Open();
                //Cerrar la conexión.
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                //Si ocurre una excepción, almacenar el mensaje de error.
                sElError = ex.Message;
            }
            //Devolver la conexión (si no hubo errores).
            return sqlConnection;
        }

        //Método para obtener una lista de bases de datos en el servidor.
        public Boolean BasesDatosServer(ref DataTable table)
        {
            Boolean bAllOk = false;

            try
            {
                // Utilizar "using" para asegurarse de que la conexión se cierre correctamente.
                using (SqlConnection sqlConnection = ConectarAlServer())
                {
                    // Verificar si la conexión está cerrada y abrirla si es necesario.
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    // Consulta SQL para obtener los nombres de las bases de datos.
                    String sQry = "SELECT name FROM sys.databases";

                    // Crear el comando SQL utilizando la consulta y la conexión.
                    using (SqlCommand cmd = new SqlCommand(sQry, sqlConnection))
                    {
                        // Crear el adaptador de datos para llenar el DataTable con los resultados.
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(table);
                        }
                    }

                    // Si todo fue bien, marcar la operación como exitosa.
                    bAllOk = true;
                }
            }
            catch (SqlException sqlEx)
            {
                // Capturar errores de SQL y propagar el mensaje.
                sElError = $"Error de SQL: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro tipo de excepción y propagar el mensaje.
                sElError = $"Error general: {ex.Message}";
            }

            // Devolver el estado de la operación (si fue exitosa o no).
            return bAllOk;
        }


        // Método para obtener una lista de tablas en una base de datos específica.
        public Boolean TablasDeLaBaseDeDatos(ref DataTable table, string BaseDeDatos)
        {
            Boolean bAllOk = false;

            try
            {
                // Obtener una conexión al servidor SQL.
                SqlConnection sqlConnection = ConectarAlServer();

                // Verificar si la conexión está cerrada y abrirla si es necesario.
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                // Consulta SQL para cambiar a la base de datos especificada y obtener los nombres de las tablas.
                String sQry = $"USE [{BaseDeDatos}]; SELECT name FROM sys.tables";

                // Crear un comando SQL utilizando la consulta y la conexión.
                SqlCommand cmd = new SqlCommand(sQry, sqlConnection);

                // Crear un adaptador de datos para llenar el DataTable con los resultados.
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                // Cerrar la conexión después de realizar la operación.
                sqlConnection.Close();

                // Si no hubo errores, la operación es exitosa.
                bAllOk = true;
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, almacenar el mensaje de error.
                sElError = ex.Message;
            }

            // Devolver el estado de la operación (si hubo errores).
            return bAllOk;
        }


        // Método para obtener información sobre las columnas y constraints de una tabla específica en una base de datos.
        public Boolean InformacionDeLasTablas(ref DataTable table, string BaseDatos, string Tabla)
        {
            Boolean bAllOk = false;

            try
            {
                // Obtener una conexión al servidor SQL.
                SqlConnection sqlConnection = ConectarAlServer();

                // Verificar si la conexión está cerrada y abrirla si es necesario.
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                // Consulta SQL para obtener información de las columnas, su tipo de dato, longitud, si son NULLABLES y los constraints.
                String sQry = $@"
                    USE [{BaseDatos}]; -- Cambiar a la base de datos especificada

                SELECT 
                    C.COLUMN_NAME,                  
                    C.DATA_TYPE,                    
                    C.CHARACTER_MAXIMUM_LENGTH,    
                    C.IS_NULLABLE,                 
                    T.CONSTRAINT_TYPE              
                FROM 
                    INFORMATION_SCHEMA.COLUMNS C
                    LEFT JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU 
                        ON C.COLUMN_NAME = CCU.COLUMN_NAME 
                        AND C.TABLE_NAME = CCU.TABLE_NAME
                    LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS T 
                        ON CCU.CONSTRAINT_NAME = T.CONSTRAINT_NAME
                WHERE 
                    C.TABLE_NAME = '{Tabla}';"; 
        
                // Crear un comando SQL utilizando la consulta y la conexión.
                SqlCommand cmd = new SqlCommand(sQry, sqlConnection);

                // Crear un adaptador de datos para llenar el DataTable con los resultados.
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                // Cerrar la conexión después de realizar la operación.
                sqlConnection.Close();

                // Si no hubo errores, la operación es exitosa.
                bAllOk = true;
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, almacenar el mensaje de error.
                sElError = ex.Message;
            }

            // Devolver el estado de la operación (si hubo errores).
            return bAllOk;
        }


        public Boolean CrearTabla(string nombreTabla, string bd, List<string> campos, List<string> tiposDatos, List<string> longitudes, List<bool> noNulleables, List<string> constraints)
        {
            Boolean bAllOk = false;  // Variable para indicar si la operación fue exitosa o no

            try
            {
                // Construir el query de creación de la tabla usando StringBuilder para construir el SQL dinámicamente
                StringBuilder queryBuilder = new StringBuilder();

                // Agregar la base de datos y el inicio del comando CREATE TABLE
                queryBuilder.AppendLine($"USE {bd}; CREATE TABLE {nombreTabla} (");

                // Lista para almacenar los nombres de las columnas que serán claves primarias
                List<string> primaryKeys = new List<string>();

                int index = 0;

                // Recorrer la lista de campos para construir la definición de cada columna en el query
                foreach (string campo in campos)
                {
                    // Obtener los valores correspondientes de las otras listas usando el índice
                    string tipoDato = tiposDatos[index];  // Tipo de dato (ej. INT, VARCHAR)
                    string longitud = longitudes[index];  // Longitud máxima (si aplica, por ejemplo para VARCHAR)
                    bool noNulleable = noNulleables[index];  // Si la columna acepta valores nulos o no
                    string constraint = constraints[index];  // Restricción (ej. PK, UNIQUE, FK)

                    // Construir la definición de la columna en el query
                    queryBuilder.Append($"{campo} {tipoDato}");

                    // Agregar la longitud de la columna si el tipo de dato requiere longitud
                    if (!string.IsNullOrEmpty(longitud) && tipoDato.ToUpper().Contains("CHAR"))
                    {
                        queryBuilder.Append($"({longitud})");  // Ejemplo: VARCHAR(50)
                    }

                    // Agregar la restricción de NULL o NOT NULL
                    queryBuilder.Append(noNulleable ? " NOT NULL" : " NULL");

                    // Agregar la restricción si aplica
                    if (!string.IsNullOrEmpty(constraint))
                    {
                        switch (constraint.ToUpper())
                        {
                            case "PRIMARY KEY":
                                // Agregar el nombre del campo a la lista de claves primarias
                                primaryKeys.Add(campo);
                                break;
                            case "UNIQUE":
                                // Agregar UNIQUE a la definición de la columna
                                queryBuilder.Append(" UNIQUE");
                                break;
                        }
                    }

                    // Finalizar la definición de la columna y agregar una nueva línea al query
                    queryBuilder.AppendLine(",");

                    // Incrementar el índice para acceder al siguiente valor en las otras listas
                    index++;
                }

                // Agregar la definición de la clave primaria si existe
                if (primaryKeys.Count > 0)
                {
                    // Agregar la restricción PRIMARY KEY al final del query
                    queryBuilder.AppendLine($"PRIMARY KEY ({string.Join(", ", primaryKeys)})");
                }

                // Quitar la última coma que se añadió al final de la lista de columnas
                queryBuilder.Length--;  // Elimina la última coma de la definición de columnas
                queryBuilder.AppendLine(");");  // Cerrar el paréntesis de la definición de la tabla

                // Obtener una conexión al servidor SQL
                SqlConnection sqlConnection = ConectarAlServer();

                // Verificar si la conexión está cerrada y abrirla si es necesario
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                // Crear un comando SQL con el query generado
                SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), sqlConnection);

                // Ejecutar el query SQL (esto crea la tabla en la base de datos)
                cmd.ExecuteNonQuery();  // Ejecuta el query sin devolver resultados

                // Cerrar la conexión a la base de datos después de ejecutar el query
                sqlConnection.Close();

                // Indicar que la operación fue exitosa
                bAllOk = true;
            }
            catch (Exception ex)
            {
                // Almacenar el error en caso de que ocurra una excepción
                sElError = ex.Message;
            }

            // Retornar el estado de la operación (true si fue exitosa, false si hubo un error)
            return bAllOk;
        }

        public Boolean AñadirCampo(string nombreTabla, string bd, List<string> campos, List<string> tiposDatos, List<string> longitudes, List<bool> noNulleables, List<string> constraints)
        {
            Boolean bAllOk = false;

            try
            {
                // Construir el query de adición de campos usando StringBuilder
                StringBuilder queryBuilder = new StringBuilder();

                // Agregar la instrucción USE para especificar la base de datos
                queryBuilder.AppendLine($"USE [{bd}];");  // Usar corchetes para evitar problemas con nombres de base de datos

                int index = 0;  // Índice para acceder a las listas paralelas

                // Recorrer la lista de campos para construir la definición de cada columna en el query
                foreach (string campo in campos)
                {
                    // Obtener los valores correspondientes de las otras listas usando el índice
                    string tipoDato = tiposDatos[index];      // Tipo de dato (ejemplo: INT, VARCHAR)
                    string longitud = longitudes[index];      // Longitud máxima (si aplica)
                    bool noNulleable = noNulleables[index];   // Si la columna acepta NULL o NOT NULL
                    string constraint = constraints[index];   // Tipo de restricción (ejemplo: PRIMARY KEY, UNIQUE)

                    // Construir la definición de la columna en el query
                    queryBuilder.Append($"ALTER TABLE [{nombreTabla}] ADD [{campo}] {tipoDato}");

                    // Agregar la longitud si el tipo de dato lo requiere (por ejemplo, VARCHAR(50))
                    if (!string.IsNullOrEmpty(longitud) && tipoDato.ToUpper().Contains("CHAR"))
                    {
                        queryBuilder.Append($"({longitud})");
                    }

                    // Agregar la restricción de NULL o NOT NULL
                    queryBuilder.Append(noNulleable ? " NOT NULL" : " NULL");

                    // Agregar la restricción adicional si aplica
                    if (!string.IsNullOrEmpty(constraint))
                    {
                        switch (constraint.ToUpper())
                        {
                            case "PRIMARY KEY":
                                queryBuilder.Append(" PRIMARY KEY");  // Agregar PRIMARY KEY a la definición
                                break;
                            case "UNIQUE":
                                queryBuilder.Append(" UNIQUE");       // Agregar UNIQUE a la definición
                                break;
                                // Agregar más casos si se necesitan otros tipos de restricción
                        }
                    }

                    // Finalizar la definición de la columna y agregar una nueva línea al query
                    queryBuilder.AppendLine(";");

                    // Incrementar el índice para acceder al siguiente elemento en las listas
                    index++;
                }

                // Ejecutar el query construido
                using (SqlConnection sqlConnection = ConectarAlServer())
                {
                    sqlConnection.Open();  // Abrir la conexión a la base de datos

                    using (SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), sqlConnection))
                    {
                        cmd.ExecuteNonQuery();  // Ejecutar el comando SQL que añade los campos
                    }
                }

                bAllOk = true;  // Marcar la operación como exitosa
            }
            catch (Exception ex)
            {
                sElError = ex.Message;  // Capturar el mensaje de error en caso de excepción
            }

            return bAllOk;  // Devolver el estado de la operación
        }
        public Boolean EliminarCampo ( string bd,string tb, string camp)
        {
            bool bAllOk = false;
            sElError = string.Empty;

            try
            {
                StringBuilder sQry = new StringBuilder();
                // Agregar la base de datos y el inicio del comando CREATE TABLE
                sQry.AppendLine($"USE {bd};");
                SqlConnection sqlConnection = ConectarAlServer();

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                sQry.Append($"ALTER TABLE {tb} DROP COLUMN {camp}");
                SqlCommand cmd = new SqlCommand (sQry.ToString(), sqlConnection);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {

                    cmd.ExecuteNonQuery();

                }
                sqlConnection.Close();
                bAllOk = true;

            }
            catch(Exception ex) 
            {
                sElError = ex.Message;
            }
            return bAllOk;
        }

        SqlConnection conection;
        SqlCommand commands;

        public bool IniciarSesion(string user, string pass)
        {
            bool id = false;
            string conexion = $"Data Source = localhost; Initial Catalog = Master; User ID = {user}; Password = {pass}";
            try
            {
                conection = new SqlConnection(conexion);
                conection.Open();
                id = true;
            }
            catch (SqlException ex)
            {
                id = false;
            }
            catch (InvalidOperationException ex)
            {
                id = false;
            }
            catch (Exception ex)
            {
                id = false;
            }
            finally
            {
                if (conection.State == ConnectionState.Open)
                    conection.Close();
            }
            return id;
        }

        public bool CrearNuevoUsuario(string query)
        {
            bool id = false;

            using (SqlConnection connection = new SqlConnection("Data Source = localhost; Initial Catalog = Master; User ID = sa; Password = CarlosAriel")) // Cambia esto por tu cadena de conexión
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    return id = false;
                }
            }
            return id = true;
        }
    }
}
