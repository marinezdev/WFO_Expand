using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;
using f = WFO.Funciones;

namespace WFO.AccesoDatos.Sistema
{
    public class Usuarios
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Usuarios> SeleccionarTodo()
        {
            b.ExecuteCommandSP("Usuarios_Seleccionar_Roles");
            List<prop.Usuarios> resultado = new List<prop.Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Usuarios item = new prop.Usuarios()
                {
                    IdUsuario = f.Numeros.ConvertirTextoANumeroEntero(reader["IdUsuario"].ToString()),
                    Clave = reader["Clave"].ToString(),
                    FechaRegistro = reader["FechaRegistro"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    RolNombre = reader["RolNombre"].ToString(),
                    Activo = bool.Parse(reader["Activo"].ToString())

                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Usuarios SeleccionarPorId(int id)
        {
            b.ExecuteCommandSP("Usuarios_Seleccionar_PorId");
            b.AddParameter("@Idusuario", id, SqlDbType.Int);
            prop.Usuarios resultado = new prop.Usuarios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdUsuario = f.Numeros.ConvertirTextoANumeroEntero(reader["IdUsuario"].ToString());
                resultado.Clave = reader["Clave"].ToString();
                resultado.FechaCambioClave = reader["FechaCambioClave"].ToString();
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.IdRol = f.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString());
                resultado.Correo = reader["Correo"].ToString();
                resultado.Activo = bool.Parse(reader["Activo"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Usuarios SeleccionarDetalle(string clave, string contra)
        {
            b.ExecuteCommandSP("Usuarios_Seleccionar_Detalle");
            b.AddParameter("@clave", clave, SqlDbType.VarChar, 50);
            b.AddParameter("@contrasena", Seguridad.Cifrado.Encriptar(contra), SqlDbType.VarChar, 50);
            prop.Usuarios resultado = new prop.Usuarios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdUsuario = f.Numeros.ConvertirTextoANumeroEntero(reader["IdUsuario"].ToString());
                resultado.Clave = reader["Clave"].ToString();
                resultado.FechaRegistro = reader["FechaRegistro"].ToString();
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.IdRol = f.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString());
                resultado.Activo = bool.Parse(reader["Activo"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int SeleccionarDiasParaCambioContraseña(string usuario)
        {
            // TODO: ###Pendiente: Hay que corregir que si se vencen los días para el cambio de contraseña ya no se puede acceder al sistema
            string consulta = "SELECT DATEDIFF(DAY, GETDATE(), FechaCambioClave) FROM usuarios WHERE idusuario=@usuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        public int SeleccionarDiasQuedanAvisoCambioContraseña(string usuario)
        {
            string consulta = "SELECT DATEDIFF(DAY, GETDATE(),(SELECT fechacambioclave FROM usuarios WHERE IdUsuario=@usuario)) AS Calculo FROM Configuracion WHERE id=3";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        public int Agregar(prop.Usuarios usuarios)
        {
            b.ExecuteCommandSP("Usuarios_Agregar");
            b.AddParameter("@nombre", usuarios.Nombre, SqlDbType.VarChar, 50);
            b.AddParameter("@clave", usuarios.Clave, SqlDbType.VarChar, 30);
            b.AddParameter("@contrasena", Seguridad.Cifrado.Encriptar(usuarios.Contraseña), SqlDbType.VarChar, 50);
            b.AddParameter("@idrol", usuarios.IdRol, SqlDbType.Int);
            b.AddParameter("@fechacambioclave", f.Fechas.ConvertirTextoAFecha(usuarios.FechaCambioClave), SqlDbType.DateTime);
            b.AddParameter("@correo", usuarios.Correo, SqlDbType.VarChar, 150);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public void AgregarUsuarioOperacion(string nombre, string correo, string clave)
        {
            b.ExecuteCommandSP("Usuarios_Agregar_Operacion");
            b.AddParameter("@nombre", nombre, SqlDbType.VarChar, 150);
            b.AddParameter("@correo", correo, SqlDbType.VarChar, 150);
            b.AddParameter("@clave", clave, SqlDbType.VarChar, 20);
            b.InsertUpdateDelete();
        }

        public void AgregarUsuarioPromotoria(string clavepromotoria, string nombre, string correo, string clave)
        {
            b.ExecuteCommandSP("Usuarios_Agregar_Promotoria");
            b.AddParameter("@clavepromotoria", clavepromotoria, SqlDbType.VarChar, 10);
            b.AddParameter("@nombre", nombre, SqlDbType.VarChar, 50);
            b.AddParameter("@correo", correo, SqlDbType.VarChar, 150);
            b.AddParameter("@clave", clave, SqlDbType.VarChar, 30);
            b.InsertUpdateDelete();
        }

        public void AgregarUsuarioSuper(string nombre, string correo, string clave)
        {
            b.ExecuteCommandSP("Usuarios_Agregar_Super");
            b.AddParameter("@nombre", nombre, SqlDbType.VarChar, 50);
            b.AddParameter("@correo", correo, SqlDbType.VarChar, 150);
            b.AddParameter("@clave", clave, SqlDbType.VarChar, 30);
            b.InsertUpdateDelete();
        }

        public int Modificar(prop.Usuarios usuarios)
        {
            b.ExecuteCommandSP("Usuarios_Actualizar");
            b.AddParameter("@idusuario", usuarios.IdUsuario, SqlDbType.Int);
            b.AddParameter("@nombre", usuarios.Nombre, SqlDbType.VarChar, 50);
            b.AddParameter("@clave", usuarios.Clave, SqlDbType.VarChar, 30);
            b.AddParameter("@fechacambioclave", f.Fechas.PrepararFechaParaAgregar(usuarios.FechaCambioClave), SqlDbType.DateTime);
            b.AddParameter("@idrol", usuarios.IdRol, SqlDbType.Int);
            b.AddParameter("@correo", usuarios.Correo, SqlDbType.VarChar, 150);
            b.AddParameter("@activo", usuarios.Activo, SqlDbType.Bit);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int ModificaConectarSesion(int id, int estado)
        {
            ManejoDatos bb = new ManejoDatos();
            bb.ExecuteCommandSP("Usuarios_Actualizar_Conectar");
            bb.AddParameter("@id", id, SqlDbType.Int);
            bb.AddParameter("@conectado", estado, SqlDbType.Bit);
            return bb.InsertUpdateDelete();
        }
       public int ModificaLogSesion(string IdSesion, string IdUsuario, string InicioSesion, int Modo)
        {
            ManejoDatos bb = new ManejoDatos();
            bb.ExecuteCommandSP("Usuarios_Modificar_LogUsuarios");
            bb.AddParameter("@IdSesion", IdSesion, SqlDbType.VarChar);
            bb.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            bb.AddParameter("@InicioSesion", InicioSesion, SqlDbType.DateTime);
            bb.AddParameter("@Modo", Modo, SqlDbType.Int);
            return bb.InsertUpdateDelete();

        }

        public int ModificaDesconectarSesion(int id, int estado)
        {
            b.ExecuteCommandSP("Usuarios_Actualizar_Desconectar");
            b.AddParameter("@id", id, SqlDbType.Int);
            b.AddParameter("@conectado", estado, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        public int ModificarContraseña(int id, string contrasena)
        {
            b.ExecuteCommandSP("Usuarios_Actualizar_Contraseña");
            b.AddParameter("@idusuario", id, SqlDbType.Int);
            b.AddParameter("@contrasena", Seguridad.Cifrado.Encriptar(contrasena), SqlDbType.VarChar, 50);
            return b.InsertUpdateDelete();
        }

        public bool Validar(string clave, string contraseña)
        {
            b.ExecuteCommandSP("Usuarios_Seleccionar_Validar");
            b.AddParameter("@clave", clave, SqlDbType.VarChar, 50);
            b.AddParameter("@contra", Seguridad.Cifrado.Encriptar(contraseña), SqlDbType.VarChar, 50);
            if (b.Select().Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
