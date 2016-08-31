package ByDan.Framework.Business.Entities;
import ByDan.Asamblea.Business.Logic.*;
import java.sql.Connection;
import java.sql.DriverManager;
import com.mysql.jdbc.*;

public class Test {
	/**
	 * @param args
	 */

	/*
	public static void TraerUsuarios(Connexion con) 
	{
		UsuarioDataAccess usuarioDataAccess=new UsuarioDataAccess();
		ArrayList<Usuario> usuarios=new ArrayList<Usuario>();
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters("");
		queryWhereSelectParameters.setIsAll(true);
		usuarios=usuarioDataAccess.GetEntities(con,queryWhereSelectParameters);
		
		for(Usuario u:usuarios)
		{
			System.out.println(u.getNombre());
			System.out.println(u.getClave());
		}
	}
	public static void TraerUsuariosLimit() 
	{
		Integer inicioPaginacion =1;
		Integer finPaginacion =3;
		UsuarioLogic usuarioLogic=new UsuarioLogic();
		QueryWhereSelectParameters queryWhereSelectParameters= new QueryWhereSelectParameters();
		queryWhereSelectParameters.setFinalQuery(" LIMIT "+inicioPaginacion.toString()+","+finPaginacion.toString() );
		usuarioLogic.GetEntities(queryWhereSelectParameters); 
		
		
		for(Usuario u:usuarioLogic.getUsuarios())
		{
			System.out.println(u.getNombre());
			System.out.println(u.getClave());
			System.out.println(u.getEsAdministrador().toString());
		}
			
		
	}
	public static void TraerUsuariosXml() 
	{
		UsuarioLogic usuarioLogic=new UsuarioLogic();
		usuarioLogic.TraerTodosUsuarios();
		
		
			System.out.println(usuarioLogic.toXmlUsuarios());
		
	}
	public static void TraerUsuarios(Connexion con,String strName1) 
	{
		UsuarioDataAccess usuarioDataAccess=new UsuarioDataAccess();
		ArrayList<Usuario> usuarios=new ArrayList<Usuario>();
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters(ParameterDbType.MYSQL,"");
		ParameterSelectionGeneral parameterSelectionGeneral= new ParameterSelectionGeneral();
		parameterSelectionGeneral.setParameterSelectionGeneralEqual(ParameterType.STRING,strName1, Usuario.getColumnNameNombre(), ParameterTypeOperator.NONE);
		queryWhereSelectParameters.addParameter(parameterSelectionGeneral);
		usuarios=usuarioDataAccess.GetEntities(con,queryWhereSelectParameters);
		
		for(Usuario u:usuarios)
		{
			System.out.println(u.getNombre());
			System.out.println(u.getClave());
		}
	}
	public static void TraerUsuarios(Connexion con,String strName1,String strName2) 
	{
		UsuarioDataAccess usuarioDataAccess=new UsuarioDataAccess();
		ArrayList<Usuario> usuarios=new ArrayList<Usuario>();
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters(ParameterDbType.MYSQL,"");
		ParameterSelectionGeneral parameterSelectionGeneral= new ParameterSelectionGeneral();
		parameterSelectionGeneral.setParameterSelectionGeneral(ParameterType.STRING, Usuario.getColumnNameNombre(), ParameterTypeSign.LIKE, "Byron", ParameterTypeSign.LIKE, "NewNombre",ParameterTypeOperator.OR);
		queryWhereSelectParameters.addParameter(parameterSelectionGeneral);
		
		usuarios=usuarioDataAccess.GetEntities(con,queryWhereSelectParameters);
		
		for(Usuario u:usuarios)
		{
			System.out.println(u.getNombre());
			System.out.println(u.getClave());
		}
	}
	public static void InsertarUsuario(Connexion con) 
	{
		Usuario usuario=new Usuario();
		usuario.setClave("NewClave");
		usuario.setNombre("NewName");
		
		ArrayList<ParameterMaintenance> parameters=new ArrayList<ParameterMaintenance>();
		
		ParameterMaintenance parameterMaintenance;
		parameterMaintenance=new ParameterMaintenance();
		parameterMaintenance.setOrder(1);
		parameterMaintenance.setParameterMaintenanceType(ParameterType.STRING);
		ParameterValue<String> parameterMaintenanceValueNombre=new ParameterValue<String>();
		parameterMaintenance.setParameterMaintenanceValue(parameterMaintenanceValueNombre);
		parameterMaintenance.setValue("NewNombre");
		
		parameters.add(parameterMaintenance);
		
		parameterMaintenance=new ParameterMaintenance();
		parameterMaintenance.setOrder(2);
		parameterMaintenance.setParameterMaintenanceType(ParameterType.STRING);
		ParameterValue<String> parameterMaintenanceValueClave=new ParameterValue<String>();
		parameterMaintenance.setParameterMaintenanceValue(parameterMaintenanceValueClave);
		parameterMaintenance.setValue("NewClave");
		
		parameters.add(parameterMaintenance);
		
		UsuarioDataAccess.Save(usuario,  con);
		
		
	}
	public static void EliminarUsuario(Usuario usuario,Connexion con) 
	{
		usuario.setIsDeleted(true);
		UsuarioDataAccess.Save(usuario,  con);
		
		
	}
	public static void ActualizarUsuario(Connexion con,Usuario usuario) 
	{
		usuario.setClave("jfm5jfm5");
		usuario.setNombre("Byron");
		UsuarioDataAccess.Save(usuario, con);
			
	}
	
	public static void Traer() 
	{
		 try {
			 Connexion con= new Connexion("jdbc:odbc:MySqlOdbcTest","schematest","","",ParameterDbType.MYSQL);
				
		UsuarioDataAccess usuarioDataAccess=new UsuarioDataAccess();
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters("");
		
		ArrayList<Usuario> usuarios=new ArrayList<Usuario>();
		
		
		usuarios=usuarioDataAccess.GetEntities(con,queryWhereSelectParameters);
		
		for(Usuario u:usuarios)
		{
			System.out.print(u.getNombre());
		}
		 }
		 catch(Exception ex)
		 {
			 
		 }
	}
	
	public static void  TraerUsuarios() 
	{
		ArrayList<Usuario> listaUsuariosLocal=new ArrayList<Usuario>();
		
		 try {
		
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters("");
		UsuarioLogic usuarioLogic=new UsuarioLogic();
		
		listaUsuariosLocal=usuarioLogic.GetEntities(queryWhereSelectParameters);
		
		for(Usuario u:listaUsuariosLocal)
		{
			System.out.println();
			System.out.print(u.getId()+" "+u.getNombre()+" "+ u.getClave()+" "+ u.getEsAdministrador().toString());
		}
		
		 }
		 catch(Exception ex)
		 {
			 
		 }
			
			
	}
	
	public static void  TraerTodosUsuarios() 
	{
		ArrayList<Usuario> listaUsuariosLocal=new ArrayList<Usuario>();
		ArrayList<Usuario> listaUsuariosLocal2=new ArrayList<Usuario>();
		
		 try {
		
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters("");
		UsuarioLogic usuarioLogic=new UsuarioLogic();
		
		usuarioLogic.TraerTodosUsuarios();
		listaUsuariosLocal=usuarioLogic.getUsuarios();
		listaUsuariosLocal2=usuarioLogic.getUsuarios();
		
		System.out.print(listaUsuariosLocal.size());
		for(Usuario u:listaUsuariosLocal)
		{
			System.out.println();
			System.out.print(u.getId()+" "+u.getNombre()+" "+ u.getClave()+" "+ u.getEsAdministrador().toString());
		}
		System.out.println();
		TreeWidgetsExt tree=new TreeWidgetsExt();
		
		System.out.print(DojoAccordion.TraerUsuarios(listaUsuariosLocal));
		 }
		 catch(Exception ex)
		 {
			 
		 }
			
			
	}
	
	
	public static void  TraerUsuario(int id) 
	{
		ArrayList<Usuario> listaUsuariosLocal=new ArrayList<Usuario>();
		
		 try {
		
		QueryWhereSelectParameters queryWhereSelectParameters=new QueryWhereSelectParameters("");
		UsuarioLogic usuarioLogic=new UsuarioLogic();
		
		Usuario usuario=new Usuario();
		usuario=usuarioLogic.GetEntity(id);
		
		
			System.out.println();
			System.out.print(usuario.getId()+" "+usuario.getNombre()+" "+ usuario.getClave());
		
		
		 }
		 catch(Exception ex)
		 {
			 
		 }
			
			
	}
	
	public static void  NuevoUsuario(String strNombre,String strClave) 
	{
		
		 try {
		
			UsuarioLogic usuarioLogic=new UsuarioLogic();
		
		
		usuarioLogic.Nuevo(strNombre, strClave);
			
		
		 }
		 catch(Exception ex)
		 {
			 
		 }
			
			
	}
	public static void  ActualizarUsuario(Integer id,String strNombre,String strClave) 
	{
		
		 try {
		
			UsuarioLogic usuarioLogic=new UsuarioLogic();
		
		
		usuarioLogic.Actualizar(id, strNombre, strClave);
			
		
		 }
		 catch(Exception ex)
		 {
			 
		 }
			
			
	}
	
	public static void  EliminarUsuario(Integer id) 
	{
		
		 try {
		
			UsuarioLogic usuarioLogic=new UsuarioLogic();
		
		
		usuarioLogic.Eliminar(id);
			
		
		 }
		 catch(Exception ex)
		 {
			 
		 }
			
			
	}
	*/
	public static void main(String[] args) {
		
		 try {
			 System.out.println("Ok");	
			 //String comandos[]={"cd C:\\Program Files\\MySQL\\MySQL Server 6.0\\bin","mysqldump -u root -p --databases asambleavirtual_dbo > \"C:\\Documents and Settings\\Personal\\Desktop\\Activismo Independiente\\Recursos\\Backups\\aa.sql\""};
			 
			 //String comandos[]={"C:\\Program Files\\MySQL\\MySQL Server 6.0\\bin\\mysqldump -u root -p --databases asambleavirtual_dbo > \"C:\\Documents and Settings\\Personal\\Desktop\\Activismo Independiente\\Recursos\\Backups\\aa.sql\""};
			
			 //String comandos[]={"cmd","cd C:\\Program Files\\MySQL\\MySQL Server 6.0\\bin","mysqldump -u root -p --databases asambleavirtual_dbo > \"C:\\Documents and Settings\\Personal\\Desktop\\Activismo Independiente\\Recursos\\Backups\\aa.sql\""};
			
			 //String comandos[]={"cmd"};
			 	
			 //Process p= Runtime.getRuntime().exec(comandos);
			 //System.out.println(p.toString());
			 //System.out.println(p.exitValue());
			 //Connexion con= new Connexion("jdbc:odbc:MySqlOdbcTest","schematest","","",ParameterDbType.MYSQL);
		
		TemaLogic temaLogic= new TemaLogic();
		Driver driverMySql= new Driver();
		DriverManager.registerDriver(driverMySql);
		
		
		 String xml="";
		 Connection conn =  DriverManager.getConnection("jdbc:mysql://localhost/asambleavirtual_dbo?" +  "user=root&password=root105");
		 
		//if(temaLogic.ProcesarActualizacionDeTemas("ArchivoUsuarioEjemploModificado.txt"))
		//{
		//	System.out.println("Procesado");
		//}
		//else
			//{
			 //temaLogic.TraerTemasBusquedaPorEsTemaPrincipal("", true);
			 //System.out.println(temaLogic.GenerarArbolTemas(temaLogic.getTemas()));
			//}	
		//paginaLogic.TraerTodosPaginas("");
		//temaLogic.TraerTodosTemas("");
		//String strPermisos=temaLogic.toXmlTemas("");//paginaLogic.TraerMenuPaginas(paginaLogic.getPaginas());
		//System.out.println(strPermisos);
		
		
		/*UsuarioRolLogic usuarioRolLogic= new UsuarioRolLogic();
		
		
		String  finalQueryPaginacion="";
		Integer inicioPaginacion;
		Integer finPaginacion;
			
		
			inicioPaginacion=0;	
			finPaginacion=10;	
					
			finalQueryPaginacion=" LIMIT "+inicioPaginacion.toString()+","+finPaginacion.toString();
		
		
		usuarioRolLogic.TraerUsuarioRolsBusquedaPorIdUsuario(finalQueryPaginacion, 1);
		System.out.println(usuarioRolLogic.toXmlUsuarioRols(""));
		
		UsuarioLogic usuarioLogic= new UsuarioLogic();
		String xml="";
		String strPermisos=usuarioLogic.VerificarPermisosAccionesMantenimiento(1,"MantenimientoParametroGeneral.jsp");
		System.out.println(strPermisos);
		
		PaginaLogic paginaLogic= new PaginaLogic();
		String xml="";
		//paginaLogic.TraerTodosPaginas("");
		paginaLogic.TraerTodosPaginas("");
		String strPermisos=paginaLogic.GenerarMenu(1);//paginaLogic.TraerMenuPaginas(paginaLogic.getPaginas());
		System.out.println(strPermisos);
	*/
		/*PaginaLogic paginaLogic= new PaginaLogic();
		String xml="";
		String strPermisos=paginaLogic.TraerTodosPaginas("");
		System.out.println(paginaLogic.toXmlPaginas(""));*/
		/*if(usuarioLogic.ValidarUsuarioLogin("byron", "danilo"))
		{
			System.out.println("Validado");		
		}
		else
		{
			System.out.println("No Validado");	
		}
		
		TipousuarioLogic tipousuarioLogic= new TipousuarioLogic();
		tipousuarioLogic.Actualizar(33,"2008-01-09 08:32:08","TEST4");
		
		bool x= true;				
		UsuarioLogic usuarioLogic= new UsuarioLogic();
		usuarioLogic.Actualizar(1,"2008-01-14 17:25:04",1, "new","newc",true,"2008/01/14",4,4.4);
		
		System.out.println(usuarioLogic.getUsuarios().size());
		System.out.println(usuarioLogic.toXmlUsuarios(""));
		*/
		/*TipoRecursoLogic tipoRecursoLogic= new TipoRecursoLogic();
		String xml="";
		tipoRecursoLogic.TraerTipoRecursoPorTipo("b");
		
		if(tipoRecursoLogic.getTipoRecurso().getId()!=0)
		{
			xml =tipoRecursoLogic.toXmlTipoRecurso("");			
		}
		else
		{
			xml =Mensaje.getMensajeGeneralNoExisteBusqueda("TipoRecurso");
		}
		
		System.out.println(xml);
		*/
		/*
		tipousuarioLogic.TraerTodosTipousuarios("");
		
		ArrayList<Class> classes=new ArrayList<Class>();
		classes.add(Usuario.class);
		  
		tipousuarioLogic.DeepLoad(tipousuarioLogic.getTipousuarios(), false, DeepLoadType.INCLUDE, classes);
		
		for(Tipousuario tipoUsuario:tipousuarioLogic.getTipousuarios())
		{
			
			for(Usuario usuario:tipoUsuario.getTR_usuarios())
			{
				usuario.setP_strNombre(usuario.getP_strNombre()+"DeppSave");
			}
			tipousuarioLogic.DeepSave(tipoUsuario, false, DeepLoadType.INCLUDE, classes);
		}
		
		
		System.out.println(tipousuarioLogic.getTipousuarios().size());
		System.out.println(tipousuarioLogic.toXmlTipousuarios());
		*/
		//tipousuarioLogic.Actualizar(6,"TEST4");
		// TODO Auto-generated method stub
		// int i=0;
		//while( i<10){
		// InsertarUsuario(con);
		/* i++;
		}*/
		 
		//TraerUsuarios(con);
		//TraerUsuarios(con,"Byron"); 
		//Usuario usuario;
	     //UsuarioDataAccess usuarioDataAccess=new UsuarioDataAccess();		
	     //usuario=usuarioDataAccess.GetEntity(con, 3);
	     //usuario.setEsAdministrador(null);
	     //ActualizarUsuario(con,usuario);
		//EliminarUsuario(usuario,con);
		
		// TraerUsuarios(con);
		// Traer();
		//EliminarUsuario(12);
		//NuevoUsuario("mau","mao");
		//ActualizarUsuario(2,"mau","mau");	
		
		
		//TraerUsuarios();
		//TraerUsuariosXml();
		//TraerUsuariosLimit();
			 //con.getConnection().close();
		
			
			 
		//ProcesarClasesHtml() ;
		
		 }
		 catch(Exception ex)
		 {
			 String strMensaje =ex.getMessage();
			
			 System.out.println("Exceeption:"+strMensaje);
		 }
		 
		 
		 }
	
	
	
	
	
		 }
	
	


