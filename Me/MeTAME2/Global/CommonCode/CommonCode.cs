/*

DESCRIPCION DE TABLA:
TIPOS:
|NOMBRE=(Nombre de la tabla de la bd).
|ESROMPIMIENTO=(Indica si la tabla es de rompimiento, solo funciona en relaciones simples true,false).
|WEBTITULO=(Titulo de los combos y columnas q hacen referencia a esta tabla).
|WEBPLURAL=(La finalizacion del nombre en plural)				s
|WEBRELACIONESNO=(El nombre de la tabla o tablas no tomadas en cuenta para proceso de mantenimiento relacionado ).
|DELCASCADE=(Determina si puede borrar o no en cascada)	|								true,false 	  (por defecto sera false)
|CLASESNO=(Las Clases donde false se pueda navegar para mantenimiento)	
|WEBCONATRAS=(Indica si en la pagina actual debe funcionar el boton atras)	true,false
|ESINTERNO=(Indica si la tabla es interna es decir hija de alguna principal y no muestra recargar informacion)	true,false
|INDICESNO=(Los Indices donde false se pueda buscar mantenimiento) se separan con una ,
|INSERTNO=(Indica si la tabla no permite insertar elementos)	true,false
|CONAUD=(Indica si se utiliza auditoria)
|CONSTORE=(Indica si se utiliza Store Procedure)
|CONORIG=(Indica si se utiliza el objeto original)
|FINALQUERY=(Indica si el query que complementa siempre en cada busqueda) -----> Reemplaza () por = cuando sea el caso
|ESREPORTE=(Indica si la tabla es de reporte true,false).
|PREFIJOID=(Prefijo sql del id para la paginacion)
|ESMENU=(Define si la tabla especificada va ser usada para el menu principal)true, false
|CONJAVASCRIPTIE=(Indica si la tabla usa include/exclude javascript true,false).
|PAQUETE=(Indica el path el cual se encuentra guardado la pagina y reporte y usado para generar arbol)Catalogos/DatosPersonales/
|ALIGN=Alinea la paginacion,control nuevo, campos de datos y acciones deacuerdo a los valores:(left,center,rigth)
|TAMBUSQUEDA=indica el tamanio horizontal del bloque de busquedas de la pagina
|ESTABLATAME=Indica si la tabla es de tame, por lo que por defecto no debe tener los campos OID y UTC
|IGNORAR=Indica si la tabla se lo ignora en el proceso o no (true,false)
|SCHEMA=Indica el nombre del esquema de la tabla.


TIPOS PARA ACCION:
|NOMBRE=(Nombre Codigo de la accion de la tabla de la bd).
|WEBTITULO=(Nombre para la pagina web de la accion de la tabla de la bd).
|POSTACCION=(Indica que debe realizarse luego de ejecutarse la accion (Mensaje,Arir otra pagina,Abrir nueva pagina,etc ).
			(1=Sin respuesta,2=IrAPagina,3=IrANuevaPagina,4=ConRetorno)


DESCRIPCION DE LOS CAMPOS:
TIPOS:
|NOMBRE=(Nombre del campo de la tabla de la bd)|
|TABLA= (El nombre de la tabla donde el campo id lo hace referencia en relacion uno a uno)|
|TIPO= (El tipo de campo en detalle Ejm: Date,Time,Timestamp)|
|TIPOSQL= (El tipo de campo en la BD en detalle Ejm: DATE,TIME,TIMESTAMP)|
|WEBTITULO=(es el titulo de cada campo que sera utiilizado como labbel)|
|WEBFILAS=(es el numero de filas que este campo muestra en la pagina web)|
|WEBCOMBO=(Columna detalle para el combo y o tabla)	|								true,false 	  (por defecto se aplicara la columna id)
|WEBORDEN=(Columna detalla el orden inicial de la columna)|							asc,desc  (por defecto no se aplicara ningun orden)
|ESPARAROMPIMIENTO=(Indica si la columna es para rompimiento, solo funciona en relaciones simples true,false).
|CONBUSQUEDA=(Indica si la columna FOREIGN KEY debe utilizar busquedas para seleccionar valores combo actual true,false).
|NOUPPER=(Columna que no obliga ser upper)| true,false	
|JSVALIDACION(La funcion especial para la validacion de cliente o javascript)	
|EDITNO=(Indica si la tabla no permite editar elementos)	true,false
|ESREPORTE=(Indica si el campo es de reporte true,false).


READ ONLY TABLES

DESCRIPCION DE TABLA:
|TIPOS:
|NOMBRE=(Nombre de la tabla de la bd).
|STARTQUERY=(Query de la parte inicial de la consulta)
|ENDQUERY=(Query de la parte inicial de la consulta)

DESCRIPCION DE LOS CAMPOS:
TIPOS:
|NOMBRE=(Nombre del campo de la tabla de la bd)|
|TIPO= (El tipo de campo en detalle Ejm: Date,Time,Timespam)|
|ALIAS=(Alias de la tabla de referencia de la bd)|
|OPERATOR=(Operador del campo)
|STARTOPERATOR=(Operador inicial)
|ENDOPERATOR=(Operador final)
|STARTOTHER=(Query de la parte inicial de la consulta)
|ENDOTHER=(Query de la parte inicial de la consulta)
*/

using CodeSmith.Engine;
using SchemaExplorer;
using System;
using System.Windows.Forms.Design;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Me
{
	
	#region Layers And Auxs
	
	public enum Layers
	{
		DataAccess,
		/// <summary>No Component Pattern Generation should be included.</summary>
		Entities,
		/// <summary>A Service Layer Pattern should be included.</summary>
		Interface,
		/// <summary>A Domain Model Pattern Generation should be included.</summary>
		Logic,
		Servlet,
		Reporte,
		JavaScript,
		JspMaintenance
	}
	
	public enum PaquetesGenerar {
		TODOS,
		DATA_ACCESS,
		ENTITIES,
		INTERFACE,
		LOGIC,
		CONSTANTES_FUNCIONES,
		EJB,
		CONTROLLER,
		WEB,
		REPORTE,
		JAVASCRIP,
		CONFIG
	}		
	
	public  class MeExtendProperty
	{
		private String strName;
		private String strValue;
		
		public  MeExtendProperty()
		{
			strName="";
			strValue="";
		}
		
		public  MeExtendProperty(String strNameParam,String strValueParam)
		{
			strName=strNameParam;
			strValue=strValueParam;
		}
		
		public String Name
		{
			get
			{
				return strName;
			}
			set
			{
				strName = value;
			}
		}
		
		public String Value
		{
			get
			{
				return strValue;
			}
			set
			{
				strValue= value;
			}
		}

	}
	
	#endregion
	
	public  class CommonCode : CodeTemplate
	{
		
		#region Const Acciones Labels
		
		public const String strPrefijoAccionTableExtendProperty="Accion_";
		public const String strPrefijoAccionTableNombreProperty="NOMBRE";
		public const String strPrefijoAccionTableWebNombreProperty="WEBTITULO";
		public const String strPrefijoAccionTablePostAccionProperty="POSTACCION";
		
		#endregion	
		
		#region Tags Descripcions
	
		// TABLE
		
		public static String strNOMBRE="NOMBRE";
		public static String strPREFIJOTABLA="PREFIJOTABLA";
		public static String strESROMPIMIENTO="ESROMPIMIENTO";
		public static String strESGUARDARREL="ESGUARDARREL";
		public static String strESGUARDARRELHIJO="ESGUARDARRELHIJO";		
		public static String strWEBTITULO="WEBTITULO";
		public static String strWEBPLURAL="WEBPLURAL";
		public static String strWEBRELACIONESNO="WEBRELACIONESNO";
		public static String strDELCASCADE="DELCASCADE";
		public static String strCLASESNO="CLASESNO";
		public static String strCLASESPERSISTENCENO="CLASESPERSISTENCENO";
		public static String strWEBCONATRAS="WEBCONATRAS";
		public static String strESINTERNO="ESINTERNO";
		public static String strINDICESNO="INDICESNO";
		public static String strKEYINDICESNO="KEYINDICESNO";
		public static String strINSERTNO="INSERTNO";
		public static String strDELETENO="DELETENO";
		public static String strCONAUDAUTO="CONAUDAUTO";
		public static String strSINAUDET="SINAUDET";
		public static String strCONAUD="CONAUD";
		public static String strCONSTORE="CONSTORE";
		public static String strCONORIG="CONORIG";
		public static String strFINALQUERY="FINALQUERY";
		public static String strSESSIONKEYQUERY="SESSIONKEYQUERY";
		public static String strNEWCODE="NEWCODE";
		public static String strESREPORTE="ESREPORTE";
		public static String strESREPORTEAUX="ESREPORTEAUX";
		public static String strESREPORTEPARAM="ESREPORTEPARAM";
		public static String strESREPORTEGROUP="ESREPORTEGROUP";
		public static String strCONNATIVE="CONNATIVE";
		public static String strPREFIJOID="PREFIJOID";
		public static String strESMENU="ESMENU";
		public static String strCONJAVASCRIPTIE="CONJAVASCRIPTIE";			
		public static String strPAQUETE="PAQUETE";
		public static String strPAQUETEJAVA="PAQUETEJAVA";
		public static String strALIGN="ALIGN";
		public static String strTAMBUSQUEDA="TAMBUSQUEDA";
		public static String strIGNORAR="IGNORAR";
		public static String strSCHEMA="SCHEMA";
		public static String strPCKG="PCKG";
		public static String strPCKGENTCLASES="PCKGENTCLASES";
		public static String strCONPERSISTENCIA="CONPERSISTENCIA";
		public static String strNOEJB="NOEJB";
		public static String strWHAUX="WHAUX";
		public static String strFINALJSPVERSION="FINALJSPVERSION";
		public static String strFINALJSFVERSION="FINALJSFVERSION";
		public static String strESPOPUP="ESPOPUP";
		public static String strNUMCOLUMNAS="NUMCOLUMNAS";
		public static String strNUMCOLUMNASEXTRA="NUMCOLUMNASEXTRA";
		public static String strNUMPAG="NUMPAG";
		public static String strCONPAQUETEJAVAGLOBAL="CONPAQUETEJAVAGLOBAL";	
	
	
		// COLUMNAS
		
		//public static String strNOMBRE="NOMBRE";
		public static String strTABLA="TABLA";
		public static String strTIPO="TIPO";
		//public static String strWEBTITULO="WEBTITULO";
		public static String strWEBFILAS="WEBFILAS";
		public static String strWEBCOMBO="WEBCOMBO";
		public static String strWEBORDEN="WEBORDEN";
		public static String strESPARAROMPIMIENTO="ESPARAROMPIMIENTO";
		public static String strCONBUSQUEDA="CONBUSQUEDA";
		public static String strJSVALIDACION="JSVALIDACION";
		public static String strEDITNO="EDITNO";
		//public static String strESREPORTE="ESREPORTE";
		//public static String strCONAUD="CONAUD";

		#endregion
		
		
		bool blnNuevaVersionJMaki=false;
	
		
		#region Global Variables
		
		public static String strGlobalSeguridadComment="";
		public static String strGlobalAuditoriaComment="";
		public static String strGlobalAuditoriaInternaComment="";
		
		public static String strGlobalSeguridadCommentNo="";
		public static String strGlobalAuditoriaCommentNo="";
		public static String strGlobalAuditoriaInternaCommentNo="";
		
		//GLOBAL VARIABLES TEMPLATE GENERACION
		
		//GlobalConPaqueteJavaGlobal
		public static bool GlobalConPaqueteJavaGlobal=false;
		public static string GlobalConPaqueteJavaGlobalImports="";
		public static string GlobalEmpresa="";
		public static string GlobalPackage="";
		
		//ESTA VARIABLE NO CAMBIA
		public static string GlobalModulePackage="global";
		//GLOBAL VARIABLES TEMPLATE GENERACION
		
		public static String Module="";
		
		
		public static Boolean blnEsLowerCaseDBNames=false;	
		public static Boolean blnEsMixedCaseDBNames=true;
		public static Boolean blnEsMaximoTamanioPopup=false;//TODAS LAS VENTANAS POPUP SON GRANDES
		
		
		public static String strGlobalSeguridadExtensionOpcion=".jsf";
		public static String strGlobalPrefijoDBNombreTablas="";
		public static String strGlobalConexionSchema="";
		
		//SI EXISTE PROBLEMAS Y MEJOR COMENTAR HASTA RESOLVER CUANDO TABLAS SON NO STANDARD
		public static String strGlobalTablaNoStandardComment="";		
		public static Boolean blnConFuncionesSqlNativas=false;
		
		
		//VARIABLES GLOBALES POR TABLA
		public static Boolean blnTieneIdentityColumn=true;
		public static Boolean blnCumpleMaximoRelacionesMantenimiento=true;
		public static Boolean blnTieneBusquedas=false;
		public static Boolean blnTieneImagen=false;
		public static Boolean blnTieneDocumento=false;
		public static Boolean blnTieneSeguridadCampo=false;	
		public static Boolean blnTieneAuditoriaAuto=false;	
		public static Boolean blnTieneTextArea=false;
		public static Boolean blnTieneTimestamp=false;
		public static Boolean blnTieneValidacionTodo=false;
		public static Boolean blnEsReporte=false;
			public static String strCommentReporte="";
		public static Boolean blnNoStandardTableFromProperties=false;
		public static Boolean blnEsTablaUnoAUnoFk=false;
		public static ArrayList arrayListEsquemasRel=null;
			public static String strNombreTablaUnoAUnoPk="";
		public static String strPackageJava="";
			//VARIABLES POR TABLA CCFA
			public static Boolean blnEsTablaLatitudLongitud=false;
			public static String strPorTablaPrefijoNombre="";
			
		//PARA NO ESTANDARD
		public static bool blnTieneTipoPKStandard=false;
		public static String strPorTablaColumnsPKParametros="";
		public static String strPorTablaColumnsPKParametrosSinComaPrimero="";
		public static String strPorTablaColumnsPKParametrosUso="";
		public static String strPorTablaColumnsPKParametrosUsoSinComaPrimero="";
		public static int intPorTablaCountColumnsPKC=0;
		public static ColumnSchemaCollection columnSchemaCollectionPK=null;
		#endregion
		
		#region Packages Names
		
		
		
		public static String strPackageFramework="framework";
		public static String strPackageUtils="util";
				
		public static String strPackageDataAccess="dataaccess";
		public static String strPackageBusiness="business";
		public static String strPackageEntities="entity";
		public static String strPackageBeans="bean";
		public static String strPackageFaces="face";
		public static String strPackageLogic="logic";
		public static String strPackageInterface="interfaces";
		public static String strPackagePresentation="presentation";
		public static String strPackageWeb="web";
		public static String strPackageServlet="servlet";
		public static String strPackageReporte="report";
		public static String strPackageSource="source";
		public static String strPackageSwing="swing";
		public static String strPackageJInternalFrames="jinternalframe";
		public static String strPackageService="service";
		public static String strEjb="ejb";
		public static String strEjbInterface="interfaces";
		public static String strEjbSession="session";
		
		//WEB SERVICES
		public static String strPackageWebServicePrefix="jws";		
		public static String strPackageWebServiceInterface="interfaces";		
		public static String strPackageWebService="webservice";		
		
		
		//IMPORT SEGURIDAD AUDITORIA
		public static String strEmpresaAuditoria="bydan";
		public static String strPackageEntitiesAuditoria="entities";						
		public static String strPackageAuditoria="auditoria";
		
		public static String strEmpresaSeguridad="bydan";
		public static String strPackageEntitiesSeguridad="entities";
		#endregion
		
		/* Me Extend Properties */
			public const String strPrefijoMeTableExtendProperty="Me_";		
		public const String strPrefijoRelativePath="../";
		public const String strPrefijoDataAccess="DataAccess";
		public const String strPrefijoBeanSwing="BeanSwing";
		public const String strPrefijoSwingInternalFrame="JInternalFrame";
		public const String strForeignKey="ForeignKey";
		public String strPrefijoFace="Face";
		public const String strPrefijoJSFFaces="RequestBean";
		public const String strPrefijoJSFSessionFaces="SessionBean";
		public const String strSufijoJSPJSFFinal="Final";
		public const String strSufijoRangoFinal="Final";
		public const String strPrefijoWebService="WebService";
		public const String strPrefijoParametroStoreProcedure="inputParam";
		public const String strClaseDetalleBean="DescripcionReporte";
		public const String strClaseBean="Bean";
		public const String strClaseConstantesFunciones="ConstantesFunciones";
		public const String strClaseAuxiliar="Compuesto";
		public const String strNative="Native";
		public const String strEndScript="</script>";
		public const String strStartPageInclude="<%@";
		public const String strStartPageIncludeOnly="<%";
		public const String strFinishPageInclude="%>";
		public const String strAjaxWebPart="AjaxWebPart";
		
		public const String strId="id";
		public const String strIsActive="isActive";
		public const String strIsExpired="isExpired";
		public const String strVersionRow="versionRow";
		public const String strDescripcion="_descripcion";
		public const String strDescripcionLabel=" DESCRIPCION";
		public const String strfield="";//"field";
		public const String strField="";//"Field";
		public const String strfield_=strfield;//+"_";//"field";
		public const String strField_=strField;//+"_";//"Field";
		
		public const String strFK="FK";
		public const String strPK="PK";
		public const String strIdGetSet="Id";
		public const String strIsActiveGetSet="IsActive";
		public const String strIsExpiredGetSet="IsExpired";
		public const String strVersionRowGetSet="VersionRow";
		public const String strINDICE="INDICE";
		
		public static String strIdDB="id";
		public static String strVersionRowDB="version_row";
		
		public const String strIdRelationshipGetSet="lId";
		public const String strCatalogoGeneralLista="Lista";
		public const String strCatalogoGeneralValor="Valor";
		
		public const int intNumeroMinimoColumnasTablaRompimiento=6;
		
		public const int intDesplazamientoReporteMaestro=15;
		
		public const int intTamColumnaTablaSwing=100;
		
		public const String strValidacion="validacion.";
		
		public const String strSeparadorXml="-";
		
		public const int intTamDetalleRelacionReporte=110;
		
		public const String strServletResponse="Json";
		public const String strServletResponseParameter="sTipoJsonResponse";

		
		#region WebLabels
		
		public const String strColorBusquedaAnidada="#CCCCCC";
		public const String strGenerarReporte="GENERAR REPORTE";
		public const String strGenerarReporteTodos="TODOS";
		public const String strRecargarInformacion="RECARGAR INFORMACION";
		public const String strEdicion="EDITAR";
		public const String strSeleccion="SELECCIONAR";
		public const String strCodigoUnico="CODIGO UNICO";
		public const String strNuevo="NUEVO";
		public const String strActualizar="ACTUALIZAR";
		public const String strEliminar="ELIMINAR";
		public const String strCancelar="CANCELAR";
		public const String strGuardarCambios="GUARDAR CAMBIOS REALIZADOS";
		public const String strMantenimientoDe="MANTENIMIENTO DE ";
		public const String strReporteDe="REPORTE DE ";
		public const String strNumeroDe="NUMERO DE ";
		public const String strAProcesar=" A PROCESAR:";
		public const String strBusquedas="BUSQUEDAS";
		public const String strBusqueda="BUSQUEDA";
		public const String strBuscar="BUSCAR";
		public const String strPor="POR";
		public const String strDe=" DE ";
		public const String strCatalogosSimples="CATALOGOS SIMPLES";
		public const String strCatalogosCompuestos="CATALOGOS COMPUESTOS";
		
		//REPORTES
		public const String strTipoBusqueda="TIPO=";
		public const String strCodigoDe="CODIGO UNICO DE ";
		public const String strParametrosBusqueda="PARAMETROS->";
		#endregion
		
		#region ReadOnly
		
		public const String strPrefijoParametro="PRM_";
		
		public int GetNumeroDeParametrosC(TableSchema tableSchema) 
		{
			int count=0;	
					
					
					foreach(ColumnSchema columnSchema in tableSchema.Columns)
					{
						if(columnSchema.Name.Contains(strPrefijoParametro))
						{
							count++;
						}
					}
			return count;
		}
		
		public int GetNumeroSinParametrosC(TableSchema tableSchema) 
		{
			int count=0;	
					
					
					foreach(ColumnSchema columnSchema in tableSchema.Columns)
					{
						if(!columnSchema.Name.Contains(strPrefijoParametro))
						{
							count++;
						}
					}
			return count;
		}
		#endregion
		
		#region Tipos de Generaciones
		
		//TODOS
		public const String strTipoGeneracionTodos="TODOS";
		//NINGUNO
		//Compuesto
		public const String strTipoGeneracionCompuestos="CS";
		public const String strTipoGeneracionNinguno="NINGUNO";
		//DeletesCascade
		public const String strTipoGeneracionDeletesCascade="DC";
		//MantenimientoClasesRelacionadas
		public const String strTipoGeneracionMantenimientoClasesRelacionadas="MCR";
		//GetXmls
		public const String strTipoGeneracionGetXmls="GX";
		//DeepForeignKey
		public const String strTipoGeneracionDeepForeignKey="DF";
		//DeepRelationship
		public const String strTipoGeneracionDeepRelationship="DR";
		//DeepForeignKeyAndRelatioship
		public const String strTipoGeneracionDeepForeignKeyAndRelatioship="DFR";
		
	
		public static bool GenerarTipoGeneracionC(String strTipoGeneracion,String strTiposGeneraciones) {
			bool blnGenerar=false;
			
			
			foreach(String strTipoGeneracionLocal in strTiposGeneraciones.Split(',')) {
				
				if(strTipoGeneracionLocal.Contains(strTipoGeneracionTodos)) {
						return true;
				}
				
				if(strTipoGeneracionLocal.Contains(strTipoGeneracionNinguno)) {
						return false;
				}
				
				
				
				if(strTipoGeneracion==strTipoGeneracionDeletesCascade) {
					if(strTipoGeneracionLocal.Contains(strTipoGeneracionDeletesCascade)) {
						blnGenerar=true;
						break;
					}
				} else if(strTipoGeneracion==strTipoGeneracionMantenimientoClasesRelacionadas) {
					if(strTipoGeneracionLocal.Contains(strTipoGeneracionMantenimientoClasesRelacionadas)) {
						blnGenerar=true;
						break;
					}
				} else if(strTipoGeneracion==strTipoGeneracionGetXmls) {
					if(strTipoGeneracionLocal.Contains(strTipoGeneracionGetXmls)) {
						blnGenerar=true;
						break;
					}
				} else if(strTipoGeneracion==strTipoGeneracionDeepForeignKey) {
					if(strTipoGeneracionLocal.Contains(strTipoGeneracionDeepForeignKey)) {
						blnGenerar=true;
						break;
					}
				} else if(strTipoGeneracion==strTipoGeneracionDeepRelationship) {
					if(strTipoGeneracionLocal.Contains(strTipoGeneracionDeepRelationship)) {
						blnGenerar=true;
						break;
					}
				} else if(strTipoGeneracion==strTipoGeneracionDeletesCascade) {
					if(strTipoGeneracionLocal.Contains(strTipoGeneracionDeepForeignKeyAndRelatioship)) {
						blnGenerar=true;
						break;
					}
				}
			}
			
			return blnGenerar;
		
		}
		#endregion
		
		#region Persistence
		
		public const String strHqlJoinRelacionnes=" INNER JOIN ";//" JOIN FETCH ";
		public const String strJpaCascadeTypeDefault="";//CascadeType.ALL
		public const String strTipoParaFecha="Date";
		//SET NO ORDENA CON ORDER BY, UN CONTROL EXECIVO CON PRIMARY (LO HACE LA BD Y ES AUTO)
		public const String strTypeCollection="List";//"Set";
		public const String strTypeNewCollection="ArrayList";//"HashSet";
		public const String strTypeNewCollectionEnd="()";//"(0)";
		
		public static string GetPersistenciaColumnaClaseC(ColumnSchema column) {
			string tipoColumna ="";//  GetTipoColumnaFromColumn(column);
			
			if(tipoColumna!="") {
				return tipoColumna;
			}
				
			string param =  column.NativeType;
			string paramType =  column.DataType.ToString();
			
			String strInit="@Column( name =\"";
			String strEnd=" )";
			String strColumnPersistence=strInit+GetNombreColumnFromProperties(column)+"\"";
			
			if(column.AllowDBNull) {
				strColumnPersistence+=", nullable=true";
			} else {
				strColumnPersistence+=", nullable=false";
			}
			
			
			switch (column.DataType) {
				case DbType.Boolean: {
					param =  "Boolean";
					break;					
				} case DbType.Binary: {
					if(column.Name==strVersionRow) {
						param =  "Timestamp";
					} else {
						param =  "byte []";
					}					
					break;				
				} case DbType.DateTime: {
					param =  "String";
					break;					
				}case DbType.Int32:case DbType.UInt32: {
					param =  "Integer";
					break;					
				} case DbType.Int64:case DbType.UInt64: {
					param =  "Long";
					break;				
				} case DbType.Int16:case DbType.UInt16: {
					param =  "Short";
					break;				
				}  case DbType.AnsiString:case DbType.AnsiStringFixedLength:case DbType.StringFixedLength:case DbType.String: {
					strColumnPersistence+=", length = "+column.Size.ToString();
					break;					
				}  case DbType.Decimal:case DbType.Double: {
					param =  "Double";
					break;
				}				
				default: {
					param =  "None:"+paramType ;
					break;
				}		
			}
			
			strColumnPersistence+=strEnd;
			return strColumnPersistence;
		}
			
		#endregion
		
		public static TableSchema GetTableSchemaFromColumnForeignKey(ColumnSchema columnSchema) {		
			TableSchema tableSchema=null;
			//NO CAMBIAR POR DEFECTO DEBE SER NULO
			//tableSchema=columnSchema.Table;
			ArrayList arrayListForeignKeys =new ArrayList();
				
			foreach(TableKeySchema tableKeySchema in columnSchema.Table.ForeignKeys) {
				//if(!ExisteTablaEnListaC(tableKeySchema.PrimaryKeyTable,arrayListForeignKeys)) {
				//arrayListForeignKeys.Add(tableKeySchema.PrimaryKeyTable);
				//}
				foreach(MemberColumnSchema memberColumnSchema in tableKeySchema.ForeignKeyMemberColumns) {
					if(memberColumnSchema.Column.Equals(columnSchema)){
						tableSchema=tableKeySchema.PrimaryKeyTable;
						break;
					}
				}
			}
			
			return tableSchema;
		}
		
		public ColumnSchema GetColumnSchemaFromColumnForeignKey(TableSchema tableSchemaPK,TableSchema tableSchemaFK,ColumnSchema columnSchemaFK) {		
			MemberColumnSchema memberColumnSchema=null;
			ColumnSchema columnSchema=null;
			TableSchema tableSchema=null;
			//NO CAMBIAR POR DEFECTO DEBE SER NULO
			//tableSchema=columnSchema.Table;
			ArrayList arrayListForeignKeys =new ArrayList();
				
			foreach(TableKeySchema tableKeySchema in columnSchemaFK.Table.ForeignKeys) {
				
				if(!tableKeySchema.PrimaryKeyMemberColumns.Count.Equals(tableKeySchema.ForeignKeyMemberColumns.Count)) {
					System.Windows.Forms.MessageBox.Show("NO COINCIDE EL NUMERO DE COLUMNAS PK  Y FK");
					
				}
				
				if(tableKeySchema.PrimaryKeyTable.Name.Equals(tableSchemaPK.Name) && tableKeySchema.ForeignKeyTable.Name.Equals(tableSchemaFK.Name)) {
					
					//for(PrimaryKeyMemberColumns) {
					for(int i=0;i<tableKeySchema.PrimaryKeyMemberColumns.Count;i++) {
						if(tableKeySchema.ForeignKeyMemberColumns[i].Column.Name.Equals(columnSchemaFK.Name)) {
							memberColumnSchema=tableKeySchema.PrimaryKeyMemberColumns[i];
							columnSchema=memberColumnSchema.Column;
							
							return columnSchema;
						}
					}	
				}
				/*
				foreach(MemberColumnSchema memberColumnSchema in tableKeySchema.ForeignKeyMemberColumns) {
					if(memberColumnSchema.Column.Equals(columnSchemaFK)){
						tableSchema=tableKeySchema.PrimaryKeyTable;
						break;
					}
				}
				*/
				
			}
			
			return columnSchema;
		}
		
		public String GetPersistenceTableManyToManyColumnsC(TableSchema TablaBase,CollectionInfo collectionInfo,String strCommentRelacionesPersistencia) {		
			String strJoinColumns="\r\n\t\t"+strCommentRelacionesPersistencia+"joinColumns=";
			String strInverseColumns="\r\n\t\t"+strCommentRelacionesPersistencia+"inverseJoinColumns=";
			String strJoinColumn="";
			String strInverseColumn="";
			ArrayList arrJoinColumns=new ArrayList();
			ArrayList arrInverseColumns=new ArrayList();
			//ArrayList arrJoinColumns1=new ArrayList();
			//ArrayList arrJoinColumns2=new ArrayList();
			bool blnEsPrimero=true;
			
			String strPersistenceTableManyToManyColumns="";
			TableKeySchema tableKeySchema=collectionInfo.TableKey;
			
			//Trace.WriteLine("FKT:"+tableKeySchema.ForeignKeyTable.Name);
			
			
			//for(MemberColumnSchema memberColumn in tableKeySchema.ForeignKeyMemberColumns) {
			MemberColumnSchema memberColumnSchemaFK=null;
			MemberColumnSchema memberColumnSchemaPK=null;
			
			if(!tableKeySchema.ForeignKeyMemberColumns.Count.Equals(tableKeySchema.PrimaryKeyMemberColumns.Count)) {
				System.Windows.Forms.MessageBox.Show("RELACION MUCHOS A MUCHOS NO COINCIDE PK Y FK EN CANTIDAD "+TablaBase.Name);
				return "";
			}
			
			for(int i=0;i< tableKeySchema.ForeignKeyMemberColumns.Count;i++) {	
				if(!blnEsPrimero) {
					strJoinColumn=",";
				}
				 memberColumnSchemaFK=tableKeySchema.ForeignKeyMemberColumns[i];
				 memberColumnSchemaPK=tableKeySchema.PrimaryKeyMemberColumns[i];
				
				strJoinColumn+="@JoinColumn(name=\""+GetNombreColumnFromPropertiesC(memberColumnSchemaFK.Column,true)+"\"";
				strJoinColumn+=",referencedColumnName=\""+GetNombreColumnFromPropertiesC(memberColumnSchemaPK.Column,true)+"\"";
				strJoinColumn+=")";
				
				//Trace.WriteLine("FCLM:"+memberColumnSchemaFK.Column.Name);
				//Trace.WriteLine("PCLM:"+memberColumnSchemaPK.Column.Name);
				
				arrJoinColumns.Add(strJoinColumn);
				strJoinColumns+=strJoinColumn;
				
				strJoinColumn="";
				blnEsPrimero=false;
			}
			
			//Trace.WriteLine("PKT:"+tableKeySchema.PrimaryKeyTable.Name);
			
			//SE UNEN LOS DOS ARRIBA
			/*
			foreach(MemberColumnSchema memberColumn in tableKeySchema.PrimaryKeyMemberColumns) {								
				strJoinColumn=",referencedColumnName="+GetNombreClaseC(memberColumn.Table.ToString())+"DataAccess.getColumnNameNative"+strIdGetSet+"()";
				arrJoinColumns2.Add(strJoinColumn);
				
				Trace.WriteLine("PCLM:"+memberColumn.Column.Name);
			}
			*/
			
			
			strJoinColumns+=",";
			
			//Trace.WriteLine("SKT:"+collectionInfo.SecondaryTable);
			
			TableKeySchema tableKeySchemaReverse=GetManyToManyTableKeySchemaInverseFromTable(collectionInfo.SecondaryTableSchema,TablaBase);
			
			if(tableKeySchemaReverse!=null) {
				
				
				//Trace.WriteLine("FKT:"+tableKeySchemaReverse.ForeignKeyTable.Name);
				
				if(!tableKeySchemaReverse.ForeignKeyMemberColumns.Count.Equals(tableKeySchemaReverse.PrimaryKeyMemberColumns.Count)) {
					System.Windows.Forms.MessageBox.Show("RELACION MUCHOS A MUCHOS NO COINCIDE PK Y FK EN CANTIDAD "+TablaBase.Name);
					return "";
				}
			
				blnEsPrimero=true;
				
				for(int i=0;i< tableKeySchemaReverse.ForeignKeyMemberColumns.Count;i++) {	
				//foreach(MemberColumnSchema memberColumn in tableKeySchemaReverse.ForeignKeyMemberColumns) {
					
					if(!blnEsPrimero) {
						strInverseColumn=",";
					}
					memberColumnSchemaFK=tableKeySchemaReverse.ForeignKeyMemberColumns[i];
					memberColumnSchemaPK=tableKeySchemaReverse.PrimaryKeyMemberColumns[i];
					
					strInverseColumn+="@JoinColumn(name=\""+GetNombreColumnFromPropertiesC(memberColumnSchemaFK.Column,true)+"\"";
					strInverseColumn+=",referencedColumnName=\""+GetNombreColumnFromPropertiesC(memberColumnSchemaPK.Column,true)+"\"";
					strInverseColumn+=")";
					
					//Trace.WriteLine("FCLM:"+memberColumnSchemaFK.Column.Name);
					//Trace.WriteLine("PCLM:"+memberColumnSchemaPK.Column.Name);
					
					arrInverseColumns.Add(strInverseColumn);
					strInverseColumns+=strInverseColumn;
					
					strInverseColumn="";
					blnEsPrimero=false;
				}
				
				
				//Trace.WriteLine("PKT:"+tableKeySchemaReverse.PrimaryKeyTable.Name);
				//ANTES SE UNEN
				/*
				foreach(MemberColumnSchema memberColumn in tableKeySchemaReverse.PrimaryKeyMemberColumns) {
					Trace.WriteLine("PCLM:"+memberColumn.Column.Name);
				}
				*/
			} else {
				//System.Windows.Forms.MessageBox.Show("RELACION MUCHOS A MUCHOS SIN INVERSE RELACION "+TablaBase.Name);
				Trace.WriteLine("RELACION MUCHOS A MUCHOS SIN INVERSE RELACION "+TablaBase.Name);
			}
			
			//strInitFuncion+="\r\n\t\tjoinColumns=@JoinColumn(name=ConstantesSql.ID+"+GetNombreClaseC(TablaBase.ToString())+"ConstantesFunciones.TABLENAME, referencedColumnName="+GetIdNameC(TablaBase)+"),";
			//strInitFuncion+="\r\n\t\tinverseJoinColumns=@JoinColumn(name=ConstantesSql.ID+"+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+"ConstantesFunciones.TABLENAME, referencedColumnName="+GetIdNameC(collectionInfo.SecondaryTableSchema)+")";
							
			/*
			Trace.WriteLine("PKT:"+collectionInfo.PrimaryTable);
			foreach(String sColumn in collectionInfo.PkColNames) {
				Trace.WriteLine("PCLM:"+sColumn);
			}
			
			Trace.WriteLine("SKT:"+collectionInfo.SecondaryTable);
			foreach(String sColumn in collectionInfo.FkColNames) {
				Trace.WriteLine("SCLM:"+sColumn);
			}
			
			Trace.WriteLine("JKT:"+collectionInfo.JunctionTable);
			foreach(String sColumn in collectionInfo.JunctionTablePkColNames) {
				Trace.WriteLine("JCLM:"+sColumn);
			}			
			*/
			
			
			
			//strPersistenceTableManyToManyColumns+="\r\n\t\tjoinColumns=@JoinColumn(name=ConstantesSql.ID+"+GetNombreClaseC(TablaBase.ToString())+"ConstantesFunciones.TABLENAME, referencedColumnName="+GetIdNameC(TablaBase)+"),";
			//strPersistenceTableManyToManyColumns+="\r\n\t\tinverseJoinColumns=@JoinColumn(name=ConstantesSql.ID+"+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+"ConstantesFunciones.TABLENAME, referencedColumnName="+GetIdNameC(collectionInfo.SecondaryTableSchema)+")";
			
			strPersistenceTableManyToManyColumns=strJoinColumns+strInverseColumns;
			
			return strPersistenceTableManyToManyColumns;
		}
		
		public TableKeySchema GetManyToManyTableKeySchemaInverseFromTable(TableSchema tableSchemaOrigen,TableSchema tableSchemaObjetivo) 
		{
			String strFuncion=string.Empty;
			String strTablaClaseRelacionada=string.Empty;
			String strInitFuncion="";//"public ";// void  getTR_";
			
			String strEndFuncion="\t}";
			
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(tableSchemaOrigen);
			
			ArrayList arrayListRelaciones=new ArrayList();
			String strNombreAdicional="";
			TableKeySchema tableKeySchema=null;
			
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{	
				
						
				/*
				if(ExisteTablasClasesYaRelacionadas(arrayListRelaciones,collectionInfo.SecondaryTable)) {
					strNombreAdicional=GetNombreAdicionalClaseRelacionadaFromRelation(collectionInfo);
					//continue;
				} else {
					strNombreAdicional="";
					arrayListRelaciones.Add(collectionInfo.SecondaryTable);
				}
				*/
				
				if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany)
				{
					if(collectionInfo.SecondaryTableSchema.Name.Equals(tableSchemaObjetivo.Name)){
						tableKeySchema=collectionInfo.TableKey;
					}
					
					/*
					strInitFuncion+="\r\n\tpublic ";	
					strInitFuncion+=GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+" get"+GetPrefijoRelacionC()+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+"() {";
					strTablaClaseRelacionada="\r\n\t\treturn this."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+";\r\n";
					*/
				}	
			}
								
			return tableKeySchema; 
		}
		
		/*
		ArrayList arrayListForeignKeys =new ArrayList();
				
				foreach(TableKeySchema tableKeySchema in TablaBase.ForeignKeys) {
					if(!ExisteTablaEnListaC(tableKeySchema.PrimaryKeyTable,arrayListForeignKeys)) {
						arrayListForeignKeys.Add(tableKeySchema.PrimaryKeyTable);
					}
				}
				
				foreach(TableSchema tableSchemaForeignKey in arrayListForeignKeys) {
					strTablaClaseRelacionada+="\r\n\tpublic "+GetNombreClaseC(tableSchemaForeignKey.ToString())+" "+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(tableSchemaForeignKey.ToString())+";";					
				}
		*/
		
		public String GetColumnsPKParametrosC(TableSchema tablaBase) {		
			String strColumnsPKParametros="";
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					strColumnsPKParametros+=GetParameterFunctionColumnC(tablaBase.Columns[i],true) ;
				} 
			} 
			
			return strColumnsPKParametros;
		}
		
		public bool GetTieneTipoPKStandardC(TableSchema tablaBase) {
			bool blnTieneTipoPKStandard=false;
			String strColumnsPKParametros="";
			int intCountPK=0;
			bool blnEsBigIntColumn=false;
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					intCountPK++;
					
					blnEsBigIntColumn=EsBigIntColumn(tablaBase.Columns[i]);					
					
				} 
			} 
			
			if(intCountPK.Equals(1)&&blnEsBigIntColumn) {
				blnTieneTipoPKStandard=true;
			}
			
			return blnTieneTipoPKStandard;
		}		
		
		public String GetColumnsPKParametrosSinComaPrimeroC(TableSchema tablaBase) {		
			String strColumnsPKParametros="";
			bool blnEsPrimero=true;
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 			
					strColumnsPKParametros+=GetParameterFunctionColumnC(tablaBase.Columns[i],!blnEsPrimero) ;
					
					if(blnEsPrimero) {
						blnEsPrimero=false;
					}
				} 
			} 
			
			return strColumnsPKParametros;
		}
		
		
		public String GetColumnsPKParametrosUsoC(TableSchema tablaBase) {		
			String strColumnsPKParametros="";
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					strColumnsPKParametros+=GetParameterFunctionUsoColumnC(tablaBase.Columns[i],true) ;
				} 
			} 
			
			return strColumnsPKParametros;
		}
		
		public String GetColumnsPKParametrosUsoSinComaPrimeroC(TableSchema tablaBase) {		
			String strColumnsPKParametros="";
			bool blnEsPrimero=true;
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					strColumnsPKParametros+=GetParameterFunctionUsoColumnC(tablaBase.Columns[i],!blnEsPrimero) ;
					
					if(blnEsPrimero) {
						blnEsPrimero=false;
					}
				} 
			} 
			
			return strColumnsPKParametros;
		}
		//GetParameterSelection(memberColumnSchema.Column,false,esUnico,false,false,esRanges,false);
		
		public String GetColumnsPKParameterSelectionC(TableSchema tablaBase,bool esNative) {		
			String strColumnsPKParametros="";
			bool esUnico=true;
			bool esUltimo=false;
			bool esRanges=false;
			int countNumeroPK=1;
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					if(countNumeroPK>=intPorTablaCountColumnsPKC) {
						esUltimo=true;
					}
					
					strColumnsPKParametros+=GetParameterSelectionC(tablaBase.Columns[i],esUltimo,esUnico,false,esNative,esRanges,false) ;
					countNumeroPK++;
				} 
			} 
			
			return strColumnsPKParametros;
		}
		
		public int GetCountColumnsPKC(TableSchema tablaBase) {		
			int intColumnsPKParametros=0;
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					intColumnsPKParametros++;
				} 
			} 
			
			return intColumnsPKParametros;
		}
		
		public ColumnSchemaCollection GetColumnsPKC(TableSchema tablaBase) {		
			ColumnSchemaCollection columnSchemaCollection=new ColumnSchemaCollection();
			
			for (int i = 0; i < tablaBase.Columns.Count; i++){ 
				if(tablaBase.Columns[i].IsPrimaryKeyMember){ 
					columnSchemaCollection.Add(tablaBase.Columns[i]);
				} 
			} 
			
			return columnSchemaCollection;
		}
		
		public bool ExisteTablaEnListaC(TableSchema tableExiste,ArrayList arrayList) {
			bool blnExiste=false;
				
			foreach(TableSchema tableSchema in arrayList) {
				if(tableSchema.Name.Equals(tableExiste.Name)) {
					blnExiste=true;
					break;	
				}
			}
			
			return blnExiste;
		}
		
		public ArrayList GetArrayListForeignKeys(TableSchema TablaBase) {		
			ArrayList arrayListForeignKeys =new ArrayList();
				
			foreach(TableKeySchema tableKeySchema in TablaBase.ForeignKeys) {
				if(!ExisteTablaEnListaC(tableKeySchema.PrimaryKeyTable,arrayListForeignKeys)) {
					arrayListForeignKeys.Add(tableKeySchema.PrimaryKeyTable);
				}
			}
			
			return arrayListForeignKeys;
		}
		
		public MemberColumnSchemaCollection GetMemberColumnSchemaForeignKeysC(TableSchema tableSchema,TableSchema tableSchemaForeignKey) {		
			MemberColumnSchemaCollection memberColumnSchemaCollectionForeignKeys =new MemberColumnSchemaCollection();
				
			foreach(TableKeySchema tableKeySchema in tableSchema.ForeignKeys) {
				if(tableKeySchema.PrimaryKeyTable.Name.Equals(tableSchemaForeignKey.Name)) {
					memberColumnSchemaCollectionForeignKeys=tableKeySchema.ForeignKeyMemberColumns;
					break;
				}
			}
			
			return memberColumnSchemaCollectionForeignKeys;
		}
		
		public String GetColumnsForeignKeysPersistenciaC(MemberColumnSchemaCollection memberColumnSchemaCollectionFK) {		
			String strColumnsForeignKeysPersistencia="";
				
			foreach(MemberColumnSchema memberColumnSchema in memberColumnSchemaCollectionFK) {
				//System.Windows.Forms.MessageBox.Show(memberColumnSchema.Name);
				strColumnsForeignKeysPersistencia+="\r\n\t@JoinColumn(name = \""+GetNombreColumnFromProperties(memberColumnSchema.Column)+"\", nullable = true,insertable=false, updatable=false)";
			}
			strColumnsForeignKeysPersistencia="\r\n\t@JoinColumns({"+strColumnsForeignKeysPersistencia+"\r\n\t})";
			
			return strColumnsForeignKeysPersistencia;
		}
		
		public String GetColumnsForeignKeysCallDataAccessC(TableSchema TablaBase,MemberColumnSchemaCollection memberColumnSchemaCollectionFK) {		
			String strColumnsForeignKeysPersistencia="";
				
			foreach(MemberColumnSchema memberColumnSchema in memberColumnSchemaCollectionFK) {
				//System.Windows.Forms.MessageBox.Show(memberColumnSchema.Name);
				strColumnsForeignKeysPersistencia+=",rel"+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+GetNombreCompletoColumnaClaseC(memberColumnSchema.Column)+ "()";
			}
			
			return strColumnsForeignKeysPersistencia;
		}
		
		public String GetColumnsForeignKeysAdditionalDescripcionC(TableSchema tableSchemaForeignKey,MemberColumnSchemaCollection memberColumnSchemaCollectionFK) {		
			String strColumnsForeignKeysPersistencia="";
				
			foreach(MemberColumnSchema memberColumnSchema in memberColumnSchemaCollectionFK) {
				//System.Windows.Forms.MessageBox.Show(memberColumnSchema.Column.Table.Name);
				//strColumnsForeignKeysPersistencia+="\r\n\t@JoinColumn(name = \""+GetNombreColumnFromProperties(memberColumnSchema.Column)+"\", nullable = true,insertable=false, updatable=false)";
				strColumnsForeignKeysPersistencia+="\t\t\tsDescripcion+="+GetNombreClaseObjetoC(tableSchemaForeignKey.ToString()) +"."+GetXmlColumnaFuncionDescripcionFromPropertiesC(memberColumnSchema.Column)+";";
				//System.Windows.Forms.MessageBox.Show(strColumnsForeignKeysPersistencia);
			}
			
			return strColumnsForeignKeysPersistencia;
		}
		
		public String GetColumnsForeignKeysAdditionalDescripcionC(TableSchema tableSchemaForeignKey,ColumnSchemaCollection columnSchemaCollectionFK) {		
			String strColumnsForeignKeysPersistencia="";
				
			foreach(ColumnSchema columnSchema in columnSchemaCollectionFK) {
				//System.Windows.Forms.MessageBox.Show(columnSchema.Table.Name);
				//strColumnsForeignKeysPersistencia+="\r\n\t@JoinColumn(name = \""+GetNombreColumnFromProperties(memberColumnSchema.Column)+"\", nullable = true,insertable=false, updatable=false)";
				strColumnsForeignKeysPersistencia+="\t\t\tsDescripcion+="+GetNombreClaseObjetoC(tableSchemaForeignKey.ToString()) +"."+GetXmlColumnaFuncionDescripcionFromPropertiesC(columnSchema)+";";
				//System.Windows.Forms.MessageBox.Show(strColumnsForeignKeysPersistencia);
			}
			
			return strColumnsForeignKeysPersistencia;
		}
		
		public String GetColumnsForeignKeysReporteDescripcionC(TableSchema TablaBase,TableSchema tableSchemaForeignKey,MemberColumnSchemaCollection memberColumnSchemaCollectionFK) {		
			String strColumnsForeignKeysPersistencia="";
			String strTipo="";
			String strPrefijo="";
			String strColumna="";	
			String strIfElse="";
			ColumnSchema column=null;
			
			foreach(MemberColumnSchema memberColumnSchema in memberColumnSchemaCollectionFK) {
				column=memberColumnSchema.Column;
				
				strTipo=GetTipoColumnaClaseC(column);	
				strPrefijo=GetPrefijoTipoC(column);	
				strColumna=GetNombreColumnaClaseC(column);	
				strIfElse="\r\nif(request.getParameter(\""+GetNameControlHtml(column) +"\"+i.tToString())== null||request.getParameter(\""+GetNameControlHtml(column) + "\"+i.toString())==\"\")\r\n{\r\n";
				   strIfElse+=strPrefijo+strColumna+"=null;\r\n}\r\nelse\r\n{\r\n";	
				//System.Windows.Forms.MessageBox.Show(memberColumnSchema.Name);
				//strColumnsForeignKeysPersistencia+="\t\t\tsDescripcion+="+GetNombreClaseObjetoC(tableSchemaForeignKey.ToString()) +"."+GetXmlColumnaFuncionDescripcionFromPropertiesC(memberColumnSchema.Column)+";";
				strColumnsForeignKeysPersistencia+=GetNombreClaseObjetoC(TablaBase.ToString())+strClaseBean.ToLower()+".setField_"+strPrefijo+ strColumna +strClaseDetalleBean+"("+GetNombreClaseC(TablaBase.ToString())+"ConstantesFunciones.get"+GetPrefijoRelacionC()+GetNombreClaseC(tableSchemaForeignKey.ToString())+"Descripcion("+GetNombreClaseObjetoC(TablaBase.ToString()) +".get"+GetPrefijoRelacionC() +GetNombreClaseC(tableSchemaForeignKey.ToString())+"())"+");";
			}
			
			return strColumnsForeignKeysPersistencia;
		}
		
			
		/*	CollectionInfo collectionInfo=...........
		
		TableKeySchema TableKey = null;
		TableSchema PrimaryTableSchema;		string PrimaryTable;
		TableSchema SecondaryTableSchema;	string SecondaryTable;	string[] SecondaryTablePkColNames;
		TableSchema JunctionTableSchema;	string JunctionTable;	string[] JunctionTablePkColNames;
		string[] PkColNames;	string[] FkColNames;	
				
		RelationshipType CollectionRelationshipType;	string CollectionTypeName = string.Empty;
		string TypeName = string.Empty;		string CallParams = string.Empty;	string PropertyName = string.Empty;
		*/
		
		public String GetFinalQueryRelacionesC(TableSchema TablaBase,CollectionInfo collectionInfo,String strNombreAdicional) {		
			String stFinalQueryRelaciones="";
			
			/*
			//PRIMARY KEY -- TIPO VUELO
			
			System.Windows.Forms.MessageBox.Show(collectionInfo.TableKey.PrimaryKeyTable.Name);
			
			foreach(MemberColumnSchema MemberColumnSchema in collectionInfo.TableKey.PrimaryKeyMemberColumns) {
				System.Windows.Forms.MessageBox.Show(MemberColumnSchema.Column.Name);
			}
			
			//FOREIGN KEY  -- VUELO
			System.Windows.Forms.MessageBox.Show(collectionInfo.TableKey.ForeignKeyTable.Name);
			
			foreach(MemberColumnSchema MemberColumnSchema in collectionInfo.TableKey.ForeignKeyMemberColumns) {
				System.Windows.Forms.MessageBox.Show(MemberColumnSchema.Column.Name);
			}
			*/
			
			if(!blnNoStandardTableFromProperties) {	
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
					stFinalQueryRelaciones+="\""+strHqlJoinRelacionnes+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+ " WHERE "+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+"."+strId+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"());\r\n";
				
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany) {	
					stFinalQueryRelaciones+="\""+strHqlJoinRelacionnes+""+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+ " WHERE "+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+"."+strId+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"());\r\n";
						
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany) {					
					stFinalQueryRelaciones+="\""+strHqlJoinRelacionnes+""+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC("dbo"+collectionInfo.JunctionTableSchema)+ "s table2 "+strHqlJoinRelacionnes+" table2."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+ " table3 WHERE table3.id=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"());\r\n";					
						
				}
			} else {
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
					stFinalQueryRelaciones+="\""+strHqlJoinRelacionnes+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+ GetWhereFinalQueryRelacionesNoStandardC(TablaBase,collectionInfo,strNombreAdicional);//" WHERE "+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+"."+strId+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()) );\r\n";
				
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany) {	
					stFinalQueryRelaciones+="\""+strHqlJoinRelacionnes+""+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+ GetWhereFinalQueryRelacionesNoStandardC(TablaBase,collectionInfo,strNombreAdicional);//" WHERE "+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+"."+strId+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()));\r\n";
						
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany) {					
					stFinalQueryRelaciones+="\""+strHqlJoinRelacionnes+""+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC("dbo"+collectionInfo.JunctionTableSchema)+ "s table2 "+strHqlJoinRelacionnes+" table2."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+ " table3 "+ GetWhereFinalQueryRelacionesNoStandardC(TablaBase,collectionInfo,strNombreAdicional);//WHERE table3.id=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()));\r\n";					
						
				}
			}
			
			return stFinalQueryRelaciones;
		}
		
		public String GetWhereFinalQueryRelacionesNoStandardC(TableSchema TablaBase,CollectionInfo collectionInfo,String strNombreAdicional) {		
			String stFinalWhereQueryRelaciones="";
			String strWhere=" WHERE ";
			
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
					stFinalWhereQueryRelaciones+=strWhere+GetWhereParametersFinalQueryRelacionesNoStandardC(TablaBase,collectionInfo,strNombreAdicional)+";\r\n";
				
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany) {	
					stFinalWhereQueryRelaciones+=strWhere+GetWhereParametersFinalQueryRelacionesNoStandardC(TablaBase,collectionInfo,strNombreAdicional)+";\r\n";
						
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany) {					
					stFinalWhereQueryRelaciones+=strWhere+GetWhereParametersFinalQueryRelacionesNoStandardC(TablaBase,collectionInfo,strNombreAdicional)+";\r\n";					
						
				}
				
			return stFinalWhereQueryRelaciones;
		}
		
		public String GetWhereParametersFinalQueryRelacionesNoStandardC(TableSchema TablaBase,CollectionInfo collectionInfo,String strNombreAdicional) {		
			String stFinalWhereParametersQueryRelaciones="";
			String stFinalWhereParameterInitialQueryRelaciones="";
			String stFinalWhereParameterFinishQueryRelaciones="";
			ArrayList arrayListParametersInitials=new ArrayList();
			ArrayList arrayListParametersFinishs=new ArrayList();
			
			String strPrePostFijo="";
			
			
			
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
					//stFinalWhereParametersQueryRelaciones+=GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+"."+strId+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()) )";
					foreach(MemberColumnSchema memberColumnSchema in collectionInfo.TableKey.PrimaryKeyMemberColumns) {
						strPrePostFijo="";
						
						if(EsConPrePostFijoQueryC(memberColumnSchema.Column) ) {
							strPrePostFijo="'";	
						}
						
						stFinalWhereParametersQueryRelaciones+=GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+"."+GetNombreCompletoLowerColumnaClaseC(memberColumnSchema.Column)+"="+strPrePostFijo+"\"+"+GetTipoColumnaParse(memberColumnSchema.Column,GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+GetNombreCompletoColumnaClaseC(memberColumnSchema.Column)+"()")+"+\""+strPrePostFijo+"\")";
					}
					
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany) {	
					//stFinalWhereParametersQueryRelaciones+=GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+"."+strId+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()));\r\n";
					strPrePostFijo="";											
						
					foreach(MemberColumnSchema memberColumnSchema in collectionInfo.TableKey.PrimaryKeyMemberColumns) {
						strPrePostFijo="";
						
						if(EsConPrePostFijoQueryC(memberColumnSchema.Column) ) {
							strPrePostFijo="'";	
						}
						
						//stFinalWhereParametersQueryRelaciones+=GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+"."+GetNombreCompletoLowerColumnaClaseC(memberColumnSchema.Column)+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()))";
						//stFinalWhereParameterInitialQueryRelaciones=GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+"."+GetNombreCompletoLowerColumnaClaseC(memberColumnSchema.Column);//+"=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()))";
						//arrayListParametersInitials.Add(stFinalWhereParameterInitialQueryRelaciones);
						stFinalWhereParametersQueryRelaciones+=GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+"."+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(TablaBase.ToString())+strNombreAdicional+"."+GetNombreCompletoLowerColumnaClaseC(memberColumnSchema.Column)+"="+strPrePostFijo+"\"+"+GetTipoColumnaParse(memberColumnSchema.Column,GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+GetNombreCompletoColumnaClaseC(memberColumnSchema.Column)+"()")+"+\""+strPrePostFijo+"\")";
																																																							
					}
					
					/*
					foreach(MemberColumnSchema memberColumnSchema in collectionInfo.TableKey.ForeignKeyMemberColumns) {
						stFinalWhereParameterFinishQueryRelaciones="=\"+"+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+GetNombreCompletoLowerColumnaClaseC(memberColumnSchema.Column)+"()))";
						arrayListParametersFinishs.Add(stFinalWhereParameterFinishQueryRelaciones);
					}
					
					for (int i = 0; i < arrayListParametersInitials.Count; i++) {
						stFinalWhereParametersQueryRelaciones+=(String)arrayListParametersInitials[i]+(String)arrayListParametersFinishs[i];
					}
					*/
					
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany) {					
					//stFinalWhereParametersQueryRelaciones+=" table3.id=\"+String.valueOf("+GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+strIdGetSet+"()))";					
					foreach(MemberColumnSchema memberColumnSchema in collectionInfo.TableKey.PrimaryKeyMemberColumns) {
						strPrePostFijo="";
						
						if(EsConPrePostFijoQueryC(memberColumnSchema.Column) ) {
							strPrePostFijo="'";	
						}
						
						stFinalWhereParametersQueryRelaciones+=" table3."+GetNombreCompletoLowerColumnaClaseC(memberColumnSchema.Column)+"="+strPrePostFijo+"\"+"+GetTipoColumnaParse(memberColumnSchema.Column,GetNombreClaseObjetoC("dbo."+TablaBase.Name)+".get"+GetNombreCompletoColumnaClaseC(memberColumnSchema.Column)+"()")+"+\""+strPrePostFijo+"\")";
					}						
				}
				
			return stFinalWhereParametersQueryRelaciones;
		}
		
		public bool EsConPrePostFijoQueryC(ColumnSchema column) {
			bool blnPrePostFijo=false;
			
			if(EsVarCharColumn(column) ||EsCharColumn(column) ||EsDateTimeColumn(column)|| EsDateColumn(column)|| EsTimeColumn(column) ) {
				blnPrePostFijo=true;
			}
			
			return blnPrePostFijo;
		}
		
		public bool EsPKCompuestoTabla(TableSchema table) {
			bool blnEsPKCompuesto=false;
			
			int intNumero=0;
			
			for (int i = 0; i < table.Columns.Count; i++){	
				if(table.Columns[i].IsPrimaryKeyMember){
					intNumero++; 
				}		
			}
			
			if(intNumero>1) {
				blnEsPKCompuesto=true;
			}
	
			return blnEsPKCompuesto;
		}
		
		#region Web
		public ArrayList arrBusquedaPorTablaFK=new ArrayList();
		public const String strJSInitialJavaScript="<script type=\"text/javascript\" language=\"javascript\">";	
		public const String strJSEndJavaScript="</script>";
		public const String strIncludeInit="<jsp:include page=";
		public const String strIncludeEnd="/>";
		public const String strHtmlTypeElementoInicial="span class=\"elementotitulocampo\"";
		public const String strHtmlTypeElementoFinal="span";
		public const String strHtmlTypeBusquedaInicial="span  class=\"busquedatitulocampo\"";
		public const String strHtmlTypeBusquedaFinal="span";
		
		
		
		public String GetWebRowAccionesTablaClaseC(TableSchema TablaBase,bool esMantenimientoSimple,bool ConFaces) 
		{
			String strHtml="";
			
			String strRelaciones="";
			
			if(!esMantenimientoSimple)
			{
				strRelaciones="Relaciones";
			}
			
			
			
			
			String strObjectFace="";
			
			if(ConFaces)
			{
				strObjectFace=GetNombreClaseObjetoC(TablaBase.ToString())+strPrefijoJSFFaces;
			}
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Acciones\" class=\"acciones\" style=\"display:none\">";							
			}
			else
			{
				strHtml+="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Acciones\" class=\"acciones\" style=\"display:${"+strObjectFace+".strVisibilidadTablaAcciones}\">";							
			}
			
			strHtml+="\r\n\t\t\t<td align=\"center\">";
			strHtml+="<A name=\"Acciones\" ></A>";
			
			
			
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t<form name=\"frmAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"\">";
			}
			else
			{
				//strHtml+="\r\n\t\t\t\t<h:form id=\"frmAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"\">";								
			}
			
			
			
			strHtml+="\r\n\t\t\t\t\t<table id=\"tblAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"\" width=\"50%\" align=\""+GetAlignTableFromPropertiesC(TablaBase)+"\" >";
			strHtml+="\r\n\t\t\t\t\t\t<tr id=\"trAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"Basicos\">";
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:visible\">";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:${"+strObjectFace+".strVisibilidadCeltaNuevo"+GetNombreClaseC(TablaBase.ToString())+"}\">";
			}
			
			
			if(!ConFaces)
			{
				strHtml+="<a:widget id=\"btndjdjtNuevo"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strNuevo+"'}\" />";
			}
			else
			{
				strHtml+="<a:widget id=\"btndjdjtNuevo"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strNuevo+"'}\" />";
				//strHtml+="\r\n\t\t\t\t\t\t\t\t<h:commandButton id=\"btndjdjtNuevo"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\"  action=\"#{sumarBean.Sumar}\" value=\""+strNuevo+"\"/>";
			}
			
			
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnActualizar"+GetNombreClaseC(TablaBase.ToString())+"\"style=\"visibility:visible\">";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnActualizar"+GetNombreClaseC(TablaBase.ToString())+"\"style=\"visibility:${"+strObjectFace+".strVisibilidadCeltaActualizar"+GetNombreClaseC(TablaBase.ToString())+"}\">";
			}
			
			
			if(!ConFaces)
			{
				strHtml+="<a:widget id=\"btndjdjtActualizar"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strActualizar+"'}\" />";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t\t<h:commandButton id=\"btndjdjtActualizar"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\"  action=\"#{sumarBean.Sumar}\" value=\""+strActualizar+"\"/>";
			}
			
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnEliminar"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:visible\">";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnEliminar"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:${"+strObjectFace+".strVisibilidadCeltaEliminar"+GetNombreClaseC(TablaBase.ToString())+"}\">";
			}
			
			
			if(!ConFaces)
			{
				strHtml+="<a:widget id=\"btndjdjtEliminar"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strEliminar+"'}\" />";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<h:commandButton id=\"btndjdjtEliminar"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\"  action=\"#{sumarBean.Sumar}\" value=\""+strEliminar+"\"/>";
			}
			
			
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnCancelar"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:visible\">";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnCancelar"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:${"+strObjectFace+".strVisibilidadCeltaCancelar"+GetNombreClaseC(TablaBase.ToString())+"}\">";
			}
			
			
			if(!ConFaces)
			{
				strHtml+="<a:widget id=\"btndjdjtCancelar"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strCancelar+"'}\" />";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t\t<h:commandButton id=\"btndjdjtCancelar"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\"  action=\"#{sumarBean.Sumar}\" value=\""+strCancelar+"\"/>";
			}
			
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t</tr>";
			strHtml+="\r\n\t\t\t\t\t\t<tr id=\"trAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"Guardar\">";
			
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnGuardarCambios"+GetNombreClaseC(TablaBase.ToString())+"\" colspan=\"4\" align=\"center\"style=\"visibility:visible\">";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnGuardarCambios"+GetNombreClaseC(TablaBase.ToString())+"\" colspan=\"4\" align=\"center\"style=\"visibility:${"+strObjectFace+".strVisibilidadCeltaGuardar"+GetNombreClaseC(TablaBase.ToString())+"}\">";
			}
			
			
			if(!ConFaces)
			{
				strHtml+="<br><a:widget id=\"btndjdjtGuardarCambios"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strGuardarCambios+"'}\" />";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t\t\t\t\t<h:commandButton id=\"btndjdjtGuardarCambios"+GetNombreClaseC(TablaBase.ToString())+strRelaciones+"\"  action=\"#{sumarBean.Sumar}\" value=\""+strGuardarCambios+"\"/>";
			}
			
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";					
			strHtml+="\r\n\t\t\t\t\t\t</tr>";
			strHtml+="\r\n\t\t\t\t\t</table>";
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t</form>";
			}
			
			
			strHtml+="\r\n\t\t\t</td>";
			strHtml+="\r\n\t\t</tr>";
			
			if(ConFaces)
			{
				strHtml+="\r\n\t\t\t\t</h:form>";
			}
			
			if(ConFaces)
			{
				strHtml+="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Mensajes\" class=\"mensajes\">";	
				strHtml+="\r\n\t\t\t<td align=\"center\">";
				strHtml+="<A name=\"Mensajes\" ></A>";
				strHtml+="\r\n\t\t\t<h:messages/>";
				strHtml+="\r\n\t\t\t</td>";
				strHtml+="\r\n\t\t</tr>";
			
			}
			
		return strHtml;
		}
		
		public String GetWebRowImagenAccionesTablaClaseC(TableSchema TablaBase,ColumnSchema column) 
		{
			String strHtml="";
			
			String strRelaciones="";
			
						
			strHtml+="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Acciones\" class=\"acciones\" style=\"display:none\">";
			strHtml+="\r\n\t\t\t<td align=\"center\">";
			strHtml+="<A name=\"Acciones\" ></A>";
			strHtml+="\r\n\t\t\t\t<form name=\"frmAccionesMantenimiento"+GetNombreColumnaClaseC(column)+"\" method=\"post\" action=\""+GetRelativePathC(TablaBase)+GetNombreClaseC(TablaBase.ToString())+"Servlet\" enctype=\"multipart/form-data\">";
			strHtml+="\r\n\t\t\t\t\t<table id=\"tblAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"\" width=\"50%\" align=\""+GetAlignTableFromPropertiesC(TablaBase)+"\" >";
			strHtml+="\r\n\t\t\t\t\t\t<tr id=\"trAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"Basicos\">";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnArchivo"+GetNombreColumnaClaseC(column)+"\" style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t<input type=\"file\" name=\"fl"+GetNombreColumnaClaseC(column)+"\" id=\"fl"+GetNombreColumnaClaseC(column)+"\" onkeydown='this.blur()'>";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnAccionMantenimiento"+GetNombreColumnaClaseC(column)+"\" style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t\t<input name=\"accionMantenimiento\" type=\"hidden\" value=\"cargarImagen"+GetNombreClaseC(TablaBase.ToString())+GetNombreColumnaClaseC(column)+"\">";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnCargar"+GetNombreColumnaClaseC(column)+"\"style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t\t<input type=\"submit\" name=\"btnCargar"+GetNombreColumnaClaseC(column)+"\" value=\"Cargar "+GetWebNombreTituloColumnFromPropertiesC(column)+"\" />";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnCancelar"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t\t<a:widget id=\"btndjdjtCancelar"+GetNombreColumnaClaseC(column)+strRelaciones+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strCancelar+"'}\" />";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnActualizar"+GetNombreClaseC(TablaBase.ToString())+"\"style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnEliminar"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			
			strHtml+="\r\n\t\t\t\t\t\t</tr>";
			
			strHtml+="\r\n\t\t\t\t\t\t<tr id=\"trAccionesMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"Guardar\">";
			strHtml+="\r\n\t\t\t\t\t\t\t<td id=\"tdbtnGuardarCambios"+GetNombreClaseC(TablaBase.ToString())+"\" colspan=\"4\" align=\"center\"style=\"visibility:visible\">";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";					
			strHtml+="\r\n\t\t\t\t\t\t</tr>";
			
			strHtml+="\r\n\t\t\t\t\t</table>";
			strHtml+="\r\n\t\t\t\t</form>";
			strHtml+="\r\n\t\t\t</td>";
			strHtml+="\r\n\t\t</tr>";
		
		return strHtml;
		}
		
		public String GetWebRowElementosTablaClaseC(TableSchema TablaBase,bool esMantenimientoSimple,bool ConFaces) 
		{
			String strHtml="";
			
			
			if(ConFaces)
			{
				strHtml+="<h:form id=\"frmMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"\">";
			}
			
			if(!ConFaces)
			{
				strHtml+="<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Elementos\" class=\"elementos\" style=\"display:none\">";
			}
			else
			{
				String strObjectFace="";
			
				if(ConFaces)
				{
					strObjectFace=GetNombreClaseObjetoC(TablaBase.ToString())+strPrefijoJSFFaces;
				}
				
				strHtml+="<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Elementos\" class=\"elementos\" style=\"display:${"+strObjectFace+".strVisibilidadTablaElementos}\">";
			}
			
			
			strHtml+="<td align=\"center\">";
			strHtml+="<A name=\"Campos\"></A>";
			
			if(!ConFaces)
			{
				strHtml+="<form name=\"frmMantenimiento"+GetNombreClaseC(TablaBase.ToString())+"\">";
			}
			
			
			
			strHtml+="\r\n\t\t\t\t<table width=\"200\"  align=\""+GetAlignTableFromPropertiesC(TablaBase)+"\" >";
			
			for (int i = 0; i < TablaBase.Columns.Count; i++)
			{
				strHtml+=GetControlVariablesC(TablaBase.Columns[i],ConFaces);
			}
			
			strHtml+="</table>";
			
			if(!ConFaces)
			{
				strHtml+="</form>";
			}
			else
			{
				//strHtml+="</h:form>";
			}
			
			strHtml+="</td>";
			strHtml+="</tr>";	
		
		return strHtml;
		}
		
		public String GetDefinicionElementosSwingTablaClaseC(TableSchema TablaBase) 
		{
			String strSwing="";
			
			for (int i = 0; i < TablaBase.Columns.Count; i++)
			{
				strSwing+=GetControlVariablesSwingC(TablaBase.Columns[i]);
			}			
		
			return strSwing;
		}
		
		public bool GetExisteBusquedasTablaC(TableSchema TablaBase) 
		{
				bool blnExisteBusqueda=false;
			
			
			foreach(IndexSchema indexSchema in TablaBase.Indexes)
			{
				if(!indexSchema.IsPrimaryKey)
				{
					
					if(indexSchema.IsUnique)
					{
					}
					else
					{
						
						if(!blnExisteBusqueda)
						{
							blnExisteBusqueda=true;
						}
						
						
					}	
																																		
				}
			}
			
			
			return blnExisteBusqueda; 
		}
		
		public String GetIndicesVariablesTablasClasesC(TableSchema TablaBase) 
		{
			String strFuncion=string.Empty;
			String strTablaClaseRelacionada=string.Empty;
			String strInitFuncion="\r\n";
			bool blnExisteBusqueda=false;
			
			String strTabPanel=string.Empty;
			String strPanelesBusquedas=string.Empty;
			String strControlesPanelesBusquedas=string.Empty;
			
			foreach(IndexSchema indexSchema in TablaBase.Indexes)
			{
				if(!indexSchema.IsPrimaryKey)
				{
					
					if(indexSchema.IsUnique)
					{
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+indexSchema.Name+"(";
					}
					else
					{
						
						if(!blnExisteBusqueda)
						{
							blnExisteBusqueda=true;
							strTabPanel="\r\n\tprotected JTabbedPane jTabbedPaneBusquedas"+GetNombreClaseC(TablaBase.ToString())+";\r\n";
						}
						
						if(indexSchema.Name.Contains("Busqueda")||indexSchema.Name.Contains("FK"))
						{
							strPanelesBusquedas+="\tprotected JPanel jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"\r\n";
							strPanelesBusquedas+="\tprotected JButton jButton"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+";\r\n";
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+"(String strFinalQuery,";
						}
						else
						{
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+"(";
						}
					}	
					int count=1;	
					foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
					{
						
						
						if(!(indexSchema.Name.Contains("BusquedaMayor")||indexSchema.Name.Contains("BusquedaMayorIgual")||indexSchema.Name.Contains("BusquedaMenor")||indexSchema.Name.Contains("BusquedaMenorIgual")||indexSchema.Name.Contains("BusquedaRango")||indexSchema.Name.Contains("BusquedaRangoIgual")))
						{
							strControlesPanelesBusquedas+="\t"+GetDefinicionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+"\r\n"; 
							
								
							if(!indexSchema.MemberColumns.Count.Equals(count))
							{
								//strInitFuncion+=";\r\n";	
							}
						}
						else
						{
							if(!indexSchema.Name.Contains("BusquedaRango"))
							{
								strControlesPanelesBusquedas+="\t"+GetDefinicionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+"\r\n"; 
									
								
							}
							else
							{
								strControlesPanelesBusquedas+="\t"+GetDefinicionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Inicial")+"\r\n";
								
								
								//strInitFuncion+=",";
								strControlesPanelesBusquedas+="\t"+GetDefinicionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Final")+"\r\n"; 
								
								
								break;
							}
						}
						count++;
					}
					//strInitFuncion+=")throws Exception;";																																			
				}
			}
			
			strInitFuncion=strTabPanel+strPanelesBusquedas+strControlesPanelesBusquedas;	
			
			return strInitFuncion; 
		}
		
		public String GetIndicesInicializacionVariablesTablasClasesC(TableSchema TablaBase) 
		{
			String strFuncion=string.Empty;
			String strTablaClaseRelacionada=string.Empty;
			String strInitFuncion="\r\n";
			bool blnExisteBusqueda=false;
			
			String strTabPanel=string.Empty;
			String strPanelesBusquedas=string.Empty;
			String strControlesPanelesBusquedas=string.Empty;
			
			foreach(IndexSchema indexSchema in TablaBase.Indexes)
			{
				if(!indexSchema.IsPrimaryKey)
				{
					
					if(indexSchema.IsUnique)
					{
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+indexSchema.Name+"(";
					}
					else
					{
						
						if(!blnExisteBusqueda)
						{
							blnExisteBusqueda=true;
							strTabPanel="\r\n\t\tjTabbedPaneBusquedas"+GetNombreClaseC(TablaBase.ToString())+"=new JTabbedPane();\r\n";
							strTabPanel+="\r\n\t\tjTabbedPaneBusquedas"+GetNombreClaseC(TablaBase.ToString())+".setBorder(javax.swing.BorderFactory.createTitledBorder(\"Busquedas\"));\r\n";
							
						}
						
						if(indexSchema.Name.Contains("Busqueda")||indexSchema.Name.Contains("FK"))
						{
							strPanelesBusquedas+="\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"=new JPanel();\r\n";
							strPanelesBusquedas+="\t\tjButton"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"= new JButton();\r\n";
 							strPanelesBusquedas+="\t\tjButton"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+".setText(\""+indexSchema.Name+"\");\r\n";
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+"(String strFinalQuery,";
						}
						else
						{
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+"(";
						}
					}	
					int count=1;	
					foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
					{
						
						
						if(!(indexSchema.Name.Contains("BusquedaMayor")||indexSchema.Name.Contains("BusquedaMayorIgual")||indexSchema.Name.Contains("BusquedaMenor")||indexSchema.Name.Contains("BusquedaMenorIgual")||indexSchema.Name.Contains("BusquedaRango")||indexSchema.Name.Contains("BusquedaRangoIgual")))
						{
							strControlesPanelesBusquedas+="\t"+GetInicializacionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+"\r\n"; 
							
								
							if(!indexSchema.MemberColumns.Count.Equals(count))
							{
								//strInitFuncion+=";\r\n";	
							}
						}
						else
						{
							if(!indexSchema.Name.Contains("BusquedaRango"))
							{
								strControlesPanelesBusquedas+="\t"+GetInicializacionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+"\r\n"; 
								
								
							}
							else
							{
								strControlesPanelesBusquedas+="\t"+GetInicializacionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Inicial")+"\r\n";
								
								
								//strInitFuncion+=",";
								strControlesPanelesBusquedas+="\t"+GetInicializacionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Final")+"\r\n"; 
								
								
								break;
							}
						}
						count++;
					}
					//strInitFuncion+=")throws Exception;";																																			
				}
			}
			
			strInitFuncion=strTabPanel+strPanelesBusquedas+strControlesPanelesBusquedas;	
			
			return strInitFuncion; 
		}
		
		public String GetIndicesSetVariablesToPanelsTablasClasesC(TableSchema TablaBase) 
		{
			
			
			String strFuncion=string.Empty;
			String strTablaClaseRelacionada=string.Empty;
			String strInitFuncion="\r\n";
			bool blnExisteBusqueda=false;
			
			String strAddTabPanel=string.Empty;
			String strTabPanel=string.Empty;
			String strPanelesBusquedas=string.Empty;
			String strControlesPanelesBusquedas=string.Empty;
			
			String strinitialHorizontalGroup=string.Empty;
			String strinitialVerticalGroup=string.Empty;
			String strFinallHorizontalGroup=string.Empty;
			String strFinalVerticalGroup=string.Empty;
			String strComponentsHorizontalGroup=string.Empty;
			String strComponentsVerticalGroup=string.Empty;
			String strGroup=string.Empty;
			String strTituloBusqueda="";
			
			foreach(IndexSchema indexSchema in TablaBase.Indexes)
			{
				if(!indexSchema.IsPrimaryKey)
				{
					
					strinitialHorizontalGroup="\r\n\t\tGroupLayout jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout = new GroupLayout(jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+");\r\n";
					strinitialHorizontalGroup+="\r\n\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.setAutoCreateContainerGaps(true);\r\n";
        			strinitialHorizontalGroup+="\r\n\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.setAutoCreateGaps(true);\r\n";
					strinitialHorizontalGroup+="\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+".setLayout(jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout);\r\n";
					strinitialHorizontalGroup+="\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.setHorizontalGroup(\r\n";
					strinitialHorizontalGroup+="\t\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createParallelGroup(Alignment.LEADING)\r\n";
					strinitialHorizontalGroup+="\t\t\t.addGroup(Alignment.TRAILING, jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createSequentialGroup()\r\n";
					strinitialHorizontalGroup+="\t\t\t\t.addGroup(jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createParallelGroup(Alignment.TRAILING)\r\n";
					strinitialHorizontalGroup+="\t\t\t\t\t.addComponent(jButton"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+")\r\n";
					strinitialHorizontalGroup+="\t\t\t\t\t.addGroup(jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createSequentialGroup()\r\n";
					
					
					strFinallHorizontalGroup="\t\t\t\t))\r\n";
					strFinallHorizontalGroup+="\t\t\t)\r\n";
					strFinallHorizontalGroup+="\t\t);\r\n";
					
					strinitialVerticalGroup="\r\n\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.setVerticalGroup(\r\n";
					strinitialVerticalGroup+="\r\n\t\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createParallelGroup(Alignment.LEADING)\r\n";
					strinitialVerticalGroup+="\r\n\t\t\t.addGroup(jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createSequentialGroup()\r\n";
					strinitialVerticalGroup+="\r\n\t\t\t.addGroup(jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"Layout.createParallelGroup(Alignment.BASELINE)\r\n";
			
					strFinalVerticalGroup="\r\n\t\t\t\t\t)\r\n";								
					strFinalVerticalGroup+="\r\n\t\t\t\t\t\t\t.addComponent(jButton"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+")\r\n";
					strFinalVerticalGroup+="\r\n\t\t\t\t\t\t\t)\r\n";
					strFinalVerticalGroup+="\r\n\t\t\t\t\t);\r\n";
					
					if(indexSchema.IsUnique)
					{
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+indexSchema.Name+"(";
						continue;
					}
					else
					{
						
						if(!blnExisteBusqueda)
						{
							blnExisteBusqueda=true;
							//strTabPanel="\r\n\t\tjTabbedPaneBusquedas"+GetNombreClaseC(TablaBase.ToString())+"=new JTabbedPane();\r\n";
						}
						
						if(indexSchema.Name.Contains("Busqueda")||indexSchema.Name.Contains("FK"))
						{
							//strPanelesBusquedas+="\t\tjPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+"=new JPanel();\r\n";
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+"(String strFinalQuery,";
						}
						else
						{
						//strInitFuncion+="\r\n\tvoid "+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+"(";
						}
					}	
					strComponentsHorizontalGroup="";
					strComponentsVerticalGroup="";
					int count=1;	
					strTituloBusqueda="";
					foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
					{
						
						
						if(!(indexSchema.Name.Contains("BusquedaMayor")||indexSchema.Name.Contains("BusquedaMayorIgual")||indexSchema.Name.Contains("BusquedaMenor")||indexSchema.Name.Contains("BusquedaMenorIgual")||indexSchema.Name.Contains("BusquedaRango")||indexSchema.Name.Contains("BusquedaRangoIgual")))
						{
							//strControlesPanelesBusquedas+="\t"+GetInicializacionControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+"\r\n"; 
							
							
							strComponentsHorizontalGroup+=".addComponent("+GetNombreTituloControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
							strComponentsHorizontalGroup+=".addComponent("+GetNombreControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE,  GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
							
							strComponentsVerticalGroup+=".addComponent("+GetNombreTituloControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
							strComponentsVerticalGroup+=".addComponent("+GetNombreControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
							
							strTituloBusqueda+=strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" ";
							
							if(!indexSchema.MemberColumns.Count.Equals(count))
							{
								//strInitFuncion+=";\r\n";	
							}
						}
						else
						{
							if(!indexSchema.Name.Contains("BusquedaRango"))
							{
								strComponentsHorizontalGroup+=".addComponent("+GetNombreTituloControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
								strComponentsHorizontalGroup+=".addComponent("+GetNombreControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
								
								strComponentsVerticalGroup+=".addComponent("+GetNombreTituloControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
								strComponentsVerticalGroup+=".addComponent("+GetNombreControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
							
								strTituloBusqueda+=strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" ";
							}
							else
							{
								strComponentsHorizontalGroup+=".addComponent("+GetNombreTituloControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Inicial")+", GroupLayout.PREFERRED_SIZE,GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
								strComponentsHorizontalGroup+=".addComponent("+GetNombreControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Inicial")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
								
								strTituloBusqueda+=strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" INICIAL ";
								
								strComponentsVerticalGroup+=".addComponent("+GetNombreTituloControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Final")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
								strComponentsVerticalGroup+=".addComponent("+GetNombreControlVariablesSwingC(memberColumnSchema.Column,indexSchema.Name,"Final")+",GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)\r\n";
							
								strTituloBusqueda+=strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" FINAL ";
								
								break;
							}
						}
						count++;
					}
					//strInitFuncion+=")throws Exception;";	
					
					
					strAddTabPanel="jTabbedPaneBusquedas"+GetNombreClaseC(TablaBase.ToString())+".addTab(\""+strTituloBusqueda+"\", jPanel"+indexSchema.Name+GetNombreClaseC(TablaBase.ToString())+");\r\n";
					
					strGroup+=strinitialHorizontalGroup+strComponentsHorizontalGroup+strFinallHorizontalGroup+strinitialVerticalGroup+strComponentsVerticalGroup+strFinalVerticalGroup+strAddTabPanel;
					
				}
			}
			
			//strInitFuncion=strTabPanel+strPanelesBusquedas+strControlesPanelesBusquedas;	
			
			return strGroup; 
		}
		
		public String GetElementosSwingTablaClaseC(TableSchema TablaBase) 
		{
			String strSwing="";
			
			for (int i = 0; i < TablaBase.Columns.Count; i++)
			{
				strSwing+=GetControlVariablesSwingC(TablaBase.Columns[i]);
			}			
		
			return strSwing;
		}
		
		public String GetWebRowPaginacionYNuevoTablaClaseC(TableSchema TablaBase,bool esMantenimientoSimple,bool esParaForeignKey,bool esParaReportes,bool ConFaces) 
		{
			String strHtml="";
			
			if(ConFaces)
			{
				strHtml+="\r\n\t\t<h:form>";
			}
			
			strHtml+="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Paginacion\">";
			strHtml+="\r\n\t\t\t<td align=\"center\">";
			strHtml+="\r\n\t\t\t\t<table width=\"100\"  align=\""+GetAlignTableFromPropertiesC(TablaBase)+"\">";
			strHtml+="\r\n\t\t\t\t\t<tr>";
			strHtml+="\r\n\t\t\t\t\t\t<td align=\"center\">";
			
			String strObjectFace="";
			String strValueFace="";
				
			if(ConFaces)
			{
				strObjectFace=GetNombreClaseObjetoC(TablaBase.ToString())+strPrefijoJSFFaces;
			}
			
			if(!ConFaces)
			{
				strHtml+="<br><a:widget id=\"btndjdjtAnteriores"+GetNombreClaseC(TablaBase.ToString())+"\" name=\"dojo.dijit.button\" value=\"{label: '<<'}\" />";
			}
			else
			{
				
				strHtml+="<br><h:commandButton id=\"btndjdjtAnteriores"+GetNombreClaseC(TablaBase.ToString())+"\" action=\"#{"+strObjectFace+".Anteriores}\" value=\"Ant\" />";
			}
			
			
			strHtml+="\r\n\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t<td align=\"center\">";
			
			
			if(!ConFaces)
			{
				strHtml+="<br><a:widget id=\"btndjdjtSiguientes"+GetNombreClaseC(TablaBase.ToString())+"\" name=\"dojo.dijit.button\" value=\"{label: '>>'}\" />";
			}
			else
			{
				strHtml+="<br><h:commandButton id=\"btndjdjtSiguientes"+GetNombreClaseC(TablaBase.ToString())+"\" action=\"#{"+strObjectFace+".Siguientes}\"  value=\"Sig\" />";
			}
			
			strHtml+="\r\n\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t</tr>";
			
			if(!esParaForeignKey&&!esParaReportes&&GetPermiteInsertarFromPropertiesC(TablaBase))
			{
				strHtml+="\r\n\t\t\t\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Nuevo\" height=\"10\">";
			} 
			else
			{
				strHtml+="\r\n\t\t\t\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Nuevo\" height=\"10\" style=\"display:none\">";
			} 
			
			strHtml+="\r\n\t\t\t\t\t\t<td align=\"center\">";
			
			
			if(!ConFaces)
			{
				if(esMantenimientoSimple)
				{
					strHtml+="<img id=\"imgNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"visibility:hidden\" src=\""+GetRelativePathC(TablaBase)+"Imagenes/insertar.gif\" width=\"20\" height=\"20\"  onclick=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"PaginaWebInteraccion.Nuevo"+GetNombreClaseC(TablaBase.ToString())+"Mostrar()\"/>";
				}
				else if(!esParaReportes)
				{
					strHtml+="<img id=\"imgNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" align=\"center\" style=\"visibility:visible\" src=\""+GetRelativePathC(TablaBase)+"Imagenes/insertar.gif\" width=\"20\" height=\"20\"  onclick=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"PaginaWebInteraccionRelacion.Nuevo"+GetNombreClaseC(TablaBase.ToString())+"MostrarRelaciones()\"/>";
				}
			
			}
			else
			{
				if(esMantenimientoSimple)
				{
					strHtml+="<h:commandButton id=\"imgNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" action=\"#{"+strObjectFace+".NuevoPreparar}\" image=\""+GetRelativePathC(TablaBase)+"Imagenes/insertar.gif\" onclick=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"PaginaWebInteraccion.Nuevo"+GetNombreClaseC(TablaBase.ToString())+"Mostrar()\"/>";
				}
				else if(!esParaReportes)
				{
					strHtml+="<h:commandButton id=\"imgNuevo"+GetNombreClaseC(TablaBase.ToString())+"\" action=\"#{"+strObjectFace+".NuevoPrepararRelaciones}\" image=\""+GetRelativePathC(TablaBase)+"Imagenes/insertar.gif\" onclick=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"PaginaWebInteraccionRelacion.Nuevo"+GetNombreClaseC(TablaBase.ToString())+"MostrarRelaciones()\"/>";
				}
			}
			
			
			strHtml+="\r\n\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t</tr>";
			strHtml+="\r\n\t\t\t\t</table>";
			strHtml+="\r\n\t\t\t</td>";
			strHtml+="\r\n\t\t</tr>";
			
			if(ConFaces)
			{
				strHtml+="\r\n\t\t</h:form>";
			}
			
			if(!esMantenimientoSimple&&!esParaReportes)
			{
				strHtml+="\r\n\t\t<tr class=\"busquedacabecera\">";
				strHtml+="\r\n\t\t\t<td>";
				strHtml+="\r\n\t\t\t\t<img id=\"imgExpandirContraerRowElementos"+GetNombreClaseC(TablaBase.ToString())+"\" align=\"left\" src=\""+GetRelativePathC(TablaBase)+"Imagenes/xcollapse.png\" width=\"20\" height=\"20\"  onclick=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"PaginaWebInteraccionRelacion.MostrarOcultarFilas"+GetNombreClaseC(TablaBase.ToString())+"()\"><h1>"+GetTituloNombreTableFromPropertiesC(TablaBase)+"</h1>";
				strHtml+="\r\n\t\t\t</td>";
				strHtml+="\r\n\t\t</tr>";
			}
		
		return strHtml;
		}
		
		public String GetWebRowTablaDatosTablaClaseC(TableSchema TablaBase,bool esMantenimientoSimple,bool esParaForeignKey,bool esParaReportes,bool esMantenimientoDeImagen,bool ConFaces) 
		{
			String strHtml="";
			
			String strPrefijo="";
			
			if(ConFaces)
			{
				strPrefijo=strPrefijoFace;
			}
			
			strHtml+="\r\n<tr><td align=\"left\"><A name=\"TablaIzquierda"+GetNombreClaseC(TablaBase.ToString())+"\"></A><img id=\"imgTablaParaDerecha"+GetNombreClaseC(TablaBase.ToString())+"\" src=\""+GetRelativePathC(TablaBase)+"Imagenes/expand.gif\" width=\"15\" height=\"15\"  onclick=\"document.location.href='Mantenimiento"+GetNombreClaseC(TablaBase.ToString())+strPrefijo+".jsp#TablaDerecha"+GetNombreClaseC(TablaBase.ToString())+"'\"/></td><td colspan=\"2\" align=\"right\"><img id=\"imgTablaParaIzquierda"+GetNombreClaseC(TablaBase.ToString())+"\" src=\""+GetRelativePathC(TablaBase)+"Imagenes/collapse.gif\" width=\"15\" height=\"15\"  onclick=\"document.location.href='Mantenimiento"+GetNombreClaseC(TablaBase.ToString())+strPrefijo+".jsp#TablaIzquierda"+GetNombreClaseC(TablaBase.ToString())+"'\"/><A name=\"TablaDerecha"+GetNombreClaseC(TablaBase.ToString())+"\"></A></td></tr>\r\n";
			
			
			strHtml+="<tr>";
			
			if(!esParaForeignKey)
			{
				strHtml+="<td colspan=\"3\">";
			} 
			else
			{
				strHtml+="<td colspan=\"3\">";
			} 
			
			
			if(!ConFaces)
			{
				strHtml+="<a:widget id=\"djtbl"+GetNombreClaseC(TablaBase.ToString())+"\" name=\"dojo.table\" value=\"{columns:[";   
				
				if(!esParaReportes)
				{
					if(!esParaForeignKey)
					{
						strHtml+="{ label :'', id :'"+strEdicion+"'},";
					}
					else
					{
						strHtml+="{ label :'', id :'"+strSeleccion+"'},";
					}
				}
				else
				{
					strHtml+="{ label :'', id :'id'},";
				}
				
				for (int i = 0; i < TablaBase.Columns.Count; i++)
				{
				
					strHtml+=GetColumnsTableMaintenanceC(TablaBase.Columns[i],esMantenimientoDeImagen);
				
					if((i!=TablaBase.Columns.Count-2)&&TablaBase.Columns[i].Name!=strId&&TablaBase.Columns[i].Name!=strIsActive&&TablaBase.Columns[i].Name!=strIsExpired&&TablaBase.Columns[i].Name!=strVersionRow)
					{
						strHtml+=",";
					} 
				}
				
				if(esMantenimientoSimple)
				{
					strHtml+=GetNavegacionTituloTablaTablasClasesRelacionadasC(TablaBase);
					strHtml+=GetActionsTableMaintenanceC(TablaBase);
				}
				
				strHtml+="],rows : [";			
				strHtml+="]}\"/>";
			}
			else
			{
				String strObjectFace="";
				
				strObjectFace=GetNombreClaseObjetoC(TablaBase.ToString())+strPrefijoJSFFaces;
				
				strHtml+="\r\n\t\t\t\t<h:form>\r\n\t\t\t\t<h:dataTable rendered=\"#{"+strObjectFace+"."+GetNombreClaseObjetoC(TablaBase.ToString())+"sModel.rowCount > 0}\" value=\"#{"+strObjectFace+"."+GetNombreClaseObjetoC(TablaBase.ToString())+"sModel}\" var=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"\">";
				
				
				for (int i = 0; i < TablaBase.Columns.Count; i++)
				{
				
					strHtml+=GetColumnsFacesTableMaintenanceC(TablaBase.Columns[i],esMantenimientoDeImagen);
				
					/*
					if((i!=TablaBase.Columns.Count-1)&&TablaBase.Columns[i].Name!=strId&&TablaBase.Columns[i].Name!=strIsActive&&TablaBase.Columns[i].Name!=strIsExpired&&TablaBase.Columns[i].Name!=strVersionRow)
					{
						strHtml+=",";
					} 
					*/
				}
				
				if(esMantenimientoSimple)
				{
					strHtml+=GetNavegacionTituloTablaFacesTablasClasesRelacionadasC(TablaBase);
				}
				
				
				strHtml+="\r\n\t\t\t\t</h:dataTable>\r\n\t\t\t\t</h:form>";
			}
			
			strHtml+="</td>";   
			strHtml+="</tr>";
			
			return strHtml;
		}
		
		public String GetWebRowParametrosBusquedaTablaClaseC(TableSchema TablaBase,bool esMantenimientoSimple,bool esParaForeignKey,bool esParaReportes,bool esBusquedaDesdeForeignKey,bool ConFaces) 
		{
			String strHtml="";
			strHtml+="<tr class=\"busqueda\">";
			strHtml+="\r\n\t\t\t<td>";
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t<form name=\"frmParametrosBusqueda"+GetNombreClaseC(TablaBase.ToString())+"\">";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t<h:form id=\"frmParametrosBusqueda"+GetNombreClaseC(TablaBase.ToString())+"\">";
			}
			
			
			strHtml+="\r\n\t\t\t\t\t<table id=\"tblParametrosBusquedaNumeroRegistros"+GetNombreClaseC(TablaBase.ToString())+"\" align=\"left\">";
			strHtml+="\r\n\t\t\t\t\t\t<tr id=\"trParametrosBusquedaNumeroRegistros"+GetNombreClaseC(TablaBase.ToString())+"\" style=\"display:none\">";
			strHtml+="\r\n\t\t\t\t\t\t\t<td>";
			strHtml+="<h1>"+strNumeroDe +GetTituloNombreTableFromPropertiesC(TablaBase)+GetPluralTituloNombreTableFromPropertiesC(TablaBase)+strAProcesar+"</h1>";
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t\t<td colspan=\"2\">";
			
			if(!ConFaces)
			{
				strHtml+="<input name=\"txtNumeroRegistros"+GetNombreClaseC(TablaBase.ToString())+"\" type=\"text\" size=\"5\" value=\"10\" onBlur=\""+GetNombreClaseObjetoC(TablaBase.ToString())+"FuncionGeneral.ValidarFormularioParametrosNumeroRegistros"+GetNombreClaseC(TablaBase.ToString())+"()\">";
			}
			else
			{
				strHtml+="<h:inputText id=\"txtNumeroRegistros"+GetNombreClaseC(TablaBase.ToString())+"\" size=\"5\" value=\"10\" />";
			}
			
			
			strHtml+="\r\n\t\t\t\t\t\t\t</td>";
			strHtml+="\r\n\t\t\t\t\t\t</tr>";
			
			strHtml+="\r\n\t\t<tr id=\"trRecargarInformacion"+GetNombreClaseC(TablaBase.ToString())+"\">";
			
			
			String strObjectFace="";
			
			if(ConFaces)
			{
				strObjectFace=GetNombreClaseObjetoC(TablaBase.ToString())+strPrefijoJSFFaces;
			}
			
			
			String strRecargarInformacionVisibility="";
			
			if(GetEsReporteFromPropertiesC(TablaBase))
			{
				strRecargarInformacionVisibility="style=\"display:none\"";
			}
			
			
			if(!GetEsInternoFromPropertiesC(TablaBase)||esBusquedaDesdeForeignKey)
			{
				if(!ConFaces)
				{
					strHtml+="\r\n\t\t<td "+strRecargarInformacionVisibility+"><br><br><a:widget id=\"btndjdjtRecargarInformacion"+GetNombreClaseC(TablaBase.ToString())+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strRecargarInformacion+"'}\" />";
				}
				else
				{
					strHtml+="\r\n\t\t<td "+strRecargarInformacionVisibility+"><br><br><h:commandButton id=\"btndjdjtRecargarInformacion"+GetNombreClaseC(TablaBase.ToString())+"\"  action=\"#{"+strObjectFace+".RecargarInformacion}\" value=\""+strRecargarInformacion+"\" />";
					
				}
				
				
				strHtml+="\r\n\t\t</td>";
			}
			
			if((esMantenimientoSimple&&!esParaForeignKey)||esParaReportes)
			{
								
				strHtml+="\r\n\t\t<td  class=\"elementos\">";
				
				if(!ConFaces)
				{
					strHtml+="\r\n\t\t<h2>"+strGenerarReporte+"</h2><input type=\"checkbox\" id=\"chbGenerarReporte\" name=\"chbGenerarReporte\"><a:widget id=\"djcmbGenerarReporte\" name=\"dojo.combobox\"/>";
				}
				else
				{
					strHtml+="\r\n\t\t<h2>"+strGenerarReporte+"</h2><h:selectBooleanCheckbox id=\"chbGenerarReporte\" value=\"#{"+strObjectFace+".blnGenerarReporte}\"/>\r\n";
					
					strHtml+="\t\t\t<h:selectOneMenu id=\"djcmbGenerarReporte\" value=\"#{"+strObjectFace+".strTipoReporte}\">\r\n";			
					strHtml+="\t\t\t<f:selectItems value=\"#{"+strObjectFace+".tiposReportes}\"/>\r\n";
					strHtml+="\t\t\t</h:selectOneMenu>\r\n";
				}
				
				
				
				strHtml+="\r\n\t\t</td>";
				
				strHtml+="\r\n\t\t<td class=\"elementos\">";
				
				if(!ConFaces)
				{
					strHtml+="\r\n\t\t<h2>"+strGenerarReporteTodos+"</h2><input type=\"checkbox\" id=\"chbGenerarTodos\" name=\"chbGenerarTodos\">";
				}
				else
				{
					strHtml+="\r\n\t\t<h2>"+strGenerarReporteTodos+"</h2><h:selectBooleanCheckbox id=\"chbGenerarTodos\" value=\"#{"+strObjectFace+".blnGenerarTodos}\"/>";
				}
				
				strHtml+="\r\n\t\t</td>";
				
			}
			
			strHtml+="\r\n\t\t</tr>";
			
			
			
			strHtml+="\r\n\t\t\t\t\t</table>";
			
			if(!ConFaces)
			{
				strHtml+="\r\n\t\t\t\t</form>";
			}
			else
			{
				strHtml+="\r\n\t\t\t\t</h:form>";
			}
			
			strHtml+="\r\n\t\t\t</td>";
			strHtml+="\r\n\t\t</tr>";
			
			
			return strHtml;
		}
		
		public String GetWebRowTituloTablaClaseC(TableSchema TablaBase,bool esMantenimientoSimple,bool esParaReportes) 
		{
			String strHtml="";
			
			strHtml+="<tr class=\"cabecera\">";
			strHtml+="\r\n\t\t\t<td>";
			if(!esParaReportes)
			{
				strHtml+="<h1>"+strMantenimientoDe+GetTituloNombreTableFromPropertiesC(TablaBase)+GetPluralTituloNombreTableFromPropertiesC(TablaBase)+"</h1>";
			}
			else
			{
				strHtml+="<h1>"+strReporteDe+GetTituloNombreTableFromPropertiesC(TablaBase)+"</h1>";
			}
			
			strHtml+="\r\n\t\t\t</td>";
			strHtml+="\r\n\t\t</tr>";
			
			return strHtml;
		}
		
		public String GetWebRowControlesBusquedasIndicesTablasClasesC(TableSchema TablaBase,bool esMantenimientoSimple,bool esParaForeignKey,bool esParaReportes,bool ConFaces) 
		{
			String strBusquedasDe="";
			String strBusquedaFK="";
			String strBusquedaFKVisibleCabbecera="";
								
			
			if(esParaForeignKey)
			{
				strBusquedasDe=" "+strDe+" ";
				strBusquedasDe+=GetTituloNombreTableFromPropertiesC(TablaBase)+GetPluralTituloNombreTableFromPropertiesC(TablaBase);
				strBusquedaFKVisibleCabbecera="";
			}
			else
			{
				 strBusquedaFKVisibleCabbecera=" style=\"display:none\" ";
			}
								
			if(!esMantenimientoSimple)
			{
				strBusquedasDe=strDe+GetTituloNombreTableFromPropertiesC(TablaBase)+GetPluralTituloNombreTableFromPropertiesC(TablaBase);
			}
			
			String strHtml=">";
			
			String strRowSearch="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"CabeceraBusquedas\" class=\"busquedacabecera\" "+strBusquedaFKVisibleCabbecera+">\r\n\t\t\t<td>\r\n\t\t\t\t<img id=\"imgExpandirContraerRowBusqueda"+GetNombreClaseC(TablaBase.ToString())+"\" align=\"left\" src=\""+GetRelativePathC(TablaBase)+"Imagenes/xcollapse.png\" width=\"20\" height=\"20\"  onclick=\"funcionGeneral.MostrarOcultarFilaCambiarImagen('tr"+GetNombreClaseC(TablaBase.ToString())+"Busquedas',this,'"+GetRelativePathC(TablaBase)+"')\"><font>"+strBusquedas+strBusquedasDe+"</font>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n";
			
			String strhtmlformularioinicial="";
			
			String strFormInicial="";
			String strFormFinal="";
			
			if(!ConFaces)
			{
				strFormInicial="<form name=\"frmBusqueda"+GetNombreClaseC(TablaBase.ToString())+"\">";
				strFormFinal="</form>";
			}
			else
			{
				strFormInicial="<h:form id=\"frmBusqueda"+GetNombreClaseC(TablaBase.ToString())+"\">";
				strFormFinal="</h:form>";
			}
			
			
			
			String strHtmlInicial0="\r\n\t\t<tr id=\"tr"+GetNombreClaseC(TablaBase.ToString())+"Busquedas\" class=\"busqueda\" style=\"display:none\">\r\n\t\t\t<td align=\"center\">\r\n\t\t\t\t<A name=\"Busquedas\"></A>\r\n\r\n\t\t\t\t"+strFormInicial+"\r\n\r\n\t\t\t\t<table width=\""+GetWidthBusquedaTableFromPropertiesC(TablaBase)+"%\" align=\"left\" style=\"visibility:visible\">";
			String strHtmlFinal0="\r\n\t\t\t\t</table>\r\n\t\t\t\t"+strFormFinal+"\r\n\r\n\t\t\t</td>\r\n\t\t</tr>";
			
			String strHtmlInicial="\r\n<tr class=\"busqueda\">\r\n<td align=\"center\">\r\n<A name=\"Busquedas\"></A>\r\n<table width=\"100%\" align=\"center\" style=\"visibility:visible\">";
			String strHtmlFinal="\r\n\t</tr>\r\n</table>\r\n</td>\r\n</tr>";
			
			String strHtmlFormularioFinal="";
			String strTituloBusqueda="";
			String strTituloBusquedaInicial="\r\n\t\t\t\t\t<tr class=\"busquedatitulo\">\r\n\t\t\t\t\t\t<td colspan=\"3\" align=\"left\" class=\"busquedatitulo\">\r\n\t\t\t\t\t\t\t<font>"+strBusqueda;
			String strTituloBusquedaMiddle="";
			String strTituloBusquedaFinal="</font>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>";
			String strTitulo=string.Empty;
			String strComboText=string.Empty;
			String strBoton=string.Empty;
			String strBotonBusqueda=string.Empty;
			String strCombo=string.Empty;
			
			String strFuncion=string.Empty;
			bool existe=false;
			
			
			String strTablaClaseRelacionada=string.Empty;
			String strInitFuncion="";
			
			
			String strObjectFace="";
			
			if(ConFaces)
			{
				strObjectFace=GetNombreClaseObjetoC(TablaBase.ToString())+strPrefijoJSFFaces;
			}
			
			if(!esParaReportes)
			{
				foreach(IndexSchema indexSchema in TablaBase.Indexes)
				{
					
					if(!VerificarIndiceBusquedaTablaC(TablaBase,indexSchema.Name))
					{
						continue;
					}
					
					if(!indexSchema.IsPrimaryKey)
					{
						
						if(indexSchema.IsUnique)
						{
							continue;
																						//strTitulo=GetNombreClaseC(TablaBase.ToString())+"</td>";
							//strBoton="\r\n\t\t<td width=\"11%\"><input type=\"button\" value=\"Buscar\" onclick=\"PaginaWebInteraccion"+  GetNombreClaseC(TablaBase.ToString())+"Buscar"+GetNombreClaseC(TablaBase.ToString())+"s('"+indexSchema.Name+ "')\">\r\n\t\t</td>";
																						//strCombo="<td width=\"79%\"><a:widget id=\"djcmb"+  GetNombreClaseC(TablaBase.ToString())+"\" name=\"dojo.combobox\"/></td>";					
					
																						//strInitFuncion+=GetNombreClaseC(TablaBase.ToString())+" "+"Traer"+GetNombreClaseC(TablaBase.ToString())+indexSchema.Name+"(";
						}
						else
						{
							existe=true;
							//strTitulo=GetNombreClaseC(TablaBase.ToString())+"s</td>";
							//strBoton="\r\n\t\t\t\t\t\t<td width=\"11%\">\r\n\t\t\t\t\t\t\t<input type=\"button\" value=\"Buscar\" onclick=\"PaginaWebInteraccion"+  GetNombreClaseC(TablaBase.ToString())+"Buscar"+GetNombreClaseC(TablaBase.ToString())+"s('"+indexSchema.Name+ "')\">\r\n\t\t\t\t\t\t</td>";
							
							if(!ConFaces)
							{
								strBotonBusqueda="<a:widget id=\"btndjdjtBuscar"+GetNombreClaseC(TablaBase.ToString())+indexSchema.Name+"\" name=\"dojo.dijit.button\" value=\"{label: '"+strBuscar+"'}\" />";
								
							}
							else
							{
								strBotonBusqueda="<h:commandButton id=\"btndjdjtBuscar"+GetNombreClaseC(TablaBase.ToString())+indexSchema.Name+"\"  action=\"#{"+strObjectFace+"."+"Get"+GetNombreClaseC(TablaBase.ToString())+"s"+indexSchema.Name+""+"}\" value=\""+strBuscar+"\"/>";
								
							}
			
							strBoton="\r\n\t\t\t\t\t\t<td width=\"11%\">\r\n\t\t\t\t\t\t\t"+strBotonBusqueda+"\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t</tr>";
							
							strComboText="";
							strTitulo="";
							strTituloBusquedaMiddle="";
							
							if(indexSchema.Name.Contains(strFK))
							{
									
								TableSchema tableRelacionada=GetNombreTablaRelacionadaFromColumn(indexSchema.MemberColumns[0]);
								
								if(tableRelacionada.Name!=TablaBase.Name)
								{
									if(GetBusquedaForeignKeyColumnFromPropertiesC(indexSchema.MemberColumns[0]))
									{   
										strBusquedaFK+="\r\n\t\t<tr>\r\n\t<td>\r\n\t<table width=\"100%\"  align=\"center\"  bgcolor=\""+strColorBusquedaAnidada+"\">";
										strBusquedaFK+=GetWebRowControlesBusquedasIndicesTablasClasesC(tableRelacionada,true,true,false,ConFaces);
										strBusquedaFK+=GetWebRowParametrosBusquedaTablaClaseC(tableRelacionada,true,true,false,true,ConFaces);
										strBusquedaFK+=GetWebRowTablaDatosTablaClaseC(tableRelacionada,true,true,false,false,ConFaces);
										strBusquedaFK+=GetWebRowPaginacionYNuevoTablaClaseC(tableRelacionada,true,true,false,ConFaces);								
										strBusquedaFK+="\r\n\t\t</table>\r\n\t</td>\r\n\t</tr>";
									}
								}
								//strTitulo+=strBusquedaFK;
								
								strTitulo+="\r\n\t\t\t\t\t\t<td width=\"10%\" class=\"busquedatitulocampo\"><h2>"+ GetWebNombreTituloColumnFromPropertiesC(indexSchema.MemberColumns[0])+"</h2>\r\n\t\t\t\t\t\t</td>";
								
								if(!ConFaces)
								{
									strCombo="<a:widget id=\""+   GetNameControlHtmlBusqueda(indexSchema.MemberColumns[0],indexSchema.Name)+"\" name=\"dojo.combobox\"/>";									
								}
								else
								{
									strCombo="<h:selectOneMenu id=\""+   GetNameControlHtmlBusqueda(indexSchema.MemberColumns[0],indexSchema.Name)+"\" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(indexSchema.MemberColumns[0])+GetNombreColumnaClaseC(indexSchema.MemberColumns[0])+indexSchema.Name+"}\"> \r\n";					
									strCombo+="<f:selectItems value=\"#{"+strObjectFace+"."+GetNombreClaseRelacionadaFromColumn(indexSchema.MemberColumns[0]).ToLower() +"s"+strForeignKey+"ListSelectItem}\"/>\r\n";
									strCombo+="</h:selectOneMenu>";
								}
							
								strComboText="\r\n\t\t\t\t\t\t<td width=\"79%\" align=\"center\">\r\n\t\t\t\t\t\t\t"+strCombo+"\r\n\t\t\t\t\t\t</td>";								
								strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(indexSchema.MemberColumns[0]);
								strTituloBusqueda=strTituloBusquedaInicial+strTituloBusquedaMiddle+ strTituloBusquedaFinal;
							}
						}	
						
						if(!indexSchema.Name.Contains("FK"))
						{
							strComboText="";
							strTitulo="";
							strTituloBusquedaMiddle="";
							int count=1;	
							foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
							{
								if(!(indexSchema.Name.Contains("BusquedaMayor")||indexSchema.Name.Contains("BusquedaMayorIgual")||indexSchema.Name.Contains("BusquedaMenor")||indexSchema.Name.Contains("BusquedaMenorIgual")||indexSchema.Name.Contains("BusquedaRango")||indexSchema.Name.Contains("BusquedaRangoIgual")))
								{
									//strTitulo
									strComboText+="\r\n\t\t\t\t\t\t<td width=\"10%\" class=\"busquedatitulocampo\"><h2>"+ GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+"</h2>\r\n\t\t\t\t\t\t</td>";
									strComboText+="\r\n\t\t\t\t\t\t<td width=\"79%\" align=\"center\">\r\n\t\t\t\t\t\t\t"+ GetControlHtmlBusqueda( memberColumnSchema.Column,indexSchema.Name,ConFaces)+"\r\n\t\t\t\t\t\t</td>";
									strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column);
								}
								else
								{
									if(!indexSchema.Name.Contains("BusquedaRango"))
									{
										//strTitulo
										strComboText+="\r\n\t\t\t\t\t\t<td width=\"10%\" class=\"busquedatitulocampo\"><h2>"+ GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+"</h2>\r\n\t\t\t\t\t\t</td>";
										strComboText+="\r\n\t\t\t\t\t\t<td width=\"79%\" align=\"center\">\r\n\t\t\t\t\t\t"+ GetControlHtmlBusqueda( memberColumnSchema.Column,indexSchema.Name,ConFaces)+"\r\n\t\t\t\t\t\t</td>";
										
										if(indexSchema.Name.Contains("BusquedaMayor"))
										{
											strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" Inicial";
										}
										else if(indexSchema.Name.Contains("BusquedaMenor"))
										{
											strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" Final";
										}
									}
									else
									{
										//strTitulo
										strComboText+="\r\n\t\t\t\t\t\t<td width=\"10%\" class=\"busquedatitulocampo\"><h2>"+ GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" Inicial</h2>\r\n\t\t\t\t\t\t</td>";
										strComboText+="\r\n\t\t\t\t\t\t<td width=\"79%\" align=\"center\">\r\n\t\t\t\t\t\t"+ GetControlHtmlBusqueda( memberColumnSchema.Column,indexSchema.Name,ConFaces).Replace(memberColumnSchema.Column.Name+indexSchema.Name+"\"",memberColumnSchema.Column.Name+indexSchema.Name+"Inicial\"")+"\r\n\t\t\t\t\t\t</td>";
										
										//strTitulo
										strComboText+="\r\n\t\t\t\t\t\t<td width=\"10%\" class=\"busquedatitulocampo\"><h2>"+ GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" Final</h2>\r\n\t\t\t\t\t\t</td>";
										strComboText+="\r\n\t\t\t\t\t\t<td width=\"79%\" align=\"center\">\r\n\t\t\t\t\t\t\t"+ GetControlHtmlBusqueda( memberColumnSchema.Column,indexSchema.Name,ConFaces).Replace(memberColumnSchema.Column.Name+indexSchema.Name+"\"",memberColumnSchema.Column.Name+indexSchema.Name+"Final\"")+"\r\n\t\t\t\t\t\t</td>";
										
										strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" Inicial";
										strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(memberColumnSchema.Column)+" Final";
										break;
									}
								}
							}
							
							strTituloBusqueda=strTituloBusquedaInicial+strTituloBusquedaMiddle+ strTituloBusquedaFinal;
							
						}
						
						
						strInitFuncion+=strTituloBusqueda+"\r\n\t\t\t\t\t<tr>"+strTitulo+strComboText+strBoton+"\r\n\r\n";
					}
					
				}
			
			}
			else
			{
					strComboText="";
							strTitulo="";
							strTituloBusquedaMiddle="";
							int count=1;	
							bool blExisteParametro=false;
							
							foreach(ColumnSchema columnSchema in TablaBase.Columns)
							{
								if(!columnSchema.Name.Contains(strPrefijoParametro))
								{
									continue;
								}
								else
								{
									blExisteParametro=true;
								}
								strComboText+="\r\n\t\t\t\t\t\t<td width=\"10%\"><h2>"+ GetWebNombreTituloColumnFromPropertiesC(columnSchema)+"</h2>\r\n\t\t\t\t\t\t</td>";
								strComboText+="\r\n\t\t\t\t\t\t<td width=\"79%\" align=\"center\">\r\n\t\t\t\t\t\t\t"+ GetControlHtmlBusqueda( columnSchema,TablaBase.Name,ConFaces)+"\r\n\t\t\t\t\t\t</td>";
								strTituloBusquedaMiddle+=" "+strPor+" "+GetWebNombreTituloColumnFromPropertiesC(columnSchema);
							}
							
							if(!blExisteParametro)
							{
								return "";
							}
							
							strTituloBusqueda=strTituloBusquedaInicial+strTituloBusquedaMiddle+ strTituloBusquedaFinal;
							
						
						strInitFuncion+=strTituloBusqueda+"\r\n\t\t\t\t\t<tr>"+strTitulo+strComboText+strBoton+"\r\n\r\n";
			}
			
			strHtml=strRowSearch+strHtmlInicial0+strhtmlformularioinicial+strInitFuncion +strHtmlFormularioFinal+strHtmlFinal0+"\r\n\r\n";
			
			if(!existe)
			{
				strHtml="";
			}
								
			return strHtml+strBusquedaFK; 
		}
		
		public String GetWebRowControlesBusquedasIndicesTablasClasesC(TableSchema TablaBase,String strIncludeInit,String strIncludeEnd) 
		{
			String strBusquedaFK="";
			
				foreach(IndexSchema indexSchema in TablaBase.Indexes)
				{
				
					if(!VerificarIndiceBusquedaTablaC(TablaBase,indexSchema.Name))
					{
						continue;
					}
					
					if(!indexSchema.IsPrimaryKey)
					{						
						if(indexSchema.IsUnique)
						{
							continue;
						}
						else
						{
							
							if(indexSchema.Name.Contains(strFK))
							{
							
								TableSchema tableRelacionada=GetNombreTablaRelacionadaFromColumn(indexSchema.MemberColumns[0]);						
								
								if(GetBusquedaForeignKeyColumnFromPropertiesC(indexSchema.MemberColumns[0]))
								{									
									strBusquedaFK+=strIncludeInit+"\""+GetRelativePathC(TablaBase)+"JavaScript/WebPageInteraction/"+GetNombreClaseC(tableRelacionada.ToString())+"WebPageInteractionEvents.js\""+strIncludeEnd;
								}								
							}
						}	
					}
					
				}
			
					
			return strBusquedaFK; 
		}
		
		public String GetSetUnicoComboFromForeignKeysC(TableSchema TablaBase) 
		{
			String strBusquedaFK="";
			
				foreach(IndexSchema indexSchema in TablaBase.Indexes)
				{
				
					if(!VerificarIndiceBusquedaTablaC(TablaBase,indexSchema.Name))
					{
						continue;
					}
					
					if(!indexSchema.IsPrimaryKey)
					{						
						if(indexSchema.IsUnique)
						{
							continue;
						}
						else
						{
							
							if(indexSchema.Name.Contains(strFK))
							{
							
								TableSchema tableRelacionada=GetNombreTablaRelacionadaFromColumn(indexSchema.MemberColumns[0]);						
								
								if(GetBusquedaForeignKeyColumnFromPropertiesC(indexSchema.MemberColumns[0]))
								{									
									//strBusquedaFK+="\r\n\t\tfuncionGeneral.Import('JavaScript/WebPageInteraction/"+GetNombreClaseC(tableRelacionada.ToString())+"WebPageInteractionEvents.js');";	
									strBusquedaFK+="\r\n\t\tthis.Bit"+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(indexSchema.MemberColumns[0]))+"UtilizaBusqueda=true;";
									strBusquedaFK+="\r\n\t\t"+GetNombreClaseObjetoC(tableRelacionada.ToString())+"PaginaWebInteraccion.ObjetoServicio="+GetNombreClaseObjetoC(TablaBase.ToString())+"Servicio;";
									strBusquedaFK+="\r\n\t\t"+GetNombreClaseObjetoC(TablaBase.ToString())+"Servicio.Set"+GetNombreClaseC(tableRelacionada.ToString())+"Unico(-1,\"ninguno\");";									
									
								}								
							}
						}	
					}
					
				}
			
					
			return strBusquedaFK; 
		}
		public string GetColumnsTableMaintenanceC(ColumnSchema column,bool esMantenimientoDeImagen)
{
	
	String strTipo=GetTipoColumnaClaseC(column);	
	String strPrefijo=" "+GetPrefijoTipoC(column);	
	String strColumna=GetNombreColumnaClaseC(column);
	
	String strColumnName="";
	String strColumnLabel="'"+GetWebNombreTituloColumnFromPropertiesC(column)+"'";
	String strColumn="";
	
	if(column.Name.Equals(strId)||column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired)|| column.Name.Equals(strVersionRow))
	{
			return string.Empty;
	}
	else
	{
		if(!column.IsForeignKeyMember&&column.DataType!=DbType.Boolean&&column.NativeType!="image"&&column.DataType!=DbType.Binary)
		{
			strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"'";
			strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
		}
		else if(column.DataType==DbType.Boolean)
		{
			strColumna= GetNombreColumnaClaseC(column);
			strColumna=strColumna.Replace(strId,"");
			strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
			
			strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Control'";
			strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";	
		}
		else if(column.IsForeignKeyMember)
		{
			strColumna= GetNombreColumnaClaseC(column);
			strColumna=strColumna.Replace(strId,"");
			strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
			
			strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Descripcion'";
			strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
		}
		else if((column.NativeType=="image"&&column.DataType==DbType.Binary&&column.Name!=strVersionRow)||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
		{
			
			if(esMantenimientoDeImagen)
			{
				strColumna= GetNombreColumnaClaseC(column);
				strColumna=strColumna.Replace(strId,"");
				strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
				
				strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Mostrar'";
				strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
			}
			else
			{
				strColumna= GetNombreColumnaClaseC(column);
				strColumna=strColumna.Replace(strId,"");
				strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
				
				strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Actualizar'";
				strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
			}
		}
	}	
			
	return strColumn;
}

public string GetColumnsFacesTableMaintenanceC(ColumnSchema column,bool esMantenimientoDeImagen)
{
		
	String strTipo=GetTipoColumnaClaseC(column);	
	String strPrefijo=GetPrefijoTipoC(column);	
	String strColumna=GetNombreColumnaClaseC(column);
	
	String strPrefijoCampo="";
	
	strPrefijoCampo=GetPrefijoTablaC().ToLower();
	
	String strColumnName=GetNombreClaseObjetoC(column.Table.ToString())+"."+strPrefijoCampo+strPrefijo+strColumna;
	String strColumnLabel=""+GetWebNombreTituloColumnFromPropertiesC(column)+"";
	String strColumn="";
		
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired)|| column.Name.Equals(strVersionRow))
	{
		return string.Empty;
	}
	else if(column.Name.Equals(strId))
	{
		
		strColumn="\r\n\t\t\t\t\t<h:column>\r\n";
		strColumn+="\t\t\t\t\t\t<f:facet name=\"header\">\r\n\t\t\t\t\t\t\t<h:outputText value=\""+strIdGetSet+"\"/>\r\n\t\t\t\t\t\t</f:facet>\r\n";
		//strColumn+="\r\n\t\t\t\t\t\t<h:outputText value=\"#{"+GetNombreClaseObjetoC(column.Table.ToString())+"."+strId+"}\"/>\r\n";
		strColumn+="\r\n\t\t\t\t\t\t<h:outputText value=\"\"/>\r\n";
		//onclick=\""+GetNombreClaseObjetoC(column.Table.ToString())+"FuncionGeneral.MostrarOcultarControlesMantenimiento(true)\"
		strColumn+="\r\n\t\t\t\t\t\t<h:commandLink action=\"#{"+GetNombreClaseObjetoC(column.Table.ToString())+strPrefijoJSFFaces+".Seleccionar}\" value=\"#{"+GetNombreClaseObjetoC(column.Table.ToString())+"."+strId+"}\" >\r\n";
		strColumn+="\t\t\t\t\t\t\t<f:setPropertyActionListener value=\"#{"+GetNombreClaseObjetoC(column.Table.ToString())+"."+strId+"}\" target=\"#{"+GetNombreClaseObjetoC(column.Table.ToString())+strPrefijoJSFFaces+"."+strId+"}\" />\r\n";
		strColumn+="\t\t\t\t\t\t</h:commandLink>\r\n";
							
		strColumn+="\t\t\t\t\t</h:column>\r\n";
		
		
	}
	else
	{
		if(!column.IsForeignKeyMember&&column.DataType!=DbType.Boolean&&column.NativeType!="image"&&column.DataType!=DbType.Binary)
		{
			//strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"'";
			//strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
				
			strColumn="\r\n\t\t\t\t\t<h:column>\r\n";
			strColumn+="\t\t\t\t\t\t<f:facet name=\"header\">\r\n\t\t\t\t\t\t\t<h:outputText value=\""+strColumnLabel+"\"/>\r\n\t\t\t\t\t\t</f:facet>\r\n";
			strColumn+="\r\n\t\t\t\t\t\t<h:outputText value=\"#{"+strColumnName+"}\"/>\r\n";
			strColumn+="\t\t\t\t\t</h:column>\r\n";
			
		}
		else if(column.DataType==DbType.Boolean)
		{
			/*
			strColumna= GetNombreColumnaClaseC(column);
			strColumna=strColumna.Replace(strId,"");
			strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
				
			strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Control'";
			strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";	
			*/
			strColumn="\r\n\t\t\t\t\t<h:column>\r\n";
			strColumn+="\t\t\t\t\t\t<f:facet name=\"header\">\r\n\t\t\t\t\t\t\t<h:outputText value=\""+strColumnLabel+"\"/>\r\n\t\t\t\t\t\t</f:facet>\r\n";
			strColumn+="\r\n\t\t\t\t\t\t<h:selectBooleanCheckbox value=\"#{"+strColumnName+"}\" disabled=\"true\"/>\r\n";
			strColumn+="\t\t\t\t\t</h:column>\r\n";
			
		}
		else if(column.IsForeignKeyMember)
		{
			strColumna= GetNombreColumnaClaseC(column);
			strColumna=strColumna.Replace(strId,"");
			strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
				
			//strColumnName=/*"'"+*/GetNombreColumnaClaseJavaScriptC(column)/*+"Descripcion'"*/;
			//strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
				
			strColumn="\r\n\t\t\t\t\t<h:column>\r\n";
			strColumn+="\t\t\t\t\t\t<f:facet name=\"header\">\r\n\t\t\t\t\t\t\t<h:outputText value=\""+strColumnLabel+"\"/>\r\n\t\t\t\t\t\t</f:facet>\r\n";
			strColumn+="\r\n\t\t\t\t\t\t<h:outputText value=\"#{"+strColumnName+"}\"/>\r\n";
			strColumn+="\t\t\t\t\t</h:column>\r\n";
			
		}
		else if((column.NativeType=="image"&&column.DataType==DbType.Binary&&column.Name!=strVersionRow)||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
		{
			if(esMantenimientoDeImagen)
			{
				strColumna= GetNombreColumnaClaseC(column);
				strColumna=strColumna.Replace(strId,"");
				strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
					
				strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Mostrar'";
				strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
			}
			else
			{
				strColumna= GetNombreColumnaClaseC(column);
				strColumna=strColumna.Replace(strId,"");
				strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
					
				strColumnName="'"+GetNombreColumnaClaseJavaScriptC(column)+"Actualizar'";
				strColumn="{ label :" +strColumnLabel+", id :"+strColumnName +"}";
			}
		}
	}	
			
	return strColumn;
}

public string GetColumnsTableMaintenanceC(TableSchema table)
{
	
	String strPlural=GetPluralTituloNombreTableFromPropertiesC(table);
	String strColumnName="";
	String strColumnLabel="'"+GetTituloNombreTableFromPropertiesC(table)+strPlural+"'";
	String strColumn="";
		
	strColumnName="'"+GetNombreClaseObjetoC(table.ToString()) +strPlural+"'";
	strColumn=",{ label :" +strColumnLabel+", id :"+strColumnName+"}";
					
	return strColumn;
}

public string GetColumnsFacesTableMaintenanceC(TableSchema table)
{
	
	String strPlural=GetPluralTituloNombreTableFromPropertiesC(table);
	String strColumnName="";
	String strColumnLabel=""+GetTituloNombreTableFromPropertiesC(table)+strPlural+"";
	String strColumn="";
			
	//strColumnName="'"+GetNombreClaseObjetoC(table.ToString()) +strPlural+"'";
	//strColumn=",{ label :" +strColumnLabel+", id :"+strColumnName+"}";
	
	strColumn="\r\n\t\t\t\t\t<h:column>\r\n";
	strColumn+="\t\t\t\t\t\t<f:facet name=\"header\">\r\n\t\t\t\t\t\t\t<h:outputText value=\"" +strColumnLabel+"\"/>\r\n\t\t\t\t\t\t</f:facet>\r\n";
	strColumn+="\r\n\t\t\t\t\t\t\t<h:commandLink action=\"#{detalleLibroBean.detalleLibro}\" value=\"" +strColumnLabel+"\" styleClass=\"link1\">\r\n";
	strColumn+="\t\t\t\t\t\t\t\t<f:setPropertyActionListener value=\"#{libro.id}\" target=\"#{detalleLibroBean.id}\" />\r\n";
	strColumn+="\t\t\t\t\t\t\t</h:commandLink>\r\n";
	strColumn+="\t\t\t\t\t</h:column>\r\n";
							
	return strColumn;
}

public string GetActionsTableMaintenanceC(TableSchema table)
{
	ArrayList arrAccionExtendsProperty=new ArrayList();
	
	arrAccionExtendsProperty=GetAccionExtendsPropertyC(table);
	
	//String strPlural=GetPluralTituloNombreTableFromPropertiesC(table);
	String strColumn="";
	//String strColumnLabel="'"+GetTituloNombreTableFromPropertiesC(table)+strPlural+"'";
	String strColumns="";
			
	//strColumnName="'"+GetNombreClaseObjetoC(table.ToString()) +strPlural+"'";
	
	String strNombreCodigo="";
	String strNombreWebTitulo="";
	
	
	foreach(MeExtendProperty meExtendProperty in arrAccionExtendsProperty)
	{
		strNombreCodigo=GetPropertyAccionTableFromPropertiesC(meExtendProperty,strPrefijoAccionTableNombreProperty);
		strNombreWebTitulo=GetPropertyAccionTableFromPropertiesC(meExtendProperty,strPrefijoAccionTableWebNombreProperty);
		
		strColumn="{ label :'"+strNombreWebTitulo+"', id :'"+strNombreCodigo+"'}";
		
		strColumns+=","+strColumn;
		
		
	}
	
	return strColumns;
}

public string GetActionsImagenTableTableMaintenanceC(TableSchema table)
{
	ArrayList arrAccionExtendsProperty=new ArrayList();
	
	arrAccionExtendsProperty=GetAccionExtendsPropertyC(table);
	
	//String strPlural=GetPluralTituloNombreTableFromPropertiesC(table);
	String strColumn="";
	//String strColumnLabel="'"+GetTituloNombreTableFromPropertiesC(table)+strPlural+"'";
	String strColumns="";
			
	//strColumnName="'"+GetNombreClaseObjetoC(table.ToString()) +strPlural+"'";
	
	String strNombreCodigo="";
	String strNombreWebTitulo="";
	String strNombreFuncionJavaScript="";
	
	foreach(MeExtendProperty meExtendProperty in arrAccionExtendsProperty)
	{
		strNombreCodigo=GetPropertyAccionTableFromPropertiesC(meExtendProperty,strPrefijoAccionTableNombreProperty);
		strNombreWebTitulo=GetPropertyAccionTableFromPropertiesC(meExtendProperty,strPrefijoAccionTableWebNombreProperty);
		strNombreFuncionJavaScript=strNombreCodigo.Substring(0,1).ToUpper()+strNombreCodigo.Substring(1,strNombreCodigo.Length-1).ToLower();
		
		strColumn=strNombreCodigo+":\"<img src=\\\""+GetRelativePathC(table)+"Imagenes/Accion/"+strNombreCodigo.ToLower()+".jpg\\\" onClick=\\\""+GetNombreClaseObjetoC(table.ToString())+"PaginaWebInteraccionEventsAdditional."+strNombreFuncionJavaScript+"(\"+arrData"+GetNombreClaseC(table.ToString())+"s[i].id+\")\\\" width=\\\"40\\\" height=\\\"40\\\">\"";
		
		strColumns+=","+strColumn;
		
		
	}
	
	return strColumns;
}

public String GetFuncionesImagenesC(TableSchema TablaBase) 
		{
			String strFuncionesImagenes="";
			
			foreach(ColumnSchema columnSchema in TablaBase.Columns)
			{				
				if((columnSchema.NativeType=="image"&&columnSchema.DataType==DbType.Binary&&columnSchema.Name!=strVersionRow)||(columnSchema.DataType==DbType.Binary&&columnSchema.Name!=strVersionRow))
				{
					
					//strFuncionesImagenes+="\r\nimport java.io.File;";
					strFuncionesImagenes+="\r\nimport java.util.Iterator;";
					strFuncionesImagenes+="\r\nimport java.util.List;";
					//strFuncionesImagenes+="\r\nimport java.io.FileInputStream;";
					strFuncionesImagenes+="\r\nimport org.apache.commons.fileupload.FileItem;";
					strFuncionesImagenes+="\r\nimport org.apache.commons.fileupload.FileItemFactory;";
					strFuncionesImagenes+="\r\nimport org.apache.commons.fileupload.disk.DiskFileItemFactory;";
					strFuncionesImagenes+="\r\nimport org.apache.commons.fileupload.servlet.ServletFileUpload;";
				}
			}
			return strFuncionesImagenes;
		
		}

public String GetClassClasesForeigKeysC(TableSchema TablaBase) 
		{
			String strTablaClaseRelacionada=string.Empty;
										
										
			foreach(ColumnSchema columnSchema in TablaBase.Columns)
			{
				if(columnSchema.IsForeignKeyMember)
				{
					strTablaClaseRelacionada+="\t\tclasses.add(new Classe("+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(columnSchema))+ ".class));\r\n";
				
				}
			}
			
			
				
			return strTablaClaseRelacionada; 
		}
		
public String GetClassClasesRelacionadasC(TableSchema TablaBase) 
		{
			String strTablaClaseRelacionada=string.Empty;
										
										
			
			
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(TablaBase);
			
			
			TableSchema tablaRelacionadaObjetivo;
				
			
				foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
				{
								
					if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
					{
						tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
					}	
					else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany)
					{
						
						tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
						
						if(!VerificarTablaRelacionFromPropertiesC(collectionInfo.JunctionTableSchema))
						{
							continue;
						}
					}
					else
					{
						continue;
					}
						
					if(!VerificarTablaRelacionFromPropertiesC(tablaRelacionadaObjetivo))
					{
						continue;
					}
					
					strTablaClaseRelacionada+="\t\tclasses.add(new Classe("+GetNombreClaseC(tablaRelacionadaObjetivo.ToString())+ ".class));\r\n";
					
				}
				
			return strTablaClaseRelacionada; 
		}



public String GetNavegacionTituloTablaTablasClasesRelacionadasC(TableSchema table) 
		{
			String strTablaClaseRelacionada=string.Empty;
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(table);
			TableSchema tablaRelacionadaObjetivo;
			
			ArrayList tablasRelacionadasEncontradas=new ArrayList();
			bool encontrado=false;
			String strClasesNoRelacionadas=string.Empty;
			String[] strClases;
			bool blClaseNo=false;
			
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{
				
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
				{
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}	
				else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany)
				{
					
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}
				else
				{
					tablaRelacionadaObjetivo=collectionInfo.JunctionTableSchema;
				}				
				
				
				strClasesNoRelacionadas=GetNombresClasesNoNavegacionFromTableFromPropertiesC(table);
				
				strClases=strClasesNoRelacionadas.Split(',');
				
				blClaseNo=false;
				
				foreach(String strClase in strClases)
				{						
					if(strClase.Equals(GetNombreClaseC(tablaRelacionadaObjetivo.ToString())))
					{
						blClaseNo=true;
						break;
					}
				}
				
				if(blClaseNo)
				{
					continue;
				}
				
				
					
				encontrado=false;
				
				
				foreach(TableSchema tableSchema in tablasRelacionadasEncontradas)
				{
					
					
					if(tableSchema.Name.Equals(tablaRelacionadaObjetivo.Name))
					{
						encontrado=true;
					}
				}
				
				
				
				if(!encontrado)
				{
					tablasRelacionadasEncontradas.Add(tablaRelacionadaObjetivo);
					strTablaClaseRelacionada+=GetColumnsTableMaintenanceC(tablaRelacionadaObjetivo);
				}
			}
								
			return strTablaClaseRelacionada; 
		}
		
		
public String GetNavegacionTituloTablaFacesTablasClasesRelacionadasC(TableSchema table) 
		{
			String strTablaClaseRelacionada=string.Empty;
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(table);
			
			TableSchema tablaRelacionadaObjetivo;
			ArrayList tablasRelacionadasEncontradas=new ArrayList();
			bool encontrado=false;
			String strClasesNoRelacionadas=string.Empty;
			String[] strClases;
			bool blClaseNo=false;
			
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{
				
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
				{
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}	
				else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany)
				{
					
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}
				else
				{
					tablaRelacionadaObjetivo=collectionInfo.JunctionTableSchema;
				}
				
				
				strClasesNoRelacionadas=GetNombresClasesNoNavegacionFromTableFromPropertiesC(table);
				
				strClases=strClasesNoRelacionadas.Split(',');
				
				blClaseNo=false;
				
				foreach(String strClase in strClases)
				{						
					if(strClase.Equals(GetNombreClaseC(tablaRelacionadaObjetivo.ToString())))
					{
						blClaseNo=true;
						break;
					}
				}
					
				if(blClaseNo)
				{
					continue;
				}
				
				
				
				encontrado=false;
				
				foreach(TableSchema tableSchema in tablasRelacionadasEncontradas)
				{
					
					
					if(tableSchema.Name.Equals(tablaRelacionadaObjetivo.Name))
					{
						encontrado=true;
					}
				}
				
				
				if(!encontrado)
				{
					tablasRelacionadasEncontradas.Add(tablaRelacionadaObjetivo);
					strTablaClaseRelacionada+=GetColumnsFacesTableMaintenanceC(tablaRelacionadaObjetivo);
				}
				
					
				
			}
								
			return strTablaClaseRelacionada; 
		}
		
		
		public string GetControlVariablesC(ColumnSchema column,bool ConFaces)
{			
	String strControl="";
	String strTituloControl="";
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
	{
			return string.Empty;
	}
	else if(column.Name.Equals(strId))
	{
		if(!ConFaces)
		{
			strControl="\r\n\t\t\t\t\t\t<td>"+"<input name=\"hdnIdActual\" type=\"text\" readonly=\"readonly\">"+"\r\n\t\t\t\t\t\t</td>";
		}
		else
		{
			String strObjectFace="";
			
			if(ConFaces)
			{
				strObjectFace=GetNombreClaseObjetoC(column.Table.ToString())+strPrefijoJSFFaces;
			}
			
			strControl="\r\n\t\t\t\t\t\t<td>"+"<h:inputHidden id=\"hdnIdActual\"  value=\"#{"+strObjectFace+"."+GetNombreClaseObjetoC(column.Table.ToString())+"."+strId+"}/>"+"\r\n\t\t\t\t\t\t</td>";
		}
			
			
	}
	else
	{
		if(ConFaces)
		{
			if(column.Name.Equals(strVersionRow))
			{
				return string.Empty;
			}
		}
		
		strControl="\r\n\t\t\t\t\t\t<td>"+GetControlHtml(column,ConFaces)+"\r\n\t\t\t\t\t\t</td>";	
	
	}
	
	
	strTituloControl=GetTituloControlVariablesC(column);
	
	return "\r\n\t\t\t\t\t<tr style=\"visibility:visible\">"+strTituloControl+strControl+"\r\n\t\t\t\t\t</tr>";
}

	
	public string GetNombreTituloControlVariablesSwingC(ColumnSchema column,String strIndexName,String strFinalName)
	{	
		String strTituloControl="";
		
		if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
		{
				return string.Empty;
		}
		else if(column.Name.Equals(strId))
		{
			if(strIndexName=="")
			{
				strTituloControl="jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+strIndexName+strFinalName+"";
			}
			else
			{
				strTituloControl="jLabel"+strIdGetSet+strIdGetSet+strIndexName+strFinalName+GetNombreClaseC(column.Table.ToString())+"";
			}
		}
		else
		{
			
			if(column.Name.Equals(strVersionRow))
			{
				return string.Empty;
			}
			if(strIndexName=="")
			{
				strTituloControl="jLabel"+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString())+strIndexName+strFinalName+"";
			}
			else
			{
				strTituloControl="jLabel"+GetNombreColumnaClaseC(column)+strIndexName+strFinalName+GetNombreClaseC(column.Table.ToString())+"";
			}
		}
		
		return strTituloControl;
	}
	
	public string GetNombreControlVariablesSwingC(ColumnSchema column,String strIndexName,String strFinalName)
	{			
		String strControl="";
		String strTituloControl="";
		String strPrefijo="";
		
		if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
		{
			return string.Empty;
		}
		else if(column.Name.Equals(strId))
		{
			strControl="jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIndexName+strFinalName;	
			return strControl;
		}
		else
		{
			
			if(column.Name.Equals(strVersionRow))
			{
				return string.Empty;
			}			
			
					
			if(!column.IsForeignKeyMember)
			{
				if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
				{
					if(column.Size<51)
					{
						strPrefijo="jTextField";
					}
					else if(column.Size<200)
					{
						strPrefijo="jTextArea";
					}
					else
					{
						strPrefijo="jTextArea";
					}
				}
				else if(column.DataType==DbType.Boolean)
				{
					strPrefijo="jCheckBox";
				}
				else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
				{
					strPrefijo="jTextField";
				}
				else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
				{
					strPrefijo="jFormattedTextField";
				}
			}
			else
			{
				strPrefijo="jComboBox";		
			}
			
		
		}
		
		if(strIndexName=="")
		{
			strControl=strPrefijo+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString());
		}
		else
		{
			strControl=strPrefijo+GetNombreColumnaClaseC(column)+strIndexName+strFinalName+GetNombreClaseC(column.Table.ToString());
		}
			
		return strControl;
	}
	
	public string GetInicializacionControlVariablesSwingC(ColumnSchema column,String strIndexName,String strFinalName)
	{			
		String strControl="";
		String strTituloControl="";
		
		if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
		{
				return string.Empty;
		}
		else if(column.Name.Equals(strId))
		{
			strTituloControl="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+" = new JLabel();;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setText(\""+strIdGetSet+"\");;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));;\r\n\r\n";


			
			strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JLabel();\r\n";
			strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
			strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
			strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

		}
		else
		{
			
			if(column.Name.Equals(strVersionRow))
			{
				return string.Empty;
			}			
			
			
			strTituloControl+="\r\n\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+" = new JLabel();\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setText(\""+GetWebNombreTituloColumnFromPropertiesC(column)+"\");;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));;\r\n";
			strTituloControl+="\t\t"+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));;\r\n\r\n";

		
			if(!column.IsForeignKeyMember)
			{
				if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
				{
					if(column.Size<51)
					{
						strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JTextField();\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

					}
					else if(column.Size<200)
					{
						strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JTextArea();\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

					}
					else
					{
						strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JTextArea();\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
						strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

					}
				}
				else if(column.DataType==DbType.Boolean)
				{
					strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JCheckBox();\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

				}
				else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
				{
					strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JTextField();\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

				}
				else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
				{
					strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JFormattedTextField();\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
					strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";

				}
			}
			else
			{
				strControl="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+"= new JComboBox();\r\n";
				strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMinimumSize(new Dimension(100,20));\r\n";
				strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setMaximumSize(new Dimension(100,20));\r\n";
				strControl+="\t\t"+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+".setPreferredSize(new Dimension(100,20));\r\n\r\n\r\n";
	
			}
			
		
		}
		
		
		
		return strTituloControl+strControl;
	}
	
	public string GetDefinicionControlVariablesSwingC(ColumnSchema column,String strIndexName,String strFinalName)
	{			
		String strControl="";
		String strTituloControl="";
		
		if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
		{
				return string.Empty;
		}
		else if(column.Name.Equals(strId))
		{
			
			strTituloControl="\r\n\tprotected JLabel jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+strIndexName+strFinalName+" = new JLabel();\r\n";
			strControl="\tprotected JLabel "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
														
		}
		else
		{
			
			if(column.Name.Equals(strVersionRow))
			{
				return string.Empty;
			}			
			
			
				strTituloControl="\r\n\tJLabel "+GetNombreTituloControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
			
			
			if(!column.IsForeignKeyMember)
			{
				if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
				{
					if(column.Size<51)
					{
						strControl="\tprotected JTextField "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
					}
					else if(column.Size<200)
					{
						strControl="\tprotected JTextArea "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
					}
					else
					{
						strControl="\tprotected JTextArea "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
					}
				}
				else if(column.DataType==DbType.Boolean)
				{
					strControl="\tprotected JCheckBox "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
				}
				else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
				{
					strControl="\tprotected JTextField "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
				}
				else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
				{
					strControl="\tprotected JFormattedTextField "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";
				}
			}
			else
			{
				strControl="\tprotected JComboBox "+GetNombreControlVariablesSwingC(column,strIndexName,strFinalName)+";\r\n";		
			}
		
		}
		
		
		
		return strTituloControl+strControl;
	}
	
	
		
	public string GetControlVariablesSwingC(ColumnSchema column)
	{			
		String strControl="";
		String strTituloControl="";
		
		if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
		{
				return string.Empty;
		}
		else if(column.Name.Equals(strId))
		{
			
			strControl="\r\n\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+" = new javax.swing.JLabel();\r\n";
			strControl+="\r\n\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+".setText(\""+strIdGetSet+"\");\r\n";
			
			strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strId+"}\"), jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
			strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
			strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
			strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
			strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											

														
		}
		else
		{
			
			if(column.Name.Equals(strVersionRow))
			{
				return string.Empty;
			}			
			
			strControl="\t\t"+GetControlSwingC(column);	
		
		}
		
		
		strTituloControl=GetTituloControlSwingVariablesC(column);
		
		return "\r\n\t\t\t\t\t"+strTituloControl+strControl+"\r\n";
	}
	
	public string GetTituloControlSwingVariablesC(ColumnSchema column)
	{
		
		String strTipo=GetTipoColumnaClaseC(column);	
		String strPrefijo=" "+GetPrefijoTipoC(column);	
		String strColumna=GetWebNombreTituloColumnFromPropertiesC(column);
		String strTituloLabel=string.Empty;
		
		strTituloLabel="\r\n\t\tjLabel"+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString())+" = new javax.swing.JLabel();\r\n";
		strTituloLabel+="\t\tjLabel"+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString())+".setText(\""+strColumna+"\");\r\n";
		
		strTituloLabel+="\t\tjLabel"+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
        strTituloLabel+="\t\tjLabel"+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        strTituloLabel+="\t\tjLabel"+GetNombreColumnaClaseC(column)+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n";
     
		String strTitleControl="";
		
		if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired) )
		{
				return string.Empty;
		}
		else if(column.Name.Equals(strVersionRow))
		{
			return string.Empty;
		}
		else if(column.Name.Equals(strId))
		{
			strTituloLabel="\r\n\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+" = new javax.swing.JLabel();\r\n";
			strTituloLabel+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+".setText(\""+strCodigoUnico+"\");\r\n";
			
			strTituloLabel+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+".setMinimumSize(new Dimension(100,20));\r\n";
        	strTituloLabel+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+".setMaximumSize(new Dimension(100,20));\r\n";
        	strTituloLabel+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+strIdGetSet+".setPreferredSize(new Dimension(100,20));\r\n";
      
			return strTituloLabel;
		}
		else
		{
			if(!column.IsForeignKeyMember)
			{
				strTitleControl=strTituloLabel +"\r\n";
			}
			else
			{
				
				strTitleControl= strTituloLabel +"\r\n";
			}
		}	
		return strTitleControl;
	}
	
		public string GetTituloControlVariablesC(ColumnSchema column)
{
	
	String strTipo=GetTipoColumnaClaseC(column);	
	String strPrefijo=" "+GetPrefijoTipoC(column);	
	String strColumna=GetWebNombreTituloColumnFromPropertiesC(column);
	
	String strTitleControl="";
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired) )
	{
			return string.Empty;
	}
	else if(column.Name.Equals(strVersionRow))
	{
		return "\r\n\t\t\t\t\t\t<td class=\"titulocampo\">\r\n\t\t\t\t\t\t</td>";
	}
	else if(column.Name.Equals(strId))
	{
		return "\r\n\t\t\t\t\t\t<td class=\"titulocampo\"><h2>"+strCodigoUnico+"</h2>\r\n\t\t\t\t\t\t</td>";
	}
	else
	{
		if(!column.IsForeignKeyMember)
		{
			strTitleControl="\r\n\t\t\t\t\t\t<td class=\"titulocampo\"><h2>"+ strColumna +"</h2>\r\n\t\t\t\t\t\t</td>";
		}
		else
		{
			/*
			strColumna= GetNombreColumnaClaseC(column);
			strColumna=strColumna.Replace(strId,"");
			strColumna=strColumna.Substring(0, 1).ToUpper() + strColumna.Substring(1, strColumna.Length-1).ToLower();
			*/
			strTitleControl="\r\n\t\t\t\t\t\t<td class=\"titulocampo\"><h2>"+ strColumna +"</h2>\r\n\t\t\t\t\t\t</td>";
		}
	}	
	return strTitleControl;
}
		
		#endregion
		
	
	
		#region QuerysComplejos
		
		public const String strPrefijoParametros="PRM_";
		
		public static string GetReadOnlyNombreCompletoColumnaClaseC(ColumnSchema column)
		{
			if(column.Name=="id")
			{
				return "Id";
			}
			else if(column.Name=="isActive")
			{
				return "IsActive";
			}
			else if(column.Name=="isExpired")
			{
				return "IsExpired";
			}
			else if(column.Name=="versionRow")
			{
				return "VersionRow";
			}
			
			string strPrefijoTabla="";
			
			if(!column.Name.Contains(strPrefijoParametros))
			{
				strPrefijoTabla=GetPrefijoTablaC();
			}
			else
			{
				strPrefijoTabla=GetPrefijoTablaParametroC();
			}
			
			string strPrefijo =strPrefijoTabla+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column);
			
			
			return strPrefijo;
		}

		public static string GetReadOnlyXmlColumnaC(ColumnSchema column,String tablaBase)
		{
		string strNombre="";
		string strGetColumn="";
		string strPrefijoTabla="";
		string strPrefijoTipo="";
		string strNombreColumna="";
		
			strPrefijoTabla=GetPrefijoTablaC();
			strPrefijoTipo=GetPrefijoTipoC(column);
			strNombreColumna=GetNombreColumnaClaseC(column);
			strGetColumn="get"+GetReadOnlyNombreCompletoColumnaClaseC(column)+"()"+GetTipoColumnaToString(column);
		
		
		if(column.Name=="id")
		{
		strNombre="xml.append(\"<item code=\\\"\"+"+"readOnly"+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+"+\"\\\">\\r\n\");\r\n";
		}
	
	
		strNombre+= "xml.append(\"<";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
		strNombre+=">\");\r\n";
		
		strNombre+="		xml.append("+"readOnly"+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+");\r\n";
		
		strNombre+="		xml.append(\"</";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
		strNombre+=">\\r\n\");\r\n";
		
				
		return strNombre;
		}
	
		public static string GetParametroNombreCompletoColumnaClaseC(ColumnSchema column)
		{
			if(column.Name=="id")
			{
				return "Id";
			}
			else if(column.Name=="isActive")
			{
				return "IsActive";
			}
			else if(column.Name=="isExpired")
			{
				return "IsExpired";
			}
			else if(column.Name=="versionRow")
			{
				return "VersionRow";
			}
			
			string strPrefijo =GetPrefijoTablaParametroC()+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column);
			
			
			return strPrefijo;
		}
		
		public static string GetPrefijoTablaParametroC()
		{
			string strPrefijoTabla=strPrefijoParametros;
			return strPrefijoTabla;
		}
	
		public String GetReadOnlyParameterSelection(ColumnSchema column,bool esUltimo) 
		{
			String strParaBusquedaString=""; 
			
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
			strParaBusquedaString="+\"%\"";
			}
			
			String strParameterSelection=String.Empty;
			strParameterSelection="\r\n\r\n\t\t\tParameterSelectionGeneral parameterSelectionGeneral"+column.Name+"= new ParameterSelectionGeneral();";
			strParameterSelection+="\r\n\t\t\tparameterSelectionGeneral"+column.Name+".setParameterSelectionGeneralEqual(ParameterType."+GetTipoColumnaClaseEnumC(column)+","+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+ GetTipoColumnaToString(column)+strParaBusquedaString +",ReadOnly"+GetNombreClaseC(column.Table.ToString())+".getColumnName"+GetNombreCampoTablaC(column)+"(),";
			
			if(esUltimo)
			{
			strParameterSelection+="ParameterTypeOperator.NONE);";		
			}
			else
			{
			strParameterSelection+=	"ParameterTypeOperator.AND);";	
			}
			
			strParameterSelection+="\r\n\t\t\tqueryWhereSelectParameters.addParameter(parameterSelectionGeneral"+column.Name+");";
		
			return strParameterSelection;
		}
		
		public string GetParametroFuncionQueryClase(ColumnSchema column)
		{		
		String strParamtro=string.Empty;	
		
		strParamtro=GetTipoColumnaClaseC(column)+" "+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column);
		return strParamtro;
		}

		public int GetTotalParametrosFuncionQueryClase(TableSchema table)
		{		int intTotalParamtros=0;	
		
		foreach(ColumnSchema column in table.Columns)
		{
			if(column.Name.Contains(strPrefijoParametros))
			{
			intTotalParamtros++;
			}
		}
		return intTotalParamtros;
		}

		#endregion 
		
		#region JavaScript
	
	public String GetJavaScriptArraysClasesRelacionadasC(TableSchema tableSchema) 
	{
				String strTablaClaseRelacionada="";	
				System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(tableSchema);
				
				
				TableSchema tablaRelacionadaObjetivo;
		
				foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
				{
					
					
					
					if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
					{
						tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
					}	
					else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany)
					{
						
						tablaRelacionadaObjetivo=collectionInfo.JunctionTableSchema;
					}
					else
					{
						continue;
					}
					
					if(!VerificarTablaRelacionFromPropertiesC(tablaRelacionadaObjetivo))
					{
						continue;
					}
					
					strTablaClaseRelacionada+=",arrData"+GetNombreClaseC(tablaRelacionadaObjetivo.ToString())+"s";
				}
				
				return strTablaClaseRelacionada; 
	}

	public  string GetNombreColumnaClaseJavaScriptFiltradosArrayToTablaC(String strPrefijo,ColumnSchema column,bool blnConPrefijoCompuesto,TableSchema tableNombreArray)
	{
		//CambiarBooleanValueToControl(
		
		String strValor="";
		String strPrefijoTabla="";
		
		if(strPrefijo!=""&&blnConPrefijoCompuesto)
		{
			strPrefijoTabla=GetNombreClaseC(column.Table.ToString());
		}
		
		if(column.Name==strIsActive||column.Name==strIsExpired||column.Name==strId)
		{
			return "";
		}
		
		if(column.DataType!=DbType.Boolean&&!column.IsForeignKeyMember&&!(column.NativeType=="image"&&column.DataType==DbType.Binary&&column.Name!=strVersionRow)&&!(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
		{
			strValor=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+":"+ "arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i]."+strPrefijo+GetNombreColumnaClaseJavaScriptC(column);
		}
		else if(column.DataType==DbType.Boolean)
		{
			strValor=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+":"+ "arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i]."+strPrefijo+GetNombreColumnaClaseJavaScriptC(column);
			strValor+=",";
			strValor+=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+"Control:"+"funcionGeneral.CambiarBooleanValueToControl(arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i]."+strPrefijo+GetNombreColumnaClaseJavaScriptC(column)+",arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i]."+strPrefijo+strId+")";
		}
		else if(column.IsForeignKeyMember)
		{
			strValor=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+"Descripcion:"+"arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i]."+strPrefijo+GetNombreColumnaClaseJavaScriptC(column)+"Descripcion";
			strValor+=",";
			strValor+=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+":"+"arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i]."+strPrefijo+GetNombreColumnaClaseJavaScriptC(column);
		}
		else if((column.NativeType=="image"&&column.DataType==DbType.Binary&&column.Name!=strVersionRow)||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
		{
			strValor=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+"Mostrar"+":\"<img src=\\\""+GetRelativePathC(tableNombreArray)+"Imagenes/mostrarimagen.gif\\\" onClick=\\\""+GetNombreClaseObjetoC(tableNombreArray.ToString())+"PaginaWebInteraccion.Mostrar"+GetNombreClaseC(tableNombreArray.ToString())+GetNombreColumnaClaseC(column)+"(\"+arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i].id+\")\\\" width=\\\"35\\\" height=\\\"35\\\">\"";
			strValor+=",";
			strValor+=strPrefijoTabla+GetNombreColumnaClaseJavaScriptC(column)+"Actualizar"+":\"<img src=\\\""+GetRelativePathC(tableNombreArray)+"Imagenes/actualizarimagen.gif\\\" onClick=\\\""+GetNombreClaseObjetoC(tableNombreArray.ToString())+"PaginaWebInteraccion.Actualizar"+GetNombreClaseC(tableNombreArray.ToString())+GetNombreColumnaClaseC(column)+"(\"+arrData"+GetNombreClaseC(tableNombreArray.ToString())+"s[i].id+\")\\\" width=\\\"35\\\" height=\\\"35\\\">\"";
		}
		
		return strValor;
	}
	
	public String GetImagenesNavegacionTablasClasesRelacionadasC(TableSchema TablaBase) 
		{
			String strTablaClaseRelacionada=string.Empty;
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(TablaBase);
			
			TableSchema tablaRelacionadaObjetivo;
			String strPlural=string.Empty;
			ArrayList tablasRelacionadasEncontradas=new ArrayList();
			bool encontrado=false;
			
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{
				
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
				{
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}	
				else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany)
				{
					
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}
				else
				{
					tablaRelacionadaObjetivo=collectionInfo.JunctionTableSchema;
				}
				
				encontrado=false;
				
				foreach(TableSchema tableSchema in tablasRelacionadasEncontradas)
				{
					if(tableSchema.Equals(tablaRelacionadaObjetivo))
					{
						encontrado=true;
					}
				}
				
				
				if(!encontrado)
				{
					strPlural=GetPluralTituloNombreTableFromPropertiesC(tablaRelacionadaObjetivo);
					strTablaClaseRelacionada+=","+GetNombreClaseObjetoC(tablaRelacionadaObjetivo.ToString())+strPlural+":\"<img src=\\\""+GetRelativePathC(TablaBase)+"Imagenes/"+GetNombreClaseObjetoC(tablaRelacionadaObjetivo.ToString())+strPlural.ToLower()+".gif\\\" onClick=\\\""+GetNombreClaseObjetoC(TablaBase.ToString())+"PaginaWebInteraccion.Actualizar"+GetNombreClaseC(tablaRelacionadaObjetivo.ToString())+strPlural+"Relacionadas"+"(\"+arrData"+GetNombreClaseC(TablaBase.ToString())+"s[i].id+\")\\\" width=\\\"40\\\" height=\\\"40\\\">\"";
				}	
			}
								
			return strTablaClaseRelacionada; 
		}
		
	public string GetNombreColumnaClaseDefaultJavaScriptFiltradosC(ColumnSchema column)
	{
		String strColumna="";
		
	if(column.Name==strIsActive||column.Name==strIsExpired)
		{
		return "";
		}
		
		if(!column.IsForeignKeyMember)
		{
			if(column.Name==strId)
			{
			strColumna=/*GetNombreColumnaClaseJavaScriptC(column)+*/"id:Int"+strIdGetSet+"Nuevo"+GetNombreClaseC(column.Table.ToString());
			}
			else if(column.Name==strVersionRow)
			{
				strColumna=GetNombreColumnaClaseJavaScriptC(column)+":\"1900-01-01 01:01:01\"";
			}
			else
			{
			strColumna=GetNombreColumnaClaseJavaScriptC(column)+":\"null\"";
			}
		}
		else
		{
			strColumna=GetNombreColumnaClaseJavaScriptC(column)+":\"null\",";
			strColumna+=GetNombreColumnaClaseJavaScriptC(column)+"Descripcion:\"null\"";
		}
		
		return strColumna;
	}

	public static string GetNombreColumnaClaseJavaScriptC(ColumnSchema column)
	{
		string strPrefijo=String.Empty;
		string strPrefijoTipo=String.Empty;
		string strNombre= String.Empty;
		
		/*
		if(column.Name!=strId)
		{*/
			strPrefijo=String.Empty;
			strPrefijoTipo=GetPrefijoTipoC(column);
			strNombre= GetNombreColumnaClaseC(column);
		/*}
		else
		{
			strNombre= "id";
		}
		*/
	strPrefijo=strPrefijoTipo+strNombre;
	
	return strPrefijo;
	}
	
	public static string GetJavaScritpVariablesFromXmlC(ColumnSchema column,bool reemplazarForeigKey,bool reemplazarBooleanValue)
	{
		string strInicio=GetNombreClaseObjetoC(column.Table.ToString())+".getElementsByTagName(strPrefijo+\"";
		string strFin="\")[0].firstChild.nodeValue;";
		string strFin2="\")[0].firstChild)";
	
	string strNombre="";
	string strGetColumn="";
	string strPrefijoTabla="";
	string strPrefijoTipo="";
	string strNombreColumna="";
	
		strPrefijoTabla=GetPrefijoTablaC();
	    strPrefijoTipo=GetPrefijoTipoC(column);
	    strNombreColumna=GetNombreColumnaClaseC(column);
		strGetColumn="get"+strPrefijoTabla+strPrefijoTipo+strNombreColumna+"()"+GetTipoColumnaToString(column);
	
	
		if(column.Name==strIsActive||column.Name==strIsExpired||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
		{
		return "";
		}
		
		string strPrefijoCorreccion=String.Empty;
		string strPrefijoTipoCorreccion =String.Empty;
		string strNombreCorreccion = String.Empty;
			
		if(column.Name!=strId)
		{
			strPrefijoCorreccion=String.Empty;
			strPrefijoTipoCorreccion =GetPrefijoTipoC(column);
			strNombreCorreccion = GetNombreColumnaClaseC(column);
		}
		else
		{
			strNombreCorreccion = "id";
		}
		
	strPrefijoCorreccion=strPrefijoTipoCorreccion+strNombreCorreccion;
	
		if(column.IsForeignKeyMember)
		{
			strNombre="\r\n\r\n\t\t\t\t\tvar "+ strPrefijoCorreccion+ "='';";
			
			strNombre+="\r\n\t\t\t\t\t"+"if("+strInicio+column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower()+strFin2+"{";
			strNombre+=strPrefijoCorreccion+"="+strInicio;
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
			strNombre+=strFin+"}";
			
			strNombre+="\r\n\r\n\t\t\t\t\tvar "+ strPrefijoCorreccion+"Descripcion"+ "='';";
			strNombre+="\r\n\t\t\t\t\t"+"if("+strInicio+column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower()+strFin2+"{";
			strNombre+= strPrefijoCorreccion+"Descripcion"+"="+ strInicio;
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();		
			strNombre+=strFin+"}";
		}
		else
		{
			strNombre="\r\n\r\n\t\t\t\t\tvar "+ strPrefijoCorreccion+ "='';";
			strNombre+="\r\n\t\t\t\t\t"+"if("+strInicio+column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower()+strFin2+"{";
			
			strNombre+=strPrefijoCorreccion+"="+ strInicio;
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
			strNombre+=strFin+"}";
		}
	
	return strNombre;
	}
	
	public static string GetNombreColumnaClaseJavaScriptFiltradosC(ColumnSchema column,bool conVersionRow)
{
	String strNombreColumna="";
	
	if(column.DataType==DbType.Binary&&column.Name!=strVersionRow)
	{
		return "";		
	}
	
	if(!conVersionRow)
	{
		if(column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
		{
		return "";
		}
	}
	else
	{
		if(column.Name==strIsActive||column.Name==strIsExpired)
		{
		return "";
		}
	}
	
	string strPrefijo=String.Empty;
	string strPrefijoTipo =String.Empty;
	string strNombre = String.Empty;
		
	if(column.Name!=strId)
	{
		strPrefijo=String.Empty;
		strPrefijoTipo =GetPrefijoTipoC(column);
		strNombre = GetNombreColumnaClaseC(column);
	}
	else
	{
		strNombre = "id";
	}
		
	strPrefijo=strPrefijoTipo+strNombre;

	
	if(!column.IsForeignKeyMember)
	{
		strNombreColumna=strPrefijo;	
	}
	else
	{
	    strNombreColumna=strPrefijo;
		strNombreColumna+=",";
		strNombreColumna+=strPrefijo+"Descripcion";
	}
	
	return strNombreColumna;
}

public static string GetNombreColumnaClaseJavaScriptFiltradosSoloForeigKeyColumnC(ColumnSchema column)
{
	String strNombreColumna="";
	
		
	string strPrefijo=String.Empty;
	string strPrefijoTipo =String.Empty;
	string strNombre = String.Empty;
		
	
		strPrefijo=String.Empty;
		strPrefijoTipo =GetPrefijoTipoC(column);
		strNombre = GetNombreColumnaClaseC(column);
	
		
	strPrefijo=strPrefijoTipo+strNombre;

	
	
		strNombreColumna=strPrefijo;
	
	
	
	return strNombreColumna;
}

public static string GetNombreColumnaClaseJavaScriptFiltradosC(ColumnSchema column)
{
	String strNombreColumna="";
	
	if(column.Name==strIsActive||column.Name==strIsExpired||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
	{
		
		return "";
	}
	
	string strPrefijo=String.Empty;
	string strPrefijoTipo =String.Empty;
	string strNombre = String.Empty;
		
	if(column.Name!=strId)
	{
		strPrefijo=String.Empty;
		strPrefijoTipo =GetPrefijoTipoC(column);
		strNombre = GetNombreColumnaClaseC(column);
	}
	else
	{
		strNombre = "id";
	}
	
	strPrefijo=strPrefijoTipo+strNombre;
	
	if(!column.IsForeignKeyMember)
	{
		strNombreColumna="this." +strPrefijo+"="+strPrefijo;
	}
	else
	{
	    strNombreColumna="this." +strPrefijo+"="+strPrefijo;
		strNombreColumna+=";\r\n\t";
		strNombreColumna+="this." +strPrefijo+"Descripcion"+"="+strPrefijo+"Descripcion";
	}
	return strNombreColumna;
}

public static string GetNombreColumnaClaseMinusculasJavaScriptNullAVacioC(ColumnSchema column)
{
	String strNombreColumna="";
	
if(column.Name==strIsActive||column.Name==strIsExpired||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
	{
	return "";
	}
	
	string strPrefijo=String.Empty;
	string strPrefijoTipo =String.Empty;
	string strNombre = String.Empty;
		
	if(column.Name!=strId)
	{
		strPrefijo=String.Empty;
		strPrefijoTipo =GetPrefijoTipoC(column);
		strNombre = GetNombreColumnaClaseC(column);
	}
	else
	{
		strNombre = "id";
	}
	
	strPrefijo=strPrefijoTipo+strNombre;
	
	if(!column.IsForeignKeyMember)
	{
		strNombreColumna="\r\n\t"+strPrefijo+"=funcionGeneral.CambiarNullAVacio(" +strPrefijo+");";
	}
	else
	{
	    strNombreColumna="\r\n\t"+strPrefijo+"=funcionGeneral.CambiarNullAVacio(" +strPrefijo+");";
		strNombreColumna+="\r\n\t"+strPrefijo+"Descripcion=funcionGeneral.CambiarNullAVacio(" +strPrefijo+"Descripcion"+");";
	}
	return strNombreColumna;
}

	public static string GetNombreColumnaClaseMinusculasFiltradosC(ColumnSchema column)
{
	String strNombreColumna="";
	
if(column.Name==strIsActive||column.Name==strIsExpired||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
	{
	return "";
	}
	
	if(!column.IsForeignKeyMember)
	{
		strNombreColumna=GetNombreColumnaClaseMinusculaC(column)+":" +GetNombreColumnaClaseMinusculaC(column);
	}
	else
	{
	    strNombreColumna=GetNombreColumnaClaseMinusculaC(column)+":" +GetNombreColumnaClaseMinusculaC(column);
		strNombreColumna+=",";
		strNombreColumna+=GetNombreColumnaClaseMinusculaC(column)+"Descripcion:" +GetNombreColumnaClaseMinusculaC(column)+"Descripcion";
	}
	return strNombreColumna;
}

public static string GetNombreColumnaClaseMinusculasFiltradosNullAVacioC(ColumnSchema column)
{
	String strNombreColumna="";
	
if(column.Name==strIsActive||column.Name==strIsExpired||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
	{
	return "";
	}
	
	if(!column.IsForeignKeyMember)
	{
		strNombreColumna=GetNombreColumnaClaseMinusculaC(column)+"=CambiarNullAVacio(" +GetNombreColumnaClaseMinusculaC(column)+");\r\n";
	}
	else
	{
	    strNombreColumna=GetNombreColumnaClaseMinusculaC(column)+"=CambiarNullAVacio(" +GetNombreColumnaClaseMinusculaC(column)+");\r\n";
		strNombreColumna+=GetNombreColumnaClaseMinusculaC(column)+"Descripcion=CambiarNullAVacio(" +GetNombreColumnaClaseMinusculaC(column)+"Descripcion"+");\r\n";
	}
	return strNombreColumna;
}

	public static string GetNombreColumnaClaseMinusculaFiltradosC(ColumnSchema column,bool conVersionRow)
{
	String strNombreColumna="";
	
	if(column.DataType==DbType.Binary&&column.Name!=strVersionRow)
	{
		return "";		
	}
	
	if(!conVersionRow)
	{
		if(column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
		{
		return "";
		}
	}
	else
	{
		if(column.Name==strIsActive||column.Name==strIsExpired)
		{
		return "";
		}
	}
	
	if(!column.IsForeignKeyMember)
	{
		strNombreColumna=GetNombreColumnaClaseMinusculaC(column);	
	}
	else
	{
	    strNombreColumna=GetNombreColumnaClaseMinusculaC(column);
		strNombreColumna+=",";
		strNombreColumna+=GetNombreColumnaClaseMinusculaC(column)+"Descripcion";
	}
	
	return strNombreColumna;
}



	public TableSchema GetTablaFromNombreC(String strTableNombre,TableSchema tableSchema) 
		{
			TableSchema tableSchemaEncontrada=tableSchema;
			
			foreach(TableSchema tableSchemai in tableSchema.Database.Tables)
			{
				if(GetNombreTableFromProperties(tableSchemai).Equals(strTableNombre))
				{
					return tableSchemai;
					break;
				}
			}
			
			return tableSchemaEncontrada;
		}
	
	public TableSchema GetTablaFromNombreClaseC(String strTableNombreClase,TableSchema tableSchema) 
		{
			TableSchema tableSchemaEncontrada=tableSchema;
			
			foreach(TableSchema tableSchemai in tableSchema.Database.Tables)
			{
				if(GetNombreClaseC(tableSchemai.ToString()).Equals(strTableNombreClase))
				{
					return tableSchemaEncontrada=tableSchemai;
					break;
				}
			}
			
			return tableSchemaEncontrada;
		}
		
	public String SetDataToCombosBusquedasIndiceTablaC(ColumnSchema column,TableSchema tableSchema) 
	{
			String	strDataToCombos="";		
						
			foreach(IndexSchema indexSchema in tableSchema.Indexes)
			{	
				if(indexSchema.IsUnique)
				{
					continue;
				}
				
							foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
							{
								if(memberColumnSchema.IsForeignKeyMember)
								{
									if(memberColumnSchema.Name.Equals(column.Name))
									{
										strDataToCombos+="\r\n\r\n\tif(jmaki.attributes.get('"+ GetNameControlHtmlBusqueda(memberColumnSchema,indexSchema.Name)+  "')!=undefined)";
										strDataToCombos+="\r\n\t{";
										strDataToCombos+="\r\n\t\tjmaki.attributes.get('"+ GetNameControlHtmlBusqueda(memberColumnSchema,indexSchema.Name)+  "').setValues(this.ArrData"+GetNombreClaseRelacionadaFromColumn(memberColumnSchema) +");";
										strDataToCombos+="\r\n\t}";
									}
								}
							
							}															
			}								
			return  strDataToCombos;
	}
	
		public String SetDataToCombosBusquedasIndiceSoloFKTablaSeleccionadoC(ColumnSchema column,TableSchema tableSchema) 
	{
			String	strDataToCombos="";		
						
			foreach(IndexSchema indexSchema in tableSchema.Indexes)
			{
				if(!indexSchema.Name.Contains(strFK+"_"))
				{
				continue;
				}
							foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
							{
								if(memberColumnSchema.IsForeignKeyMember&&indexSchema.MemberColumns.Count.Equals(1))
								{
									if(memberColumnSchema.Name.Equals(column.Name))
									{
									strDataToCombos+="\r\n\t\tjmaki.attributes.get('"+ GetNameControlHtmlBusqueda(memberColumnSchema,indexSchema.Name)+  "').wrapper.dataProvider.setData(arrData"+GetNombreClaseRelacionadaFromColumn(memberColumnSchema) +");";
									strDataToCombos+="\r\n\t\tjmaki.attributes.get('"+ GetNameControlHtmlBusqueda(memberColumnSchema,indexSchema.Name)+  "').wrapper.setValue(Servicios"+GetNombreClaseC(tableSchema.ToString())+"IntIdUnico"+GetNombreClaseRelacionadaFromColumn(memberColumnSchema) +");";
									}
								}
							
							}															
			}								
			return  strDataToCombos;
	}
	
		public static string GetVariablesFromXmlC(ColumnSchema column,bool reemplazarForeigKey,bool reemplazarBooleanValue)
	{
		string strInicio="item.getElementsByTagName(\"";
		string strFin="\")[0].firstChild.nodeValue;";
		string strFin2="\")[0].firstChild)";
	
	string strNombre="";
	string strGetColumn="";
	string strPrefijoTabla="";
	string strPrefijoTipo="";
	string strNombreColumna="";
	
		strPrefijoTabla=GetPrefijoTablaC();
	    strPrefijoTipo=GetPrefijoTipoC(column);
	    strNombreColumna=GetNombreColumnaClaseC(column);
		strGetColumn="get"+strPrefijoTabla+strPrefijoTipo+strNombreColumna+"()"+GetTipoColumnaToString(column);
	
	
		if(column.Name==strIsActive||column.Name==strIsExpired||(column.DataType==DbType.Binary&&column.Name!=strVersionRow))
		{
		return "";
		}
	
		if(column.IsForeignKeyMember)
		{
			strNombre="\r\n\r\n\t\t\t\t\tvar "+ column.Name.Substring(0, column.Name.Length).ToLower()+ "='';";
			
			strNombre+="\r\n\t\t\t\t\t"+"if("+strInicio+column.Name.Substring(0, column.Name.Length).ToLower()+strFin2+"{";
			strNombre+=column.Name.Substring(0, column.Name.Length).ToLower()+"="+strInicio;
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
			strNombre+=strFin+"}";
			
			strNombre+="\r\n\r\n\t\t\t\t\tvar "+ column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+ "='';";
			strNombre+="\r\n\t\t\t\t\t"+"if("+strInicio+column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+strFin2+"{";
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+"="+ strInicio;
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion";		
			strNombre+=strFin+"}";
		}
		else
		{
			strNombre="\r\n\r\n\t\t\t\t\tvar "+ column.Name.Substring(0, column.Name.Length).ToLower()+ "='';";
			strNombre+="\r\n\t\t\t\t\t"+"if("+strInicio+column.Name.Substring(0, column.Name.Length).ToLower()+strFin2+"{";
			
			strNombre+=column.Name.Substring(0, column.Name.Length).ToLower()+"="+ strInicio;
			strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
			strNombre+=strFin+"}";
		}
	/*if(reemplazarForeigKey)
	{
		if(column.IsForeignKeyMember)
		{
		strNombre="var "+ column.Name.Substring(0, column.Name.Length).ToLower()+ strInicio;
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion";
		strNombre+=strFin;
		}
		else
		{
		strNombre="var "+ column.Name.Substring(0, column.Name.Length).ToLower()+ strInicio;
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
		strNombre+=strFin;
		}
	}*/
	/*else
	{
		strNombre="var "+ column.Name.Substring(0, column.Name.Length).ToLower()+ strInicio;
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
		strNombre+=strFin;
	}*/
	/*
	if(reemplazarBooleanValue)
	{
		if(column.DataType==DbType.Boolean)
		{
		strNombre+="\t\t"+ column.Name.Substring(0, column.Name.Length).ToLower()+"=CambiarBooleanValueToControl("+column.Name.Substring(0, column.Name.Length).ToLower() +",id);\r\n";	
		}	
	}
	*/
	return strNombre;
	}
	
	#endregion
		
		#region Licence
		
		public String  GetByDanLicence() 
		{
			String strLicencia="";
			strLicencia+="/*";
			strLicencia+="\r\n* ============================================================================";
			strLicencia+="\r\n* GNU Lesser General Public License";
			strLicencia+="\r\n* ============================================================================";
			strLicencia+="\r\n*";
			strLicencia+="\r\n* BYDAN - Free Java BYDAN library.";
			strLicencia+="\r\n* Copyright (C) 2008 ";
			strLicencia+="\r\n* ";
			strLicencia+="\r\n* This library is free software; you can redistribute it and/or";
			strLicencia+="\r\n* modify it under the terms of the GNU Lesser General Public";
			strLicencia+="\r\n* License as published by the Free Software Foundation; either";
			strLicencia+="\r\n* version 2.1 of the License, or (at your option) any later version.";
			strLicencia+="\r\n* ";
			strLicencia+="\r\n* This library is distributed in the hope that it will be useful,";
			strLicencia+="\r\n* but WITHOUT ANY WARRANTY; without even the implied warranty of";
			strLicencia+="\r\n* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU";
			strLicencia+="\r\n* Lesser General Public License for more details.";
			strLicencia+="\r\n* ";
			strLicencia+="\r\n* You should have received a copy of the GNU Lesser General Public";
			strLicencia+="\r\n* License along with this library; if not, write to the Free Software";
			strLicencia+="\r\n* Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307, USA.";
			strLicencia+="\r\n* ";
			strLicencia+="\r\n* BYDAN Corporation";
			strLicencia+="\r\n*/";
			
			return strLicencia;
		}
		#endregion
		
		#region Me Extend Properties
	
	public  String GetPropertyAccionTableFromPropertiesC(MeExtendProperty meExtendProperty,String strProperty)
	{
			
		String[] descripciones;
		String[] tipo;
		String strPropertyValue="";
		
		
			if(meExtendProperty.Value!="")
			{			
				descripciones=meExtendProperty.Value.Split('|');
							
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
								
					if(tipo[0].Equals(strProperty))
					{
						strPropertyValue=tipo[1];
						
						break;
					}
				}
			}	
		
		
		
		
		return strPropertyValue;
	}
	
	public  ArrayList GetPropertyAccionsTableFromPropertiesC(TableSchema tableSchema,String strProperty)
	{
		ArrayList arrAccionPropertyValues=new ArrayList();
		
		ArrayList arrAccionExtendsProperty=new ArrayList();
		
		arrAccionExtendsProperty=GetAccionExtendsPropertyC(tableSchema);
		
		String[] descripciones;
		String[] tipo;
		String strPropertyValue="";
		
		foreach(MeExtendProperty meExtendProperty in arrAccionExtendsProperty)
		{
			if(meExtendProperty.Value!="")
			{			
				descripciones=meExtendProperty.Value.Split('|');
							
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
								
					if(tipo[0].Equals(strProperty))
					{
						strPropertyValue=tipo[1];
						arrAccionPropertyValues.Add(strPropertyValue);
						break;
					}
				}
			}	
		}
		
		
		
		return arrAccionPropertyValues;
	}
	
	public ArrayList GetAccionExtendsPropertyC(TableSchema tableSchema)
	{
		ArrayList arrAccionExtendsProperty=new ArrayList();
		
		arrAccionExtendsProperty=GetExtendsPropertyC(tableSchema,strPrefijoAccionTableExtendProperty);
		
		return arrAccionExtendsProperty;
	}
	
	public ArrayList GetExtendsPropertyC(TableSchema tableSchema,String strTipoExtendProperty)
	{
		ArrayList arrExtendsProperty=new ArrayList();
		
		String strExtend=string.Empty;										 						
		
		MeExtendProperty meExtendProperty=new MeExtendProperty();
		
		foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
		{					
			if(extendedProperty.Name.Contains(strPrefijoMeTableExtendProperty+strTipoExtendProperty))
			{
					meExtendProperty=new MeExtendProperty(extendedProperty.Name,extendedProperty.Value.ToString());
					arrExtendsProperty.Add(meExtendProperty);
			}
		}
		//MeExtendProperty
		return arrExtendsProperty;
	}
	
	#endregion
	
		#region Data Type
		public String GetParameterFunctionColumnC(ColumnSchema columnSchema,bool blnConComaInicial) 
		{
			String strParameterFunctionColumn="";
			String strComa="";
			
			if(blnConComaInicial) {
				strComa=",";
			}
			
			strParameterFunctionColumn+=strComa+GetTipoColumnaClaseC(columnSchema)+" "+GetPrefijoTipoC(columnSchema)+GetNombreColumnaClaseC(columnSchema); 
		
			return strParameterFunctionColumn;
		}
		
		public String GetParameterFunctionUsoColumnC(ColumnSchema columnSchema,bool blnConComaInicial) 
		{
			String strParameterFunctionColumn="";
			String strComa="";
			
			if(blnConComaInicial) {
				strComa=",";
			}
			
			strParameterFunctionColumn+=strComa+GetPrefijoTipoC(columnSchema)+GetNombreColumnaClaseC(columnSchema); 
		
			return strParameterFunctionColumn;
		}
		
		public Boolean EsDecimalColumn(ColumnSchema columnSchema) 
		{
			Boolean isDecimal=false;
			
			if(columnSchema.DataType==DbType.Decimal) {
				 isDecimal=true;
			}
			
			return isDecimal;
		}
		
		public Boolean EsBigIntColumn(ColumnSchema columnSchema) 
		{
			Boolean isBigInt=false;
			
			if(columnSchema.DataType==DbType.Int64) {
				 isBigInt=true;
			}
			
			return isBigInt;
		}
		
		public Boolean EsIntColumn(ColumnSchema columnSchema) 
		{
			Boolean isInt=false;
			
			if(columnSchema.DataType==DbType.Int32) {
				 isInt=true;
			}
			
			return isInt;
		}
	
		public Boolean EsSmallIntColumn(ColumnSchema columnSchema) 
		{
			Boolean isSmallInt=false;
			
			if(columnSchema.DataType==DbType.Int16) {
				 isSmallInt=true;
			}
			
			return isSmallInt;
		}
		
		public Boolean EsCharColumn(ColumnSchema columnSchema) 
		{
			Boolean isChar=false;
			
			if(columnSchema.DataType==DbType.AnsiStringFixedLength) {
				 isChar=true;
			}
			
			return isChar;
		}
		
		public Boolean EsVarCharColumn(ColumnSchema columnSchema) 
		{
			Boolean isVarChar=false;
			
			if(columnSchema.DataType==DbType.AnsiString) {
				 isVarChar=true;
			}
			
			return isVarChar;
		}
		
		public Boolean EsDateTimeColumn(ColumnSchema columnSchema) 
		{
			Boolean isDateTime=false;
			
			if(columnSchema.DataType==DbType.DateTime) {
				if(GetTipoColumnaFromColumn(columnSchema).Equals("Timestamp")) {
					isDateTime=true;
				}
				/*
				if(GetTipoColumnaFromColumn(columnSchema).Equals("")) {
					isDateTime=true;
				} else {
					if(GetTipoColumnaFromColumn(columnSchema).Equals("Timestamp")) {
						isDateTime=true;
					}
				}
				*/ 
			}
			
			return isDateTime;
		}
		
		public Boolean EsDateColumn(ColumnSchema columnSchema) 
		{
			Boolean isDate=false;
			
			if(columnSchema.DataType==DbType.DateTime) {	
				if(GetTipoColumnaFromColumn(columnSchema).Equals("")) {
					isDate=true;
				} else {
					if(GetTipoColumnaFromColumn(columnSchema).Equals("Date")) {
						isDate=true;
					}
				}			
			}
			
			return isDate;
		}
		
		public Boolean EsTimeColumn(ColumnSchema columnSchema) 
		{
			Boolean isTime=false;
			
			if(columnSchema.DataType==DbType.DateTime) {			
				if(GetTipoColumnaFromColumn(columnSchema).Equals("Time")) {
					isTime=true;
				}
			}
			
			return isTime;
		}
		
		public Boolean EsBitColumn(ColumnSchema columnSchema) 
		{
			Boolean isBit=false;
			
			if(columnSchema.DataType==DbType.Boolean) {
				 isBit=true;
			}
			
			return isBit;
		}
		//SE APLICA PARA VERSIONROW,IMAGE Y BINARY PROPIAMENTE DICHO O CAMPO DE ARCHIVO CUALQUIERA
		public Boolean EsBinaryColumn(ColumnSchema columnSchema) 
		{
			Boolean isBinary=false;
			
			if(columnSchema.DataType==DbType.Binary) {
				 isBinary=true;
			}
			
			return isBinary;
		}
		
		//SE APLICA PARA IMAGENES O ARCHIVOS CUALQUIERA
		public Boolean EsImagenArchivoColumn(ColumnSchema columnSchema) 
		{
			Boolean isBinary=false;
			Boolean isImage=false;
			
			isBinary=EsBinaryColumn(columnSchema);
			
			if(columnSchema.NativeType.Equals("image")) {
				 isImage=true;
			}
			
			return isBinary&&isImage;
		}
		
		//TIPOS QUE NO DEBERIAN ESTAR
		
		public Boolean EsTextColumn(ColumnSchema columnSchema) 
		{
			Boolean isText=false;
			
			if(columnSchema.DataType==DbType.AnsiString&&columnSchema.Size==16) {
				 isText=true;
			}
			
			return isText;
		}
		
		public Boolean EsStringNVarCharColumn(ColumnSchema columnSchema) 
		{
			Boolean isStringNVarChar=false;
			
			if(columnSchema.DataType==DbType.String) {
				 isStringNVarChar=true;
			}
			
			return isStringNVarChar;
		}
		
		public Boolean EsPathImagenDocumentoColumn(ColumnSchema columnSchema) 
		{
			Boolean isPathImagenDocumento=false;
			String strPathImagen="PathImagen";
			String strPathDocumento="PathDocumento";
			
			if(columnSchema.Name.Contains(strPathImagen)||columnSchema.Name.Contains(strPathDocumento)) {
				 isPathImagenDocumento=true;
			}
			
			return isPathImagenDocumento;
		}
		
		public Boolean EsPathDocumentoColumn(ColumnSchema columnSchema) 
		{
			Boolean isPathDocumento=false;
			String strPathDocumento="PathDocumento";
			
			if(columnSchema.Name.Contains(strPathDocumento)) {
				 isPathDocumento=true;
			}
			
			return isPathDocumento;
		}
		
		public Boolean EsPathImagenColumn(ColumnSchema columnSchema) 
		{
			Boolean isPathDocumento=false;
			String strPathImagen="PathImagen";
			
			if(columnSchema.Name.Contains(strPathImagen)) {
				 isPathDocumento=true;
			}
			
			return isPathDocumento;
		}
		
		public Boolean EsAutoAuditoriaColumnC(ColumnSchema columnSchema) 
		{
			Boolean isAutoAuditoria=false;
			String strPrefijoUsuario="Aux";
			
			if(columnSchema.Name.Equals(strId+strPrefijoUsuario+"Usuario")
			||columnSchema.Name.Equals("InsertFechaHora")
			||columnSchema.Name.Equals("InsertProceso")
			||columnSchema.Name.Equals(strId+strPrefijoUsuario+"UsuarioUpdate")
			||columnSchema.Name.Equals("UpdateFechaHora")
			||columnSchema.Name.Equals("UpdateProceso")
			) {
				isAutoAuditoria=true;
				return isAutoAuditoria;
			} 
				
			return isAutoAuditoria;
		}
		
		public static String GetNombreConSeparacionC(String strNombre) {
			String strDescritionTabla=string.Empty;
			bool blnEsPrimero=true;
			
			foreach(char c in strNombre) {
				if(!blnEsPrimero && char.IsUpper(c)) {
					strDescritionTabla+="_";
				}
				
				strDescritionTabla+=c.ToString();
				
				if(blnEsPrimero) {
					blnEsPrimero=false;
				}	
			}
			
			return strDescritionTabla;	
		}
		#endregion
		
		#region Me Extra Code
		
		public static string GetExtra1Servlet(TableSchema table)
		{
			return ExtraCode.GetExtra1Servlet(table);
		}
		
		#endregion
	
		
		#region DataAccess Functions
		
		
		public String GetNombreTablaC(TableSchema TablaBase) 
		{
			String strNombreTabla=string.Empty;
										 			
			strNombreTabla=GetNombreTableFromProperties(TablaBase);
								
			return strNombreTabla; 
		}
		
		public string GetParameterNoLastIndexC(TableSchema table)
		{		
			string strNombre =String.Empty; 
			int count=0;	
				
			for (int i = 0; i < table.Columns.Count; i++){ 	
			strNombre=table.Columns[i].Name;
			switch (strNombre)
			{
				case strVersionRow:
				{
					break;
				}
				
				default:
				{
					count++;
					break;
				}
		
			}
				} 
				
			return count.ToString();
		}
		
		public string GetParameterLastIndexC(TableSchema table)
		{		
			string strNombre =String.Empty; 
			int count=0;	
				
			for (int i = 0; i < table.Columns.Count; i++){ 	
		
					count++;
					
		
				} 
				
			return count.ToString();
		}

		public static string GetNombreCampoTablaC(ColumnSchema column)
		{
			string strPrefijo = "\r\n\tpublic static String getColumnName"+column.Name+"()";
			strPrefijo += " {\r\n\t\treturn \""+ GetNombreColumnFromProperties(column)+"\";\r\n\t}";
			
			switch (column.Name)
			{
				case strVersionRow:
				{
					strPrefijo=String.Empty;
					break;
				}	
				case strIsExpired:
				{
					strPrefijo=String.Empty;
					break;
				}	
				case strIsActive:
				{
					strPrefijo=String.Empty;
					break;
				}	
				case strId:
				{
					strPrefijo=String.Empty;
					break;
				}
			}
			
			return strPrefijo;
		}
		
		public static bool ComprobarComlumnaEsTablaTameC(ColumnSchema column,int intContador)
			{
			bool nombreTabla=true;
			
			if(column.Name==strId||column.Name==strIsActive||column.Name==strIsExpired|| column.Name==strVersionRow)
			{
				if(GetEsTablaTameTableFromPropertiesC(column.Table))
				{
					nombreTabla=false;
				}
			}
			else
			{
				//Fuerza que la validacion sea falsa cuando la tabla es de tame y el contador es penultimo, caso contrario
				//devuelve una , al final del select
				
				if(GetEsTablaTameTableFromPropertiesC(column.Table))
				{
					if(intContador == column.Table.Columns.Count - 2)
					{
						nombreTabla=false;
					}
				}
			}
			
			return nombreTabla;
		}
		
		public String GetNombreColumnaIdC(TableSchema TablaBase) 
		{
			String strNombreColumnaId=string.Empty;
										 			
			foreach(ColumnSchema columnSchema in TablaBase.Columns)
			{
					
				if(columnSchema.IsPrimaryKeyMember)
				{
					strNombreColumnaId=GetNombreColumnFromProperties(columnSchema);
				}
			}
								
			return strNombreColumnaId; 
		}
		
		public string GetSqlParameterInsertColumnsC(ColumnSchema column)
		{
			
			String param=GetNombreColumnFromProperties(column)/*+"=?"*/;
		
			switch (column.Name)
			{
				/*
				case strVersionRow:
				{
					param=GetNombreColumnFromProperties(column)+"=CURRENT_TIMESTAMP";
					break;
				}	
				case "isActive":
				{
					param+="=true";
					break;
				}	
				case "isExpired":
				{
					param+="=false";
					break;
				}	
				*/
				case strId:
				{
					param="";
					break;
				}
				
			}
			
			return param;
		}
		
		public string GetSqlParameterInsertValuesC(ColumnSchema column)
		{
			
			String param=/*GetNombreColumnFromProperties(column)+*/"?";
		
			switch (column.Name)
			{
				case strVersionRow:
				{
					param=/*GetNombreColumnFromProperties(column)+*/"now()";
					break;
				}	
				/*case "isActive":
				{
					param+="=true";
					break;
				}	
				case "isExpired":
				{
					param+="=false";
					break;
				}	
				*/
				case strId:
				{
					param="";
					break;
				}
				
			}
			
			return param;
		}
		
		public string GetSqlParameterInsertPostgresC(ColumnSchema column,bool esValor)
		{
			
			String param="";
			
			if(!esValor) {
				param+=GetNombreColumnFromPropertiesC(column,true);
			} else {
				param+="?";
			}
			
			switch (column.Name)
			{
				case strVersionRow:
				{
					if(!esValor) {
						param=GetNombreColumnFromPropertiesC(column,true);
					} else {
						param="current_timestamp";
					}
					
					break;
				}	
				/*case "isActive":
				{
					param+="=true";
					break;
				}	
				case "isExpired":
				{
					param+="=false";
					break;
				}	
				*/
				case strId:
				{
					if(TieneIdentityColumnC(column.Table)) {
						param="";
					} else {
						param+=",";
					}
					
					break;
				}
				
			}
			
			return param;
		}
		
		public string GetSqlStoreProcedureParameterInsertC(ColumnSchema column)
		{
			
			String param="";//GetNombreColumnFromProperties(column);	
			
			param+="?";
			
			switch (column.Name)
			{
				/*
				case strVersionRow:
				{
					param="CURRENT_TIMESTAMP";GetNombreColumnFromProperties(column);
					//param+="=CURRENT_TIMESTAMP";
					break;
				}
				*/
				/*case "isActive":
				{
					param+="=true";
					break;
				}	
				case "isExpired":
				{
					param+="=false";
					break;
				}	
				*/
				case strId:case strVersionRow:
				{
					param="";
					break;
				}
				
			}
			
			return param;
		}
		
		public string GetSqlStoreProcedureParameterUpdateC(ColumnSchema column)
		{
			
			String param="";// GetNombreColumnFromProperties(column);
			
			param+="?";
			
			switch (column.Name)
			{
				/*
				case strVersionRow:
				{
					param="CURRENT_TIMESTAMP";// GetNombreColumnFromProperties(column);
					//param+="=CURRENT_TIMESTAMP";
					break;
				}	
				*/
				case strId:case strVersionRow:
				{
					param=String.Empty;
					break;
				}
			}
			
			return param;
		}
		
		public string GetSqlParameterUpdateC(ColumnSchema column)
		{
			
			String param=GetNombreColumnFromProperties(column)+"=?";
		
			switch (column.Name)
			{
				case strVersionRow:
				{
					param=String.Empty;
					break;
				}	
				case strId:
				{
					param=String.Empty;
					break;
				}
			}
			
			return param;
		}
		
		public string GetSqlParameterSelectC(ColumnSchema column,TableSchema TablaBase,String Schema)
		{
			String param= "";
			
			param=GetSqlParameterSelectC(column,TablaBase,Schema,false);
			
			return param;
		}
		public string GetSqlParameterSelectC(ColumnSchema column,TableSchema TablaBase,String Schema,bool conDbEsquemas)
		{
			//INICIAL
			String param= "";//"\"+"+GetNombreClaseC(TablaBase.ToString())+"DataAccess.SCHEMA+\".\"+"+GetNombreClaseC(TablaBase.ToString())+"DataAccess.TABLENAME"+"+\"."+GetNombreColumnFromProperties(column);
			
			if(conDbEsquemas) {
				//CON ESQUEMA
				param="";// "\"+"+GetNombreClaseC(TablaBase.ToString())+"DataAccess.SCHEMA+\".\"+";//+GetNombreClaseC(TablaBase.ToString())+"DataAccess.TABLENAME"+"+\"."+GetNombreColumnFromProperties(column);
			}
			
			//SE QUITA TABLA
			/*GetNombreClaseC(TablaBase.ToString())+"DataAccess.TABLENAME"+"+\"."+*/
			
			param+= GetNombreColumnFromPropertiesC(column,true);
			
			if(column.Name==strId||column.Name==strIsActive||column.Name==strIsExpired|| column.Name==strVersionRow)
			{
				if(!GetEsTablaTameTableFromPropertiesC(TablaBase))
				{
					//SE QUITA
					//param="\"+"+GetNombreClaseC(TablaBase.ToString())+"DataAccess.SCHEMA+\".\"+"+GetNombreClaseC(TablaBase.ToString())+"DataAccess.TABLENAME"+"+\"."+GetNombreColumnFromProperties(column);
				}
				else
				{	//SE QUITA
					//param="";
				}
			}
			
			return param;
		}
		#endregion
	
	public void InicializarVariablesEmpresaC(String NombreEmpresa,bool blnConFuncionesSqlNativasParam) {
	
	}
	
	public bool TieneClasesRelacionadasOForeignKeyC(TableSchema TablaBase)
	{
		bool blnTieneForeignKey=false;
			
			blnTieneForeignKey=TieneForeignKeyC(TablaBase)||TieneClasesRelacionadasC(TablaBase);
				
		return blnTieneForeignKey;
	}
	
		public bool TieneClasesRelacionadasC(TableSchema TablaBase) 
		{
			bool blnTieneRelaciones=false;
			String strTablaClaseRelacionada="";
			String strFuncionInit="\r\n\tpublic void Save"+GetNombreClaseC(TablaBase.ToString())+"RelacionesDetalles(Long idUsuario";
			
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(TablaBase);
										 			
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
				{
					blnTieneRelaciones=true;
					//strTablaClaseRelacionada+=","+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+ " "+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable);
				}
				else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany)
				{	
					blnTieneRelaciones=true;
				}
				else if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany)
				{
					/*
					if(!VerificarTablaRelacionFromPropertiesC(collectionInfo.JunctionTableSchema))
						{
							continue;
						}
					*/
					blnTieneRelaciones=true;
					//strTablaClaseRelacionada+=",ArrayList<"+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+"> "+GetNombreClaseObjetoC("dbo."+collectionInfo.SecondaryTable)+ "s";
				}
			}
			
			
			
			return blnTieneRelaciones; 
		}
		
		public bool TieneForeignKeyODatoBooleanC(TableSchema TablaBase)
	{
		bool blnTieneForeignKey=false;
			
			blnTieneForeignKey=TieneForeignKeyC(TablaBase)||TieneDatoBooleanC(TablaBase);
				
		return blnTieneForeignKey;
	}

	public bool TieneForeignKeyC(TableSchema TablaBase)
	{
		bool blnTieneForeignKey=false;
			
			foreach(ColumnSchema columnSchema in TablaBase.Columns)
				{
										
					if(columnSchema.IsForeignKeyMember)
					{
						blnTieneForeignKey=true;
					
					}
				}
				
		return blnTieneForeignKey;
	}
	
	public bool TieneDatoBooleanC(TableSchema TablaBase)
	{
		bool blnTieneForeignKey=false;
			
			foreach(ColumnSchema columnSchema in TablaBase.Columns)
				{
					if(columnSchema.Name==strIsActive||columnSchema.Name==strIsExpired||columnSchema.Name==strId||columnSchema.Name==strVersionRow)
					{
						continue; 
					}
					
					if(columnSchema.DataType==DbType.Boolean)
					{
						blnTieneForeignKey=true;
					
					}
				}
				
		return blnTieneForeignKey;
	}
	
		public String SoloSiEsParaSeguridadC(DatabaseSchema databaseSchema) 
		{
			String strSeguridad="";
			
			if(databaseSchema.Name=="Seguridad"||databaseSchema.Name=="SeguridadBasico"||databaseSchema.Name=="Seguridads")
			{
				strSeguridad="Seguridad";
			}			
			
			return strSeguridad;
		}
		
		public void EsConSeguridadC(bool esConSeguridad) 
		{
			strGlobalSeguridadComment="";
			
			if(!esConSeguridad)
			{
				strGlobalSeguridadComment="//";
				strGlobalSeguridadCommentNo="";
			}			
			else
			{
				strGlobalSeguridadCommentNo="//";
			}
			
			//System.Windows.Forms.MessageBox.Show(strGlobalSeguridadComment);
			//return strGlobalSeguridadComment;
		}
		
		public void EsConAuditoriaC(bool esConAuditoria) 
		{
			strGlobalAuditoriaComment="";
			
			if(!esConAuditoria)
			{
				strGlobalAuditoriaComment="//";
			 	strGlobalAuditoriaCommentNo="";
			}			
			else
			{
				strGlobalAuditoriaCommentNo="//";
			}			
			
			//return strGlobalAuditoriaComment;
		}
		
		
		public void EsConAuditoriaInternaC(bool esConAuditoriaInterna) 
		{
			strGlobalAuditoriaInternaComment="";
			
			if(esConAuditoriaInterna)
			{
				strGlobalAuditoriaInternaComment="//";
			 	strGlobalAuditoriaInternaCommentNo="";
			}			
			else
			{
				strGlobalAuditoriaInternaCommentNo="//";
			}			
			
			//return strGlobalAuditoriaComment;
		}
		
		
		public bool ContieneImagenesC(TableSchema TablaBase) 
		{
			bool blContiene=false;
			
			foreach(ColumnSchema columnSchema in TablaBase.Columns)
			{				
				if((columnSchema.NativeType=="image"&&columnSchema.DataType==DbType.Binary&&columnSchema.Name!=strVersionRow)||(columnSchema.DataType==DbType.Binary&&columnSchema.Name!=strVersionRow))
				{
					 blContiene=true;
				}
			}
			return blContiene;
		
		}
		
		public TableSchema GetTablaDetalleClaseRelacionadaC(String strTabla,TableSchema TablaBase) 
		{
			TableSchema tablaRelacionada=null;
			String strBusquedaTablaClaseRelacionada="";
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(TablaBase);
					
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{
				tablaRelacionada=GetTablaFromNombreC(collectionInfo.SecondaryTable,TablaBase);
								
				if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany&&strTabla.Contains(tablaRelacionada.Name))
				{
					break;			
				}
				else
				{
					tablaRelacionada=null;
				}
			}
					
			return tablaRelacionada;
		}

		public bool ExisteBusquedasIndicesTablasClasesC(TableSchema TablaBase) 
		{

			bool existe=false;
			
			
			String strTablaClaseRelacionada=string.Empty;
			
			foreach(IndexSchema indexSchema in TablaBase.Indexes)
			{
			
				if(!indexSchema.IsPrimaryKey)
				{
					
					if(indexSchema.IsUnique)
					{
						continue;
					}
					else
					{
						existe=true;
						
						/*
						if(indexSchema.Name.Contains("FK") ||indexSchema.Name.Contains("fk"))
						{
						}
						*/
					}	
										
				}
				
			}
											
			return existe; 
		}
		
	public  bool GetTableAuxiliarFromPropertiesC(TableSchema table,ref SchemaExplorer.TableSchemaCollection  tablasJunctionrelacionadas,ref SchemaExplorer.TableSchemaCollection tablasDetalleRelacionadas)
	{
		String nombreTabla="false";
		String nombreColumna="false";
		String[] descripciones;
		String[] descripcionesColumna;
		String[] tipo;
		String[] tipoColumna;
		bool esRompimiento=false;
		bool puedeGenerarDetalle=true;
		ColumnSchema columnaSeleccionada;
		columnaSeleccionada=table.Columns[0];
		TableSchema tablaDetalleRelacionada;
		TableSchema junctionTableSchema;
		
		System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(table);
				
		foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
		{
			//System.Windows.Forms.MessageBox.Show(collectionInfo.CollectionRelationshipType.ToString());
			if(collectionInfo.CollectionRelationshipType==RelationshipType.ManyToMany)
			{
				/*
				System.Windows.Forms.MessageBox.Show("Relacion M-M "+table.Name);
				
				
				if(collectionInfo.SecondaryTable!=null)
				{
					System.Windows.Forms.MessageBox.Show("Relacion M-M Secondary "+collectionInfo.SecondaryTable);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("No existe secondary");
				}
				
				if(collectionInfo.JunctionTable!=null)
				{
					System.Windows.Forms.MessageBox.Show("Relacion M-M junction "+collectionInfo.JunctionTable);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("No existe junction");
				}
				*/
				junctionTableSchema=collectionInfo.JunctionTableSchema;
				tablaDetalleRelacionada=collectionInfo.SecondaryTableSchema;
								
				if(junctionTableSchema.Description!="")
				{
					
					descripciones=table.Description.Split('|');
								
						foreach(String descripcion in descripciones)
						{
							tipo=descripcion.Split('=');
									
							if(tipo[0].Equals("NOTABLADETALLE"))
							{
								nombreTabla=tipo[1];
								
								if(nombreTabla.Equals(tablaDetalleRelacionada.Name))
								{
									puedeGenerarDetalle=false;
									break;
								}
							}
						}
				}
								
				if(puedeGenerarDetalle)
				{
					if(!esRompimiento)
					{
						esRompimiento=true;
					}
					
					puedeGenerarDetalle=true;
					
					tablasJunctionrelacionadas.Add(junctionTableSchema);
					tablasDetalleRelacionadas.Add(tablaDetalleRelacionada);
				}
				else
				{
					puedeGenerarDetalle=true;
				}
			}
		}
			
						
		
		return esRompimiento;
	}
	
		public  String GetJavascriptColumnaDescripcionComboFromTablaPropertiesC(TableSchema tablaRelacionada)
	{
	
		
	string strPrefijoTabla="";
	string strPrefijoTipo =""; 
	string strNombre = "";
	
	String strColumnaDetalle="id";
	String[] descripciones;
	String[] tipo;
	
	foreach(ColumnSchema columnSchema in tablaRelacionada.Columns)
	{
		
		foreach(ExtendedProperty extendedProperty in columnSchema.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
			descripciones=((String)extendedProperty.Value).Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WEBCOMBO"))
						{
							//if(tipo[1]=="true")
							//{
																
								strColumnaDetalle=GetNombreColumnaClaseJavaScriptC(columnSchema);
							//}
							
							break;
						}
					}
			}
					
		}
	}
				
	return strColumnaDetalle;
	}
	
	public static string GetCascadeTableFromPropertiesC(TableSchema table)
	{
		String nombreTabla="false";
		String[] descripciones;
		String[] tipo;
		
		if(table.Description!="")
		{
			
			descripciones=table.Description.Split('|');
						
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
							
					if(tipo[0].Equals("DELCASCADE"))
					{
						nombreTabla=tipo[1];
						
						break;
					}
				}
		}
		
		return nombreTabla;
	}
	
	public  String GetJavascriptColumnaDescripcionComboFromPropertiesC(ColumnSchema column)
	{
	
	TableSchema tablaRelacionada=column.Table;//GetNombreTablaRelacionadaFromColumn(column);
	
	String strNombreTabla=GetNombreClaseRelacionadaFromColumn(column);
	
	foreach(TableSchema tableForeignKey in column.Database.Tables)
	{
		if(tableForeignKey.Name.Equals(strNombreTabla))
		{
			tablaRelacionada=tableForeignKey;
		}
	}
	
	string strPrefijoTabla="";
	string strPrefijoTipo =""; 
	string strNombre = "";
	
	String strColumnaDetalle="id";
	String[] descripciones;
	String[] tipo;
	
	foreach(ColumnSchema columnSchema in tablaRelacionada.Columns)
	{
		if(columnSchema.Name!=strId)
		{
			foreach(ExtendedProperty extendedProperty in columnSchema.ExtendedProperties)
			{
				if(extendedProperty.Name=="CS_Description")
				{
				descripciones=((String)extendedProperty.Value).Split('|');
						
						foreach(String descripcion in descripciones)
						{
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("WEBCOMBO"))
							{
								//if(tipo[1]=="true")
								//{
																	
									strColumnaDetalle=GetNombreColumnaClaseJavaScriptC(columnSchema);
								//}
								
								break;
							}
						}
				}
						
			}
		}
	}
				
	return strColumnaDetalle;
	}
	
	public System.Collections.Hashtable GetTablasRelacionadas(SchemaExplorer.TableSchema table) {
		Hashtable hashChildrenTablesFinalFinal=new System.Collections.Hashtable();
		
		hashChildrenTablesFinalFinal=GetTablasRelacionadas(table,true);	
		
		return hashChildrenTablesFinalFinal;
	}
	
	public System.Collections.Hashtable GetTablasRelacionadas(SchemaExplorer.TableSchema table,bool conTodasRelaciones) 
		{
			SchemaExplorer.TableSchemaCollection tablasRelacionadas=new SchemaExplorer.TableSchemaCollection();
							
			String strClasesNoRelacionadas=string.Empty;
			
			strClasesNoRelacionadas=GetNombresClasesNoNavegacionFromTableFromPropertiesC(table);
			
			if(strClasesNoRelacionadas.Equals("NINGUNO")) {
				return new System.Collections.Hashtable();
			}
			
			for (int i = 0; i < table.Database.Tables.Count; i++)
			{ 	
         	if(table.Database.Tables[i].Equals(table))
			{
				continue;
			}
			tablasRelacionadas.Add(table.Database.Tables[i]);
			}
							
			System.Collections.Hashtable hashChildrenTables=GetChildrenCollections(table, tablasRelacionadas);				
			System.Collections.Hashtable hashChildrenTablesFinal=new System.Collections.Hashtable();
			System.Collections.Hashtable hashChildrenTablesFinalFinal=new System.Collections.Hashtable();
			
			ArrayList arrayListChildrenTables=new ArrayList();
			
			foreach(CollectionInfo collectionInfo in hashChildrenTables.Values)	{
				if(!ExisteRelacion(arrayListChildrenTables,collectionInfo.SecondaryTable)){
					arrayListChildrenTables.Add(collectionInfo.SecondaryTable);
					//Trace.WriteLine(collectionInfo.SecondaryTable);
					//Trace.WriteLine(collectionInfo.PropertyName);
					hashChildrenTablesFinal.Add(collectionInfo.PropertyName,collectionInfo);
				}
			}
			
			String sClasesNoRelacionadas=String.Empty;
			String[] sClases;
			bool blClaseNo=false;
			TableSchema tablaRelacionadaObjetivo;							
				
			foreach(CollectionInfo collectionInfo in hashChildrenTablesFinal.Values)	{
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				} else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany) {					
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				} else {
					tablaRelacionadaObjetivo=collectionInfo.JunctionTableSchema;
				}
				
				if(!conTodasRelaciones) {
					sClasesNoRelacionadas=GetNombresClasesNoNavegacionFromTableFromPropertiesC(table);
									
					sClases=sClasesNoRelacionadas.Split(',');
					
					blClaseNo=false;
					
					foreach(String sClase in sClases){						
						if(sClase.Equals(GetNombreClaseC(tablaRelacionadaObjetivo.ToString()))) {
							blClaseNo=true;
							break;
						}
					}
						
					if(blClaseNo) {
						continue;
					}
				}
				
				hashChildrenTablesFinalFinal.Add(collectionInfo.PropertyName,collectionInfo);
			}
			
			//Trace.WriteLine(hashChildrenTablesFinalFinal.Count);
			//return hashChildrenTables; 
			return hashChildrenTablesFinalFinal; 
		}
		
		public ArrayList GetTablasRelacionadasFinal(TableSchema table) {
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(table);
			
			TableSchema tablaRelacionadaObjetivo;
			ArrayList tablasRelacionadasEncontradas=new ArrayList();
			bool encontrado=false;
			String sClasesNoRelacionadas=String.Empty;
			String[] sClases;
			bool blClaseNo=false;
			
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values)
			{
				
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne)
				{
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}	
				else if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany)
				{
					
					tablaRelacionadaObjetivo=collectionInfo.SecondaryTableSchema;
				}
				else
				{
					tablaRelacionadaObjetivo=collectionInfo.JunctionTableSchema;
				}
				
				
				sClasesNoRelacionadas=GetNombresClasesNoNavegacionFromTableFromPropertiesC(table);
				
				sClases=sClasesNoRelacionadas.Split(',');
				
				blClaseNo=false;
				
				foreach(String sClase in sClases)
				{						
					if(sClase.Equals(GetNombreClaseC(tablaRelacionadaObjetivo.ToString())))
					{
						blClaseNo=true;
						break;
					}
				}
					
				if(blClaseNo)
				{
					continue;
				}
												
				encontrado=false;
				
				foreach(TableSchema tableSchema in tablasRelacionadasEncontradas)
				{										
					if(tableSchema.Name.Equals(tablaRelacionadaObjetivo.Name))
					{
						encontrado=true;
					}
				}				
				
				if(!encontrado)
				{
					tablasRelacionadasEncontradas.Add(tablaRelacionadaObjetivo);
					//sTablaClaseRelacionada+=GetCargarTablesRelacionadas(tablaRelacionadaObjetivo,collectionInfo);
				}				
			}
			
		return tablasRelacionadasEncontradas;
	}
	
	public int GetNumeroClasesRelacionadasFinalC(TableSchema table) {
		int iTotal=0;
		
		String sTablaClaseRelacionada=String.Empty;
		
		ArrayList tablasRelacionadasEncontradas=GetTablasRelacionadasFinal(table);
		
		iTotal=tablasRelacionadasEncontradas.Count;
						
		return iTotal;
	}
	
		public bool ExisteRelacion(ArrayList arrayListChildrenTables,String strSecondaryTableItem) {
			bool blnExiste=false;
			
			foreach(String strSecondaryTable in arrayListChildrenTables) {
				if(strSecondaryTable.Equals(strSecondaryTableItem)) {
					blnExiste=true;	
				}
			}
			
			return blnExiste;
		}
		
	public SchemaExplorer.TableSchemaCollection GetTablasRelacionadasDondeTablaEsForeignKeyC(SchemaExplorer.TableSchema table) 
		{
			SchemaExplorer.TableSchemaCollection tablasRelacionadas=new SchemaExplorer.TableSchemaCollection();
										 			
			for (int i = 0; i < table.Database.Tables.Count; i++)
			{ 	
				if(table.Database.Tables[i].Equals(table))
				{
					//continue;
				}
				
				
				foreach(ColumnSchema columnSchema in table.Database.Tables[i].Columns)
				{
					if(!columnSchema.IsForeignKeyMember)
					{
						continue;
					}
					
					if(GetNombreClaseRelacionadaFromColumn(columnSchema).Equals(table.Name))
					{
						tablasRelacionadas.Add(table.Database.Tables[i]);
						break;
					}
				}
			}
								
			return tablasRelacionadas; 
		}
		
	public static string GetTituloNombreTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla=GetNombreClaseC(table.ToString());
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WEBTITULO"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetAnchoAltoAuxiliarTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WHAUX"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public string GetAnchoAuxiliarTableFromPropertiesC(TableSchema table)
	{
		String strAnchoAuxiliar="";
		String nombreTabla="";
		nombreTabla=GetAnchoAltoAuxiliarTableFromPropertiesC(table);
		
		//EL ANCHO YA TOMA EN CUENTA UNA COLUMNA
		int intNumeroColumnasForm=GetAlignVerticalFormNumeroColumnasTableC(table);
			intNumeroColumnasForm--;
			
		int intAnchoNumeroColumnas=intNumeroColumnasForm*280;
		int intAncho=0;
		
		if(nombreTabla.Equals("")) {
			if(blnTieneImagen || blnTieneDocumento || blnTieneTextArea) {
				intAncho=400+intAnchoNumeroColumnas;
				strAnchoAuxiliar=intAncho.ToString();
			} else {
				intAncho=330+intAnchoNumeroColumnas;
				strAnchoAuxiliar=intAncho.ToString();
			}
		} else {
			strAnchoAuxiliar=nombreTabla.Split(',')[0];
		}
		//Trace.WriteLine(strAnchoAuxiliar);
		return strAnchoAuxiliar;
	}
	
	public static string GetAltoAuxiliarTableFromPropertiesC(TableSchema table)
	{
		String strAnchoAuxiliar="";
		String nombreTabla="";
		nombreTabla=GetAnchoAltoAuxiliarTableFromPropertiesC(table);
		int intValorTotal=0;
		int intValorBase=150;
		int intValorBasePorColumna=85;
		
		int intNumeroColumnas=GetNumeroColumnasC(table);
		
		
		if(nombreTabla.Equals("")) {
			intValorTotal=intValorBase+(intNumeroColumnas*intValorBasePorColumna);
			
			//System.Windows.Forms.MessageBox.Show(intValorTotal.ToString());
			
			if(intValorTotal<400) {
				strAnchoAuxiliar=intValorTotal.ToString();
			} else if (intValorTotal<600 && intNumeroColumnas<=4) {
				strAnchoAuxiliar="350";
			} else {
				strAnchoAuxiliar="500";
			}
			
		} else {
			strAnchoAuxiliar=nombreTabla.Split(',')[1];
		}
		
		return strAnchoAuxiliar;
	}
	
	public static string GetTopAuxiliarTableFromPropertiesC(TableSchema table)
	{
		String strAnchoAuxiliar="";
		String nombreTabla="";
		nombreTabla="";//GetAnchoAltoAuxiliarTableFromPropertiesC(table);
		int intValorTotal=0;
		int intValorBase=200;
		int intValorBasePorColumna=35;
		
		int intNumeroColumnas=GetNumeroColumnasC(table);
		
		if(nombreTabla.Equals("")) {
			strAnchoAuxiliar="200";
			
			intValorTotal=intValorBase-(intNumeroColumnas*intValorBasePorColumna);
			
			//System.Windows.Forms.MessageBox.Show(intValorTotal.ToString());
			
			if(intValorTotal<=0) {
				strAnchoAuxiliar="40";
				
			} else if(intValorTotal<200&&intNumeroColumnas<=4) {
				strAnchoAuxiliar="200";
				
			} else {
				strAnchoAuxiliar=intValorTotal.ToString();
			}
			
		} else {
			strAnchoAuxiliar=nombreTabla.Split(',')[0];
		}
		
		return strAnchoAuxiliar;
	}
	
	public static bool GetEsTablaTameTableFromPropertiesC(TableSchema table)
	{
	bool nombreTabla=false;
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("ESTABLATAME"))
						{
							nombreTabla=true;
							break;
						}
					}
	}		
	//System.Windows.Forms.MessageBox.Show(nombreTabla.ToString());
	return nombreTabla;
	}
	
	public static int GetNumeroColumnasC(TableSchema TablaBase)
	{
		int intNumeroColumnas=0;
		
		for (int i = 0; i < TablaBase.Columns.Count; i++){ 
			if(TablaBase.Columns[i].Name==strIsActive||TablaBase.Columns[i].Name==strIsExpired||TablaBase.Columns[i].Name==strId||TablaBase.Columns[i].Name==strVersionRow){continue; }
			intNumeroColumnas++;
		}
		
		return intNumeroColumnas;
	}
	
	public static int GetNumeroColumnasPkC(TableSchema TablaBase)
	{
		int intNumeroColumnas=0;
		
		for (int i = 0; i < TablaBase.Columns.Count; i++){ 
			//if(TablaBase.Columns[i].Name==strIsActive||TablaBase.Columns[i].Name==strIsExpired||TablaBase.Columns[i].Name==strId||TablaBase.Columns[i].Name==strVersionRow){continue; }
			if(TablaBase.Columns[i].IsPrimaryKeyMember) {
				intNumeroColumnas++;
			}
		}
		
		return intNumeroColumnas;
	}
	
	public static bool GetEsIgnorarTableFromPropertiesC(TableSchema table)
	{
	bool nombreTabla=false;
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("IGNORAR"))
						{
							nombreTabla=true;
							break;
						}
					}
	}		
	//System.Windows.Forms.MessageBox.Show(nombreTabla.ToString());
	return nombreTabla;
	}
	
	public static string GetAlignTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="center";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("ALIGN"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public int GetAlignHorizontalParameterTableC(TableSchema table)
	{
		//String nombreTabla="center";
		int intCountLimiteCenter=0;
		
		foreach(ColumnSchema columnSchema in table.Columns) {
			if(EsAutoAuditoriaColumnC(columnSchema)||columnSchema.Name==strIsActive||columnSchema.Name==strIsExpired||/*columnSchema.Name==strId||*/columnSchema.Name==strVersionRow) {
				continue; 
			} 
			
			intCountLimiteCenter+=GetValorHorizontalWebColumnaClaseC(columnSchema);
		}	
		
		return intCountLimiteCenter;
	}
	
	public int GetAlignVerticalParameterTableC(TableSchema table)
	{
		//String nombreTabla="center";
		int intCountLimiteCenter=0;
		
		foreach(ColumnSchema columnSchema in table.Columns) {
			if(EsAutoAuditoriaColumnC(columnSchema)||columnSchema.Name==strIsActive||columnSchema.Name==strIsExpired||/*columnSchema.Name==strId||*/columnSchema.Name==strVersionRow) {
				continue; 
			} 
			
			intCountLimiteCenter+=GetValorVerticalWebColumnaClaseC(columnSchema);
		}	
		
		return intCountLimiteCenter;
	}
	
	public int GetAlignVerticalFormNumeroColumnasTableC(TableSchema table)
	{
		//String nombreTabla="center";
		int intCountLimiteCenter=0;
		int intCountNumeroColumnasAbsoluto=0;
		int intCountNumeroColumnas=0;
		int intValorLimite=100;
		int intNumeroColumnasFromProperties=0;
		
		intNumeroColumnasFromProperties=GetNumeroColumnasTableFromPropertiesC(table);
		
		if(intNumeroColumnasFromProperties<=0) {
			intCountLimiteCenter=GetAlignVerticalParameterTableC(table);
			//Trace.WriteLine("No Total Columnas:"+intCountLimiteCenter);
			
			if(intCountLimiteCenter>intValorLimite) {
				intCountNumeroColumnasAbsoluto=intCountLimiteCenter/intValorLimite;
				
				//Trace.WriteLine("No Total Form Columnas:"+intCountNumeroColumnasAbsoluto);
				
				int intResiduo= intCountLimiteCenter % intValorLimite;
				
				//Trace.WriteLine("RESIUDO"+intResiduo);
				
				if(intResiduo>=30) {
					intCountNumeroColumnasAbsoluto++;
				}
	
				if(intCountNumeroColumnasAbsoluto>0) {
					intCountNumeroColumnas=intCountNumeroColumnasAbsoluto;
				} else {
					intCountNumeroColumnas=1;
				}
			} else {
				intCountNumeroColumnas=1;
			}
		}  else {
			intCountNumeroColumnas=intNumeroColumnasFromProperties;
		}
		
		return intCountNumeroColumnas;
	}
	
	public static string GetWidthBusquedaTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="50%";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("TAMBUSQUEDA"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public static int GetNumeroColumnasTableFromPropertiesC(TableSchema table)
	{
	int intNumerocolumnas=0;
	String nombreTabla="50%";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NUMCOLUMNAS"))
						{
							nombreTabla=tipo[1];
							intNumerocolumnas=int.Parse(nombreTabla);
							break;
						}
					}
	}							
	return intNumerocolumnas;
	}
	
	public static int GetNumeroColumnasExtraTablaTableFromPropertiesC(TableSchema table)
	{
	int intNumerocolumnas=0;
	String nombreTabla="50%";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NUMCOLUMNASEXTRA"))
						{
							nombreTabla=tipo[1];
							intNumerocolumnas=int.Parse(nombreTabla);
							break;
						}
					}
	}							
	return intNumerocolumnas;
	}
	
	public static string GetNombreTableForeingKeyFromPropertiesC(TableSchema table)
	{
	String nombreTabla=GetNombreTableFromProperties(table);
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NOMBREFK"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetConAtrasTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="false";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WEBCONATRAS"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	#region NO VALIDACIONES
	public static bool GetNoIdTableFromPropertiesC(TableSchema table) {
		bool blnNoId=false;
		String nombreTabla="s";
		String[] descripciones;
		String[] tipo;
		
		if(table.Description!="") {
			
			descripciones=table.Description.Split('|');
						
						foreach(String descripcion in descripciones) {
							tipo=descripcion.Split('=');
							
							if(tipo[0]!=null) {
								if(tipo[0].Equals("NOID")) {
									nombreTabla=tipo[1];
									blnNoId=true;
									break;
								}
							}
						}
		}
		
		return blnNoId;
	}
	
	public static bool GetNoIdentityTableFromPropertiesC(TableSchema table) {
		bool blnNoIdentity=false;
		String nombreTabla="s";
		String[] descripciones;
		String[] tipo;
		
		if(table.Description!="") {
			
			descripciones=table.Description.Split('|');
						
						foreach(String descripcion in descripciones) {
							tipo=descripcion.Split('=');
							
							if(tipo[0]!=null) {
								if(tipo[0].Equals("NOIDENTITY")) {
									nombreTabla=tipo[1];
									blnNoIdentity=true;
									break;
								}
							}
						}
		}
		
		return blnNoIdentity;
	}
	
	public static bool GetNoVersionRowTableFromPropertiesC(TableSchema table) {
		bool blnNoVersionRow=false;
		String nombreTabla="s";
		String[] descripciones;
		String[] tipo;
		
		if(table.Description!="") {
			
			descripciones=table.Description.Split('|');
						
						foreach(String descripcion in descripciones) {
							tipo=descripcion.Split('=');
							
							if(tipo[0]!=null) {
								if(tipo[0].Equals("NOVERSIONROW")) {
									nombreTabla=tipo[1];
									blnNoVersionRow=true;
									break;
								}
							}
						}
		}
		
		return blnNoVersionRow;
	}
	
	public bool GetNoStandardTableFromPropertiesC(TableSchema table) {
		bool blnNoStandard=false;
		String nombreTabla="s";
		String[] descripciones;
		String[] tipo;
		
		if(table.Description!="") {
			
			descripciones=table.Description.Split('|');
						
						foreach(String descripcion in descripciones) {
							tipo=descripcion.Split('=');
							
							if(tipo[0]!=null) {
								if(tipo[0].Equals("NOSTANDARD")) {
									nombreTabla=tipo[1];
									blnNoStandard=true;
									break;
								}
							}
						}
		}
		
		return blnNoStandard;
	}
	#endregion 
	
	public static string GetPluralTituloNombreTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="s";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("WEBPLURAL"))
							{
								nombreTabla=tipo[1];
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetFinalQueryTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("FINALQUERY"))
							{
								nombreTabla=tipo[1];
								nombreTabla=nombreTabla.Replace("()","=");
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetPrefijoSqlIdTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("PREFIJOID"))
							{
								nombreTabla=tipo[1];
								nombreTabla=nombreTabla.Replace("()","=");
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetRelativePathC(TableSchema table)
	{
		String strRelativePath=strPrefijoRelativePath;
		
		String strRelativePathFromTableProperties=GetRelativePathNavegacionFromTableFromPropertiesC(table);
		
		String strPath=String.Empty;
		
		String[] strRelativePathsFromTableProperties=strRelativePathFromTableProperties.Split('/');
		bool blnEsPrimera=true;
		
		foreach(String strRelativePathFromTable in strRelativePathsFromTableProperties)
		{
			if(!blnEsPrimera)
			{
				strPath+="../";
			}
			else
			{
				blnEsPrimera=false;
			}
		}
		
		return strRelativePath+strPath+"";
	}
	
	public static string GetRelativePathNavegacionFromTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("PAQUETE"))
							{
								nombreTabla=tipo[1];
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetNombresClasesNoNavegacionFromTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("CLASESNO"))
							{
								nombreTabla=tipo[1];
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public static string GetNombresClasesNoPersistenciaFromTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("CLASESPERSISTENCENO"))
							{
								nombreTabla=tipo[1];
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public static bool ExisteNombresClasesNoPersistenciaFromTableFromPropertiesC(TableSchema tableOrigen,TableSchema tableRelacionada)
	{
		String nombreTabla="";
		String strClasesNoRelacionadas=GetNombresClasesNoPersistenciaFromTableFromPropertiesC(tableOrigen);
				
		String[] strClases=strClasesNoRelacionadas.Split(',');
				
		bool blClaseNo=false;
				
				foreach(String strClase in strClases)
				{						
					if(strClase.Equals(GetNombreClaseC(tableRelacionada.ToString())))
					{
						blClaseNo=true;
						break;
					}
				}
					
				
		return blClaseNo;
	}
	
	public static string GetNombresIndicesNoBusquedanFromTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0]!=null)
						{
							if(tipo[0].Equals("INDICESNO"))
							{
								nombreTabla=tipo[1];
								break;
							}
						}
					}
	}							
	return nombreTabla;
	}
	
	public bool VerificarIndiceBusquedaTablaC(TableSchema table,String strNombreIndice) 
		{
			String strTablaClaseRelacionada=string.Empty;
			
			TableSchema tablaRelacionadaObjetivo;
			ArrayList tablasRelacionadasEncontradas=new ArrayList();
			bool encontrado=false;
			String strClasesNoRelacionadas=string.Empty;
			String[] strClases;
			bool blVerificado=true;
			
			
				
				
				
				strClasesNoRelacionadas=GetNombresClasesNoNavegacionFromTableFromPropertiesC(table);
				
				strClases=strClasesNoRelacionadas.Split(',');
				
								
				foreach(String strClase in strClases)
				{						
					if(strClase.Equals(strNombreIndice))
					{
						blVerificado=false;
						break;
					}
				}
						
								
			return blVerificado; 
		}
		
	public static bool TieneIdentityColumnC(ColumnSchema columnSchemaPK) {
	bool blnValidacionPK=true;
	
	if(!columnSchemaPK.IsForeignKeyMember) {
			foreach(ExtendedProperty extendedProperty in columnSchemaPK.ExtendedProperties)
			{
				if(extendedProperty.Name.Equals("CS_IsIdentity"))
				{
					if(extendedProperty.Value.Equals(false))
					{
						if(!GetNoIdTableFromPropertiesC(columnSchemaPK.Table) && !GetNoIdentityTableFromPropertiesC(columnSchemaPK.Table)) {
							blnValidacionPK=false;
							
							//PUEDE NO VALIDAR IDENTITY PERO DEBEN SER TABLAS CON NOMBRE ESTADO
							/*
							if(!C1_EsConPkNOAutoNumerico || (C1_EsConPkNOAutoNumerico && !table.Name.Contains("Estado"))) {
								blnValidacionPK=true;
								strValidation +="No es identity </br>\r\n";
								
							}
							*/
						}
					}
				}
			}
		}	
		
	return blnValidacionPK;
}

	public bool GetConMaximoRelacionesC(TableSchema tableSchema) {
		bool blnValidacionPK=true;
		
		//Trace.WriteLine(GetNumeroClasesRelacionadasFinalC(tableSchema));
		
		if((GetNumeroClasesRelacionadasFinalC(tableSchema) >5 && (GetNombresClasesNoNavegacionFromTableFromPropertiesC(tableSchema).Equals(""))) || GetNombresClasesNoNavegacionFromTableFromPropertiesC(tableSchema).Equals("NINGUNO")) {
			 blnValidacionPK=false;
			
		}
		
		return blnValidacionPK;
	}
	
	public bool TieneIdentityColumnC(TableSchema tableSchema) {
		bool blnValidacionPK=false;
		
		foreach(ColumnSchema columnSchemaPK in tableSchema.Columns) {
			if(!columnSchemaPK.IsForeignKeyMember) {
				foreach(ExtendedProperty extendedProperty in columnSchemaPK.ExtendedProperties)
				{
					if(extendedProperty.Name.Equals("CS_IsIdentity"))
					{
						if(extendedProperty.Value.Equals(true))
						{
							blnValidacionPK=true;	
							
							break;
							//if(!GetNoIdTableFromPropertiesC(columnSchemaPK.Table) && !GetNoIdentityTableFromPropertiesC(columnSchemaPK.Table)) {
								//blnValidacionPK=false;
								
								//PUEDE NO VALIDAR IDENTITY PERO DEBEN SER TABLAS CON NOMBRE ESTADO
								/*
								if(!C1_EsConPkNOAutoNumerico || (C1_EsConPkNOAutoNumerico && !table.Name.Contains("Estado"))) {
									blnValidacionPK=true;
									strValidation +="No es identity </br>\r\n";
									
								}
								*/
							//}
							
						} else {
							
						}
					}
				}
			}	
		}
		
		return blnValidacionPK;
	}
	
	public string GetNombreClaseRelacionadaFromColumn0(ColumnSchema column)
	{
	String nombreClase="NONE";
	String tabla=column.Name.Replace(strId,"");	
	String[] descripciones;
	String[] tipo;
	
	if(tabla!="")
	{
			nombreClase=tabla;
	}
	else
	{
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{	
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("TABLA"))
						{
							nombreClase=tipo[1];
							break;
						}
					}
					break;
			}		
		}
			
	}
	
	return nombreClase;
	}
	
	public string GetNombreClaseRelacionadaFromColumn(ColumnSchema column)
	{
		String nombreClase="NONE";
		
		//ANTES FUNCIONABA PERO SIN CATALOGOS GENERALES
		//nombreClase=GetNombreClaseRelacionadaFromColumn(column,false);
		
		TableSchema tableSchemaFK=GetTableSchemaFromColumnForeignKey(column);
		
		if(tableSchemaFK!=null) {
			nombreClase=tableSchemaFK.ToString().Replace(tableSchemaFK.Owner+".","");
		} else {
			nombreClase=GetNombreClaseRelacionadaFromColumn(column,false);
		}
		
		return nombreClase;
	}
	
	//SI ES true ES PARA RELACION UNO A UNO	
	public static string GetNombreClaseRelacionadaFromColumn(ColumnSchema column,bool blnEsParaUnoAUno)
	{
	String nombreClase="NONE";
	String tabla=column.Name.Replace(strId,"");
	String[] descripciones;
	String[] tipo;
	
	if(tabla!="") {	
		if(!blnEsParaUnoAUno) {
			nombreClase=GetNombreClaseRelacionadaImproveFromColumn(column);//column.Name.Substring(2,column.Name.Length-2);//tabla;
		} else {
			nombreClase="";
		}
	}
	else
	{
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TABLA"))
					{
						nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}
			
	}
	
	return nombreClase;
	}
	
	//CON ESTA FUNCION PERMITE TENER MAS DE 1 FOREIGN KEY CON LA MISMA TABLA
	public static string GetNombreClaseRelacionadaImproveFromColumn(ColumnSchema column)
	{
		String nombreClase=column.Name.Substring(2,column.Name.Length-2);
		bool existeNombreClase=false;
		
		foreach(TableSchema tableSchema in column.Database.Tables) {
			if(nombreClase.Equals(tableSchema.Name)) {
				existeNombreClase=true;
			}
		}
		
		String strUltimaPalabra="";
		String strUltimaPalabraOrdenada="";
		
		char charActual='a';
		
			
			
		if(!existeNombreClase) {
			for(int i=nombreClase.ToCharArray().Length-1;i>-1;i--) {
				charActual=nombreClase.ToCharArray()[i];
				strUltimaPalabra+=charActual.ToString();
					
				if(Char.IsUpper(charActual)) {
					break;
				}
			}
			
			for(int j=strUltimaPalabra.ToCharArray().Length-1;j>-1;j--) {
				charActual=strUltimaPalabra.ToCharArray()[j];
				
				strUltimaPalabraOrdenada+=charActual.ToString();
			}
			
			nombreClase=nombreClase.Replace(strUltimaPalabraOrdenada,"");
			//Trace.WriteLine(column.Name+"-->"+nombreClase);
		
		}
		
		return nombreClase;
	}
	
		//ESTA FUNCION PERMITE TENER EL NOMBRE DEL FOREIGN KEY, DONDE PUEDE TENER MAS DE 1 CON LA MISMA TABLA
	public  string GetNombreCompletoClaseRelacionadaFromColumn(ColumnSchema column)
	{
	String nombreClase="NONE";
	String tabla=column.Name.Replace(strId,"");
	String[] descripciones;
	String[] tipo;
	TableSchema tableSchemaRelacionadaFK=null;
	
	if(tabla!="")
	{	nombreClase=column.Name.Substring(2,column.Name.Length-2);//tabla;			
	}
	else
	{
		tableSchemaRelacionadaFK=GetTableSchemaFromColumnForeignKey(column);
		
		if(tableSchemaRelacionadaFK!=null) {
			nombreClase=GetNombreClaseC(tableSchemaRelacionadaFK.ToString());
			
			return nombreClase;
		}
		
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TABLACOMPLETA"))
					{
						nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}				
			
	}
	
	return nombreClase;
	}
	
	//ESTA FUNCION PERMITE TENER EL NOMBRE ADICIONAL DEL FOREIGN KEY, DONDE PUEDE TENER MAS DE 1 CON LA MISMA TABLA
	public string GetNombreAdicionalClaseRelacionadaFromRelation(CollectionInfo collectionInfo)
	{
		String nombreAdicional="";
		
		TableKeySchema tableKeySchema=collectionInfo.TableKey;
		
		String strNombreTablaPrimary=tableKeySchema.PrimaryKeyTable.Name;
		
		String strNombreTablaForeign=tableKeySchema.ForeignKeyTable.Name;
		
		//Trace.WriteLine("Table Primary:"+strNombreTablaPrimary);		
		//Trace.WriteLine("Table Foreign:"+strNombreTablaForeign);		
		
		//EL REEMPLAZO ES EN BASE A LA UNICA CLAVE FOREIGN OSEA UNICA PK id
		if(tableKeySchema.ForeignKeyMemberColumns.Count.Equals(1)) {
			String strNombreColumnaFKSecondary=tableKeySchema.ForeignKeyMemberColumns[0].Column.Name;
			//Trace.WriteLine("Column Secondary:"+strNombreColumnaFKSecondary);
			//Trace.WriteLine(strNombreTablaPrimary);
			//Trace.WriteLine("fk Primary:"+strNombreColumnaFKSecondary);
			if(!strNombreColumnaFKSecondary.Equals(strId)) {
				nombreAdicional=strNombreColumnaFKSecondary.Replace(strId+strNombreTablaPrimary,"");
				//Trace.WriteLine(strNombreTablaForeign+nombreAdicional);
				//Trace.WriteLine("Adicional:"+nombreAdicional+"--"+strNombreColumnaFKSecondary+"---"+strId+strNombreTablaPrimary);
			}
		}
		
		//Trace.WriteLine("_____________________________________");
		
		return nombreAdicional;
	}
	
	//ESTA FUNCION PERMITE TENER EL NOMBRE DE ONE TO MANY, DONDE PUEDE TENER MAS DE 1 CON LA MISMA TABLA
	public  string GetNombreExtraClaseRelacionadaFromRelacionC(TableKeySchema tableKeySchema)
	{
	TableSchema tableSchemaPK=tableKeySchema.PrimaryKeyTable;
	
	String nombreClase="";
	String tabla="";//column.Name.Replace(strId,"");
	String[] descripciones;
	String[] tipo;
	TableSchema tableSchemaRelacionadaFK=null;
	
	foreach(ColumnSchema columnSchema in tableKeySchema.ForeignKeyMemberColumns) {
		tabla+=columnSchema.Name.Replace(strId+tableSchemaPK.Name,"");
	}
		
	/*
	if(tabla!="")
	{	nombreClase=column.Name.Substring(2,column.Name.Length-2);//tabla;			
	}
	*/
	
	return tabla;
	}
	
	public static bool ExisteTablasClasesYaRelacionadas(ArrayList arrayList,String SecondaryTable) {
		bool blnExiste=false;
		
		foreach(String strSecondaryTable in arrayList) {
			if(strSecondaryTable.Equals(SecondaryTable)) {
				blnExiste=true;
				break;
			}
		}
		
		return blnExiste;
	}
	
	public static bool EsClaseRelacionadaUnoAUnoFromColumnC(ColumnSchema column)
	{
	String nombreClase="NONE";
	String tabla=column.Name.Replace(strId,"");
	String[] descripciones;
	String[] tipo;
	bool esRelacionUnoAUno=false;
	
	if(tabla=="")
	{
		esRelacionUnoAUno=true;
			
	}
		return esRelacionUnoAUno;
	}
	
	public TableSchema GetNombreTablaRelacionadaFromColumn(ColumnSchema column)
	{
	
		System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(column.Table);
		String strNombreColumna=GetNombreColumnFromProperties(column);
		String strTablaEncontrada=GetNombreClaseRelacionadaFromColumn(column);
		
			
			foreach(TableSchema tableSchema in column.Table.Database.Tables)
			{
				
				if(strTablaEncontrada==GetNombreClaseC(tableSchema.ToString()))
				{
					strTablaEncontrada=GetNombreTableFromProperties(tableSchema);
					break;
				}
			}
		
			
		
		TableSchema tableSchemaEncontrada=GetTablaFromNombreC(strTablaEncontrada,column.Table);
		
		
		return tableSchemaEncontrada;
	}
	
	public static bool TieneColumnaTipoTexto(IndexSchema indexSchema)
	{
	bool tieneColumnaTexto=false;
	
		foreach(MemberColumnSchema memberColumnSchema in indexSchema.MemberColumns)
		{
			if(memberColumnSchema.DataType==DbType.AnsiString ||memberColumnSchema.DataType==DbType.AnsiStringFixedLength ||memberColumnSchema.DataType==DbType.String||memberColumnSchema.DataType==DbType.StringFixedLength)
			{
				tieneColumnaTexto=true;
			}
		}
								
	return tieneColumnaTexto;
	}
	
	public static string GetNombreColumnIdFromPropertiesAndTable(TableSchema table)
	{
	String nombreColumnIdTabla="idNone";
	
			foreach(ColumnSchema column in table.Columns)
			{	
				if(column.IsPrimaryKeyMember)
				{
					nombreColumnIdTabla=GetNombreColumnFromProperties(column);
					break;
				}
			}
								
	return nombreColumnIdTabla;
	}
	
	public  bool VerificarTablaRelacionFromPropertiesC(TableSchema tableRelacionada)
	{
	
		bool verificado=true;
		
		String[] descripciones;
		String[] tipo;
		String relacion;
		
		if(tableRelacionada.Description!="")
		{
			
			descripciones=tableRelacionada.Description.Split('|');
						
						foreach(String descripcion in descripciones)
						{
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("WEBRELACIONESNO"))
							{
								relacion=tipo[1];
																
								if(relacion=="false")
								{
									verificado=false;
									break;
								}
																
							}
						}
		}
		
		return verificado;
	}
	
	public bool GetExistTagTableFromPropertiesC(TableSchema table,String strTag)
	{
		
	bool blnExist=false;
	
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals(strTag))
						{
							blnExist=true;
							break;
						}
					}
	}
	
	return blnExist;
	}
	
	public static string GetNombreTableFromProperties(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NOMBRE"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public static bool GetConEjbServiceTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	bool blnConAuditoria=true;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NOEJB"))
						{
							nombreTabla=tipo[1];
							blnConAuditoria=false;
							break;
						}
					}
	}							
	return blnConAuditoria;
	}
	
	public static bool GetConAuditoriaTableFromProperties(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	bool blnConAuditoria=false;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("CONAUD"))
						{
							nombreTabla=tipo[1];
							blnConAuditoria=true;
							break;
						}
					}
	}							
	return blnConAuditoria;
	}
	
	public static bool GetConPersistenciaTableFromProperties(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	bool blnConPersistencia=true;
	
	if(table.Description!="")
	{
		//System.Windows.Forms.MessageBox.Show(blnConPersistencia.ToString());
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("CONPERSISTENCIA"))
						{	//System.Windows.Forms.MessageBox.Show(blnConPersistencia.ToString());
							nombreTabla=tipo[1];
							
							if(nombreTabla!="true") {
								blnConPersistencia=false;
								break;
							}
						}
					}
	}
	
	//System.Windows.Forms.MessageBox.Show(blnConPersistencia.ToString());
	return blnConPersistencia;
	}
	
	public static bool GetConStoreProceduresTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	bool blnConAuditoria=false;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("CONSTORE"))
						{
							nombreTabla=tipo[1];
							blnConAuditoria=true;
							break;
						}
					}
	}							
	return blnConAuditoria;
	}
	
	public static string GetAuditoriaCommentTableFromProperties(TableSchema table)
	{
		String strComment="";
		
		if(GetConAuditoriaTableFromProperties(table))						
		{
			strComment="//";
		}
	return strComment;
	}
	
	
	
	public static bool GetConJavaScriptIncludeTableFromProperties(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	bool blnConAuditoria=false;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("CONJAVASCRIPTIE"))
						{
							nombreTabla=tipo[1];
							blnConAuditoria=true;
							break;
						}
					}
	}							
	return blnConAuditoria;
	}
	
	public static bool GetConOriginalTableFromProperties(TableSchema table)
	{
	String nombreTabla=table.Name;
	String[] descripciones;
	String[] tipo;
	bool blnConAuditoria=false;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("CONORIG"))
						{
							nombreTabla=tipo[1];
							blnConAuditoria=true;
							break;
						}
					}
	}							
	return blnConAuditoria;
	}
	
	public static string GetOriginalCommentTableFromProperties(TableSchema table)
	{
		String strComment="";
		
		if(!GetConOriginalTableFromProperties(table))						
		{
			strComment="//";
		}
	return strComment;
	}
	
	public static string GetSchemaTableFromProperties(TableSchema table,String strSchema)
	{
	String nombreTabla=strSchema;
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("SCHEMA"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	public string GetNewCodeTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NEWCODE"))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}					
	
		if(blnEsTablaUnoAUnoFk || !TieneIdentityColumnC(table)) {
			nombreTabla="SinIdGenerated";
		}
	return nombreTabla;
	}
	
	public static string GetReporteGrupoTableFromPropertiesC(TableSchema table)
	{
	String nombreTabla="DEFAULT";
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals(strESREPORTEGROUP))
						{
							nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return nombreTabla;
	}
	
	
	public static bool TieneSchemaTableFromPropertiesC(TableSchema table,String strSchema)
	{
		bool blnTieneSchema=false;
	String nombreTabla=strSchema;
	String[] descripciones;
	String[] tipo;
	
	if(table.Description!="")
	{
		
		descripciones=table.Description.Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("SCHEMA"))
						{
							blnTieneSchema=true;
							//nombreTabla=tipo[1];
							break;
						}
					}
	}							
	return blnTieneSchema;//nombreTabla;
	}
	
	public static string GetNombreOrderColumnFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			foreach(ColumnSchema column in tableSchema.Columns)
			{
				
				
				foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("WEBORDEN"))
								{
									if(blExiste)
									{
										nombreColumna+=",";
									}
									else
									{
										blExiste=true;
									}
																		
									nombreColumna+=" "+GetNombreColumnFromProperties(column)+" "+tipo[1]+" ";
									break;
								}
							}
					}
							
				}
			}
		
					
		if(!blExiste)
		{
			return "";
		}
		
		return "\" order by "+nombreColumna+"\"+";
	}
	
	public static bool GetEsRompimientoFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("ESROMPIMIENTO"))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	public static bool GetEsInternoFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("ESINTERNO"))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	public static bool GetEsReporteFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("ESREPORTE"))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	public static bool GetEsAuxiliarFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
			 nombreColumna+="";
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("ESAUXILIAR"))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	
	
	public static bool GetEsReporteAuxiliarFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
			nombreColumna+="";
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals(strESREPORTEAUX))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	public static bool GetEsReporteParametroFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
			nombreColumna+="";
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals(strESREPORTEPARAM))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	public static bool GetEsReporteParametroFromPropertiesC(ColumnSchema columnSchema)
	{
		String nombreColumna="";
			nombreColumna+="";
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in columnSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals(strESREPORTEPARAM))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	
	public String GetEsPaqueteReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
		
		if(blnEsReporte) {
			strPaqueteReporte="."+strPackageReporte;
			
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetEsPaqueteImportReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
		
		if(blnEsReporte) {
			strPaqueteReporte=strPackageReporte+".";
			
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetEsImplementableAdditionableFromReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
		
		if(blnEsReporte) {
			strPaqueteReporte="Additionable";
			
		} else {
			strPaqueteReporte="Implementable";
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetAdditionalFromReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
		
		if(blnEsReporte) {
			strPaqueteReporte="ADDITIONAL";
			
		} else {
			strPaqueteReporte="";
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetEsImportPaqueteReporteC(TableSchema TablaBase,String strToComplete) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporteAuxiliar=GetEsReporteAuxiliarFromPropertiesC(TablaBase);
		
		if(blnEsReporteAuxiliar) {
			strPaqueteReporte="\r\n"+strToComplete+"."+strPackageReporte+".*;";
			
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetEsImportPaqueteReporteParaEsReporteC(TableSchema TablaBase,String strToComplete) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporteAuxiliar=GetEsReporteFromPropertiesC(TablaBase);
		
		if(blnEsReporteAuxiliar) {
			strPaqueteReporte="\r\n"+strToComplete+"."+strPackageReporte+".*;";
			
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetEsTablaAuxiliarReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;		
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
				
		if(blnEsReporte) {
			strPaqueteReporte=TablaBase.Owner;
			/*
			if(GetBuscarTablaAuxiliarReporteC(TablaBase)!=null) {
				strPaqueteReporte=GetNombreClaseC(GetBuscarTablaAuxiliarReporteC(TablaBase).ToString());
			}
			*/
		} else {
			strPaqueteReporte=GetNombreClaseC(TablaBase.ToString());
		}
		
		return strPaqueteReporte;				
	}
	
	public String GetEsTablaObjetoAuxiliarReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
		
		if(blnEsReporte) {
			strPaqueteReporte=GetNombreClaseC(GetBuscarTablaAuxiliarReporteC(TablaBase).ToString()).ToLower();
			
		} else {
			strPaqueteReporte=GetNombreClaseC(TablaBase.ToString()).ToLower();
		}
		
		return strPaqueteReporte;				
	}
	
	public TableSchema GetBuscarTablaAuxiliarReporteC(TableSchema TablaBase) {
		String strPaqueteReporte=string.Empty;
		bool blnEsReporte=GetEsReporteFromPropertiesC(TablaBase);
		String strNombreGrupo=GetReporteGrupoTableFromPropertiesC(TablaBase);
		TableSchema TablaBaseReporteAuxiliar=null;
		/*
		if(blnEsReporte) {
			strPaqueteReporte="."+strPackageReporte;
			
		} else {
			strPaqueteReporte=GetNombreClaseC(TablaBase.ToString());
		}
		*/
		
		for (int i = 0; i < TablaBase.Database.Tables.Count; i++){ 
			if(GetEsReporteAuxiliarFromPropertiesC(TablaBase.Database.Tables[i])) {
				if(GetReporteGrupoTableFromPropertiesC(TablaBase.Database.Tables[i]).Equals(strNombreGrupo)) {
					TablaBaseReporteAuxiliar=TablaBase.Database.Tables[i];
					break;
				}
			}
		
		} 
	
		return TablaBaseReporteAuxiliar;				
	}
	
	public static bool GetEsNativeFromPropertiesC(TableSchema tableSchema)
	{
		return true;
		String nombreColumna="";
			nombreColumna+="";
		bool blExiste=false;

		if(!blnConFuncionesSqlNativas) {//ESTA VARIABLE SE PONE TRUE CUANDO SE INICIALIZAN LAS VARIABLES EN LA PRIMERA PANTALLA
			
			String[] descripciones;
			String[] tipo;
			
					foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
					{
						if(extendedProperty.Name=="CS_Description")
						{
						descripciones=((String)extendedProperty.Value).Split('|');
								
								foreach(String descripcion in descripciones)
								{
									tipo=descripcion.Split('=');
									
									if(tipo[0].Equals(strCONNATIVE))
									{									
										blExiste=true;
										break;
									}
								}
						}
								
					}		
		} else {
			blExiste=true;
		}
					
		
		
		return blExiste;
	}
	
	public static bool GetEsMenuFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("ESMENU"))
								{
									
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blExiste;
	}
	
	public static bool GetPermiteInsertarFromPropertiesC(TableSchema tableSchema)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blPermite=true;
		
			
				
				
				foreach(ExtendedProperty extendedProperty in tableSchema.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("INSERTNO"))
								{
									
									blPermite=false;
									break;
								}
							}
					}
							
				}
			
		
					
		
		
		return blPermite;
	}
	
	public static bool GetBusquedaForeignKeyColumnFromPropertiesC(ColumnSchema column)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
				
				
				foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("CONBUSQUEDA"))
								{
									blExiste=true;
									
									break;
								}
							}
					}
							
				}
			
		
					
		return blExiste;

	}
	
	public static bool GetConRangoBusquedasColumnFromPropertiesC(ColumnSchema column)
	{
		String nombreColumna="";
			nombreColumna+="";		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
				foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
						descripciones=((String)extendedProperty.Value).Split('|');
						
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("CONRANGOINDICES"))
								{
									blExiste=true;
									
									break;
								}
								
							}
					}							
				}
						
		return blExiste;
	}
	
	public static bool GetConRangoBusquedasIndicesColumnFromPropertiesC(ColumnSchema column,IndexSchema indexSchema)
	{
		String nombreColumna="";
			nombreColumna+="";		
		String[] descripciones=null;
			descripciones=null;
		String[] tipo=null;
			tipo=null;
		bool blExiste=GetConRangoBusquedasIndicesColumnFromPropertiesC(column);
						
		return blExiste;
	}
	
	public static bool GetConRangoBusquedasIndicesColumnFromPropertiesC(ColumnSchema column,String  strIndexSchemaName)
	{
		String nombreColumna="";
			nombreColumna+="";
				
		String[] descripciones;
			descripciones=null;
		String[] tipo;
			tipo=null;
		bool blExiste=GetConRangoBusquedasIndicesColumnFromPropertiesC(column);
		
				
						
		return blExiste;
	}
	
	public static bool GetConRangoBusquedasIndicesColumnFromPropertiesC(ColumnSchema column)
	{
		String nombreColumna="";
			nombreColumna+="";
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
				foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
						descripciones=((String)extendedProperty.Value).Split('|');
						
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("CONRANGOINDICE"))
								{
									blExiste=true;
									
									break;
								}
								
							}
					}							
				}
						
		return blExiste;
	}
	
	public static bool GetEsReporteColumnFromPropertiesC(ColumnSchema column)
	{
		String nombreColumna="";
		
		
		String[] descripciones;
		String[] tipo;
		bool blExiste=false;
		
				
				
				foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
				{
					if(extendedProperty.Name=="CS_Description")
					{
					descripciones=((String)extendedProperty.Value).Split('|');
							
							foreach(String descripcion in descripciones)
							{
								tipo=descripcion.Split('=');
								
								if(tipo[0].Equals("ESREPORTE"))
								{
									blExiste=true;
									break;
								}
							}
					}
							
				}
			
		
					
		return blExiste;

	}
	
	public static string GetNombreColumnFromProperties0(ColumnSchema column)
	{
	String nombreColumna=column.Name;
	String[] descripciones;
	String[] tipo;
	
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
			descripciones=((String)extendedProperty.Value).Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NOMBRE"))
						{
							nombreColumna=tipo[1];
							break;
						}
					}
			}
					
		}
				
	return nombreColumna;
	}
	
	public static string GetNombreColumnFromProperties(ColumnSchema column)	{
		return GetNombreColumnFromPropertiesInterno(column,false);
	}
	
	public static string GetNombreColumnFromPropertiesC(ColumnSchema column,bool blnParaSql)	{
		return GetNombreColumnFromPropertiesInterno(column,blnParaSql);
	}
	
	public static string GetNombreColumnFromPropertiesInterno(ColumnSchema column,bool blnParaSql)
	{
	String nombreColumna="";
	
	if(column==null) {
		return nombreColumna;
	}
	
		if(blnEsLowerCaseDBNames) {
			nombreColumna=GetNombreConSeparacionC(column.Name).ToLower();
		} else {	
			if(blnEsMixedCaseDBNames) {
				nombreColumna=column.Name;
			} else {
				nombreColumna=GetNombreConSeparacionC(column.Name).ToUpper();
			}
		}
	
	
	String[] descripciones;
	String[] tipo;
	
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
			descripciones=((String)extendedProperty.Value).Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("NOMBRE"))
						{
							nombreColumna=tipo[1];
							
							if(blnEsLowerCaseDBNames) {
								nombreColumna=nombreColumna.ToLower();
							} else {	
								if(blnEsMixedCaseDBNames) {
									nombreColumna=nombreColumna;
								} else {
									nombreColumna=nombreColumna.ToUpper();
								}
							}
							break;
						}
					}
			}
					
		}
	
		if(column.Name.Equals(strId)&&column.IsPrimaryKeyMember&&nombreColumna.Equals(strIdDB)) {
			//Trace.WriteLine(column.Table.Name+"-"+nombreColumna+"="+strIdDB);
			if(!blnParaSql) {
				nombreColumna="ConstantesSql.ID";
			} else {
				nombreColumna=strIdDB;
			}
		}
		
		if(column.Name.Equals(strVersionRow)/* && nombreColumna.Equals(strVersionRowDB)*/) {
			//Trace.WriteLine(column.Table.Name+"-"+nombreColumna+"="+strVersionRowDB);						
			if(!blnParaSql) {
				nombreColumna="ConstantesSql.VERSIONROW";
			} else {
				nombreColumna=strVersionRowDB;
			}
		}
		
		return nombreColumna;
	}
	
	public  String GetDescripcionComboColumnFromProperties(TableSchema tableSchema)
	{
	
		
	string strPrefijoTabla="";
	string strPrefijoTipo =""; 
	string strNombre = "";
	
	String strColumnaDetalle="id";
	String[] descripciones;
	String[] tipo;
	
	foreach(ColumnSchema columnSchema in tableSchema.Columns)
	{
		if(columnSchema.Name==strId)
		{
			strColumnaDetalle=".get"+strIdGetSet+"().toString()";
		}
		
		foreach(ExtendedProperty extendedProperty in columnSchema.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
			descripciones=((String)extendedProperty.Value).Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WEBCOMBO"))
						{
							//if(tipo[1]=="true")
							//{
								strColumnaDetalle=".get"+GetNombreCompletoColumnaClaseC(columnSchema)+"()"+GetTipoColumnaToString(columnSchema);
							//}
							
							break;
						}
					}
			}
					
		}
	}
				
	return strColumnaDetalle;
	}
	
	public  ColumnSchema GetColumnSchemaComboColumnFromProperties(TableSchema tableSchema)
	{
	
	ColumnSchema columnSchemaDetalleCombo=null;
	
	string strPrefijoTabla="";
	string strPrefijoTipo =""; 
	string strNombre = "";
	
	String strColumnaDetalle="id";
	String[] descripciones;
	String[] tipo;
	
	foreach(ColumnSchema columnSchema in tableSchema.Columns)
	{
		if(columnSchema.Name==strId)
		{
			columnSchemaDetalleCombo=columnSchema;
		}
		
		foreach(ExtendedProperty extendedProperty in columnSchema.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
			descripciones=((String)extendedProperty.Value).Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WEBCOMBO"))
						{
							columnSchemaDetalleCombo=columnSchema;
							
							break;
						}
					}
			}
					
		}
	}
				
	return  columnSchemaDetalleCombo;
	}
	
	public static string GetWebNombreTituloColumnFromPropertiesC(ColumnSchema column)
	{
		String nombreColumna=GetNombreColumnaClaseC(column);
		String[] descripciones;
		String[] tipo;
		
		
			foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
			{
				
				
				if(extendedProperty.Name=="CS_Description")
				{
										
					descripciones=((String)extendedProperty.Value).Split('|');
										
						foreach(String descripcion in descripciones)
						{
							
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("WEBTITULO"))
							{								
								nombreColumna=tipo[1];
								break;
							}
						}
				}
						
			}
				
		return nombreColumna;
	}
	
	public static string GetJavaScriptValidacionColumnFromPropertiesC(ColumnSchema column)
	{
		String nombreColumna="";
		String[] descripciones;
		String[] tipo;
		
		
			foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
			{
				
				
				if(extendedProperty.Name=="CS_Description")
				{
										
					descripciones=((String)extendedProperty.Value).Split('|');
										
						foreach(String descripcion in descripciones)
						{
							
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("JSVALIDACION"))
							{								
								nombreColumna=tipo[1];
								break;
							}
						}
				}
						
			}
				
		return nombreColumna;
	}
	
	public static bool GetNoUpperColumnFromPropertiesC(ColumnSchema column)
	{
		bool blnNoUpper=false;
		
		String nombreColumna=GetNombreColumnaClaseC(column);
		String[] descripciones;
		String[] tipo;
		
		
			foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
			{
				
				
				if(extendedProperty.Name=="CS_Description")
				{
										
					descripciones=((String)extendedProperty.Value).Split('|');
										
						foreach(String descripcion in descripciones)
						{
							
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("NOUPPER"))
							{								
								nombreColumna=tipo[1];
								 blnNoUpper=true;
								break;
							}
						}
				}
						
			}
				
		return blnNoUpper;
	}
	
	public static bool GetNoEditColumnFromPropertiesC(ColumnSchema column)
	{
		bool blnNoEdit=false;
		
		String nombreColumna=GetNombreColumnaClaseC(column);
		String[] descripciones;
		String[] tipo;
		
		
			foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
			{
				
				
				if(extendedProperty.Name=="CS_Description")
				{
										
					descripciones=((String)extendedProperty.Value).Split('|');
										
						foreach(String descripcion in descripciones)
						{
							
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("EDITNO"))
							{								
								nombreColumna=tipo[1];
								 blnNoEdit=true;
								break;
							}
						}
				}
						
			}
				
		return blnNoEdit;
	}
	
	public static bool GetEsNullColumnFromPropertiesC(ColumnSchema column)
	{
		bool blnNoEdit=false;
		
		String nombreColumna=GetNombreColumnaClaseC(column);
		String[] descripciones;
		String[] tipo;
		
		
			foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
			{
				
				
				if(extendedProperty.Name=="CS_Description")
				{
										
					descripciones=((String)extendedProperty.Value).Split('|');
										
						foreach(String descripcion in descripciones)
						{
							
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("ESNULL"))
							{								
								nombreColumna=tipo[1];
								 blnNoEdit=true;
								break;
							}
						}
				}
						
			}
				
		return blnNoEdit;
	}
	
	public static int GetWebNumeroFilasColumnFromPropertiesC(ColumnSchema column)
	{
	int numeroFilasRows=column.Size/50;
	String[] descripciones;
	String[] tipo;
	
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
			descripciones=((String)extendedProperty.Value).Split('|');
					
					foreach(String descripcion in descripciones)
					{
						tipo=descripcion.Split('=');
						
						if(tipo[0].Equals("WEBFILAS"))
						{
							if(int.TryParse(tipo[1],out numeroFilasRows))
							{
								numeroFilasRows=int.Parse(tipo[1]);
							}
							
							break;
						}
					}
			}
					
		}
				
	return numeroFilasRows;
	}
	
	public static string GetFunctionValidationControlHtmlBusqueda(ColumnSchema column)
	{
		
	String strName="";
	String strLetNull="";
	
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired)|| column.Name.Equals(strId)|| column.Name.Equals(strVersionRow))
	{
			return string.Empty;
	}
		
	
	strName=column.Name;
	
		if(!column.IsForeignKeyMember)
		{
		
			if(column.AllowDBNull)
			{
			strLetNull="\"s\"";
			}
			else
			{
			strLetNull="\"s\"";
			}
			
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
				
			strName="validacion.Check_text(this,"+column.Size+",\"n\",\"n\",\"s\",\"s\",\"s\","+strLetNull+")";
							
			}
			
			else if(column.DataType==DbType.Boolean)
			{
				return string.Empty;
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double)
			{
				strName="validacion.Check_num(this,0,1000000,\"s\","+strLetNull+")";
				
			}
			else if(column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
			strName="validacion.Check_num(this,0,1000000,\"n\","+strLetNull+")";	
			}
			else if(column.DataType==DbType.Int16||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
			strName="check_num(this,0,1000,\"n\",\"s\")";	
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strName="validacion.Check_dateDate(this,"+strLetNull+")";
			
			}
		}
		else
		{

		return string.Empty;
		}
	
	
	return strName;
}
	
	public static string GetOnBlurEventValidationControlHtmlBusqueda(ColumnSchema column,String strIndexName)
	{
	/*String strFunctionMensaje="createSimpleYahooDialogErrorValidacion('Validacion campo: "+column.Name+ "',";
	
	String strName=" onBlur=\""+strFunctionMensaje +GetFunctionValidationControlHtmlBusqueda(column)+",this)\"";
	*/
	String strName=" onBlur=\""+GetNombreClaseObjetoC(column.Table.ToString())+"FuncionGeneral.ValidarFormulario"+GetNombreClaseC(column.Table.ToString())+strIndexName+"()\"";
	return strName;
	}	

	public static string GetOnBlurEventValidationControlHtml(ColumnSchema column)
	{
	/*
	String strFunctionMensaje="createSimpleYahooDialogErrorValidacion('Validacion campo: "+column.Name+ "',";
	
	String strName=" onBlur=\""+strFunctionMensaje +GetFunctionValidationControlHtml(column)+",this)\"";
	
	*/
	String strName=" onBlur=\""+GetNombreClaseObjetoC(column.Table.ToString())+"FuncionGeneral.Validar"+GetNombreClaseC(column.Table.ToString())+GetNombreColumnaClaseC(column)+"()\"";

	return strName;
	}
	
	public static string GetFunctionValidationControlHtml(ColumnSchema column)
	{
		
	String strName="";
	String strLetNull="";
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired)|| column.Name.Equals(strId)|| column.Name.Equals(strVersionRow))
	{
			return string.Empty;
	}
		
	
	strName=column.Name;
	
		if(!column.IsForeignKeyMember)
		{
			if(GetJavaScriptValidacionColumnFromPropertiesC(column)!="")
			{
				strName=strValidacion+GetJavaScriptValidacionColumnFromPropertiesC(column);
						
				return strName;
			}
			
			
			if(column.AllowDBNull)
			{
			strLetNull="\"n\"";
			}
			else
			{
			strLetNull="\"s\"";
			}
			
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
				
			strName="validacion.Check_text(this,"+column.Size+",\"n\",\"n\",\"s\",\"s\",\"s\","+strLetNull+")";
							
			}
			
			else if(column.DataType==DbType.Boolean)
			{
				return "\"\"";
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double)
			{
				strName="validacion.Check_num(this,0,1000000,\"s\","+strLetNull+")";
				
			}
			else if(column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
			strName="validacion.Check_num(this,0,1000000,\"n\","+strLetNull+")";	
			}
			else if(column.DataType==DbType.Int16||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
			strName="validacion.Check_num(this,0,1000,\"n\","+strLetNull+")";	
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strName="validacion.Check_dateDate(this,"+strLetNull+")";
			
			}
		}
		else
		{

		return string.Empty;
		}
	
	
	return strName;
}
	
	public String GetParameterSelectionC(ColumnSchema column,bool esUltimo,bool esUnico,bool esCompuesto,bool esNative,bool esRanges,bool esRangesFinal) 
		{
			String strParaBusquedaString=""; 
			String strParaBusquedaString0=""; 
			String strParaBusquedaCompuesto=""; 
			String strEqualsLike="Equal";
			
			String strSufijoRangesFinal="";
			String strSufijoRangesFinalName="";
						
			//SETEO LOS PARAMETROS DEACUERDO SI ES RANGO
			
			if(esRanges) {
				if(esRangesFinal) {
					esUltimo=true;
					strEqualsLike="MenorIgual";
					strSufijoRangesFinal=strSufijoRangoFinal;
					strSufijoRangesFinalName=",\""+strSufijoRangoFinal+"\"";	
				} else {
					esUltimo=false;
					strEqualsLike="MayorIgual";
				}
			}
						
			/*
			if(GetConRangoBusquedasColumnFromPropertiesC(column)||GetConRangoBusquedasIndicesColumnFromPropertiesC(column,indexSchema)) {
				if(!esRangesFinalFinal) {
					esRangesFinalInicial=true;
				}
			}
			*/
			
			/*
			if(esRangesFinal) {
				strSufijoRangesFinal=strSufijoRangoFinal;	
			}
			*/
			
			if(esCompuesto)
			{
				strParaBusquedaCompuesto="Constantes.SCHEMA+\".\"+"+GetNombreClaseC(column.Table.ToString())+"DataAccess.TABLENAME+\".\"+";
										  
			}
			
			if(/*column.DataType==DbType.DateTime ||*/column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
				if(!esUnico)	
				{
					strEqualsLike="Like";
					strParaBusquedaString="+\"%\"";
					strParaBusquedaString0="\"%\"+";
				}
			}
			
			String strConNative="";
			
			if(esNative) {
				strConNative=strNative;
			}
			
			String strColumnName="";
			
			if(column.Name!=strId) {
				strColumnName=column.Name;
			} else {
				strColumnName=strIdGetSet;
			}
			
			bool blnEsPKCompuestoTabla=EsPKCompuestoTabla(column.Table);
			String  strPalabraclaveEsPKCompuestoTabla="";
			
			if(blnEsPKCompuestoTabla&&column.IsPrimaryKeyMember) {
				strPalabraclaveEsPKCompuestoTabla="Constantes.IDCOMPOSEKEY+";
			}
			
			String strParameterSelection=String.Empty;
			strParameterSelection="\r\n\r\n\t\t\tParameterSelectionGeneral parameterSelectionGeneral"+column.Name+strSufijoRangesFinal+"= new ParameterSelectionGeneral();";
			strParameterSelection+="\r\n\t\t\tparameterSelectionGeneral"+column.Name+strSufijoRangesFinal+".setParameterSelectionGeneral"+strEqualsLike+"(ParameterType."+GetTipoColumnaClaseEnumC(column)+","+strParaBusquedaString0+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strSufijoRangesFinal+ /*GetTipoColumnaToString(column)+*/strParaBusquedaString +","+strParaBusquedaCompuesto+strPalabraclaveEsPKCompuestoTabla+GetNombreClaseC(column.Table.ToString())+"DataAccess.getColumnName"+strConNative+strColumnName+"()"+strSufijoRangesFinalName+",";
			
			if(esUltimo) {
			strParameterSelection+="ParameterTypeOperator.NONE);";		
			}
			else
			{
			strParameterSelection+=	"ParameterTypeOperator.AND);";	
			}
			
			strParameterSelection+="\r\n\t\t\tqueryWhereSelectParameters.addParameter(parameterSelectionGeneral"+column.Name+strSufijoRangesFinal+");";
		
			return strParameterSelection;
		}
		
	public string GetWebMethodAnnotationFromConWebServicesC(bool ConWebServices)
{	
	String strWebMethodAnnotationFromConWebServices="";
	
	if(ConWebServices) {
		strWebMethodAnnotationFromConWebServices="@WebMethod";
	
	}
	return strWebMethodAnnotationFromConWebServices;
}

	public static string GetNameControlHtml(ColumnSchema column)
	{
	
	
	String strName="";
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
	{
			return string.Empty;
	}
		
	if(column.Name.Equals(strId))
	{
		strName="hdnIdActual";
		
	}
	else if(column.Name.Equals(strVersionRow))
	{
		strName="hdn"+column.Name;
		
	}
	else
	{
		if(!column.IsForeignKeyMember)
		{
		
		
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
				
			strName="txt"+column.Name;
							
			}
			
			else if(column.DataType==DbType.Boolean)
			{
				strName="chb"+column.Name;
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
				strName="txt"+column.Name;
				
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strName="dddp"+GetNombreClaseC(column.Table.ToString())+column.Name;
			
			}
		}
		else
		{

		strName="djcmb"+GetNombreClaseC(column.Table.ToString())+column.Name;
		}
	}
	
	return strName;
}

	public static string GetNameControlHtmlBusqueda(ColumnSchema column,String strIndexName)
	{
	
	String strName="";
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
	{
			return string.Empty;
	}
		
	if(column.Name.Equals(strId))
	{
		strName="hdnIdActual"+ strIndexName;
		
	}
	else if(column.Name.Equals(strVersionRow))
	{
		strName="hdn"+column.Name+ strIndexName;
		
	}
	else
	{
		if(!column.IsForeignKeyMember)
		{
		
		
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
				
			strName="txt"+column.Name+strIndexName;
							
			}
			
			else if(column.DataType==DbType.Boolean)
			{
				strName="chb"+column.Name+strIndexName;
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
				strName="txt"+column.Name+strIndexName;
				
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strName="dddp"+GetNombreClaseC(column.Table.ToString())+column.Name+strIndexName;
			
			}
		}
		else
		{

		strName="djcmb"+GetNombreClaseC(column.Table.ToString())+column.Name+strIndexName;
		}
	}
	
	return strName;
}

	public string GetControlHtmlBusqueda(ColumnSchema column,String strIndexName,bool ConFaces)
	{
	
	String strTipo=GetTipoColumnaClaseC(column);	
	String strPrefijo=" "+GetPrefijoTipoC(column);	
	String strColumna=GetNombreColumnaClaseC(column);
	
	String strControl="";
	String strType="";
	String strName="";
	
	String strObjectFace="";
	String strValueFace="";
	
	if(ConFaces)
	{
		strObjectFace=GetNombreClaseObjetoC(column.Table.ToString())+strPrefijoJSFFaces;
	}
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
	{
			return string.Empty;
	}
		
	if(column.Name.Equals(strId))
	{
		strType="\"hidden\"";
		strName=" name=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
		
		if(!ConFaces)
		{
			strControl="<input type="+strType+strName+">";
		}
		else
		{
			strControl="<h:inputHidden "+strName.Replace("name=","id=")+" />";
		}
	}
	if(column.Name.Equals(strVersionRow))
	{
		strType="\"hidden\"";
		strName=" name=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
		
		
		if(!ConFaces)
		{
			strControl="<input type="+strType+strName+">";
		}
		else
		{
			strControl="<h:inputHidden "+strName.Replace("name=","id=")+" />";
		}
	}
	else
	{
		if(!column.IsForeignKeyMember)
		{
		
		
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
			String strMaxLength="";
			String strRows="";
			int numRows=0;
			int numCols=0;
			
			strName=" name=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
			
				if(column.Size<51)
				{
					strType="\"text\"";
					strMaxLength=" maxlength=\""+column.Size.ToString()+"\"";
					
					if(!ConFaces)
					{
						strControl="<input type="+strType+strMaxLength+strName+GetOnBlurEventValidationControlHtmlBusqueda(column,strIndexName)+">";					
					}
					else
					{
						strValueFace=" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strIndexName+"}\" ";
						
						strControl="<h:inputText "+strName.Replace("name=","id=")+strValueFace+strMaxLength+" />";
					}
				}
				else if(column.Size<200)
				{
					numRows=column.Size/30;
					numCols=30;
					
					strType="<textarea";
					strRows=" rows=\""+numRows.ToString()+"\"";
					strMaxLength=" cols=\""+numCols.ToString()+"\"";
					
					
					if(!ConFaces)
					{
						strControl=strType+strName+strMaxLength+strRows+GetOnBlurEventValidationControlHtmlBusqueda(column,strIndexName)+"></textarea>";
					}
					else
					{
						strValueFace=" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strIndexName+"}\" ";
						
						strControl="<h:inputTextarea "+strName.Replace("name=","id=")+strValueFace+strRows+strMaxLength+" />";
					}
				}
				else
				{
					numRows=GetWebNumeroFilasColumnFromPropertiesC(column);
					numCols=50;
					
					strType="<textarea";
					strRows=" rows=\""+numRows.ToString()+"\"";
					strMaxLength=" cols=\""+numCols.ToString()+"\"";
					
					
					if(!ConFaces)
					{
						strControl=strType+strName+strMaxLength+strRows+GetOnBlurEventValidationControlHtmlBusqueda(column,strIndexName)+"></textarea>";
					}
					else
					{
						strValueFace=" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strIndexName+"}\" ";
						
						strControl="<h:inputTextarea "+strName.Replace("name=","id=")+strValueFace+strRows+strMaxLength+" />";
					}
				}
			}
			
			else if(column.DataType==DbType.Boolean)
			{
				strType="\"checkbox\"";
				strName=" name=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
				
				
				if(!ConFaces)
				{
					strControl="<input type="+strType+strName+">";
				}
				else
				{
					strValueFace=" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strIndexName+"}\" ";
						
					strControl="<h:selectBooleanCheckbox "+strName.Replace("name=","id=")+strValueFace+" />";
				}
				
	
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
				strType="\"text\"";
				strName=" name=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
				String strMaxLength=" maxlength=\""+column.Size.ToString()+"\"";
				
				
				if(!ConFaces)
				{
					strControl="<input type="+strType+strMaxLength+strName+GetOnBlurEventValidationControlHtmlBusqueda(column,strIndexName)+">";
				}
				else
				{
					strValueFace=" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strIndexName+"}\" ";
					
					strControl="<h:inputText "+strName.Replace("name=","id=")+strValueFace+strMaxLength+" />";
				}
	
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strType="\"dojo.dropdowndatepicker\"";
				strName=" id=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
				strControl="<a:widget  name="+strType+strName+"/>";
	
			}
		}
		else
		{

			strName=" id=\""+GetNameControlHtmlBusqueda(column,strIndexName)+"\"";
			
			
			
			if(!ConFaces)
			{
				strControl="<a:widget"+strName+"name=\"dojo.combobox\""+"/>";
			}
			else
			{
				strControl="<h:selectOneMenu "+strName.Replace("name=","id=")+" value=\"#{"+strObjectFace+"."+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+strIndexName+"}\">";				
				strControl+="<f:selectItems value=\"#{"+strObjectFace+"."+GetNombreClaseRelacionadaFromColumn(column).ToLower() +"s"+strForeignKey+"ListSelectItem}\"/>\r\n";
				strControl+="</h:selectOneMenu>";
			}
	
		}
	}
	
	return strControl;
}

public static string GetControlSwingC(ColumnSchema column)
	{
	
	String strTipo=GetTipoColumnaClaseC(column);	
	String strPrefijo=" "+GetPrefijoTipoC(column);	
	String strColumna=GetNombreColumnaClaseC(column);
	
	String strControl="";
	String strType="";
	String strName="";
	
	String strObjectFace="";
	String strValueFace="";
	String strPrefijoCampo="";
	
	
	strObjectFace=GetNombreClaseObjetoC(column.Table.ToString())+strPrefijoJSFFaces;
	strPrefijoCampo=GetPrefijoTablaC().ToLower();
	
	string strFieldColumnaclase=GetPrefijoTablaC().ToLower()+GetPrefijoTipoC(column)+strColumna;
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
	{
			return string.Empty;
	}
		
	if(column.Name.Equals(strId))
	{
		strControl="jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+"= new JLabel();\r\n\r\n";
		
		strControl+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
        strControl+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        strControl+="\t\tjLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";
		
		strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strId+"}\"), jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
		strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
		strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
		strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jLabel"+strIdGetSet+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
		strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											

		
		
	}
	if(column.Name.Equals(strVersionRow))
	{
				
		strValueFace=" value=\"#{"+strObjectFace+"."+strVersionRow+"}\" ";			
		
		strControl="jLabel"+strVersionRowGetSet+GetNombreClaseC(column.Table.ToString())+"= new JLabel();\r\n\r\n";
		
		strControl+="\t\tjLabel"+strVersionRowGetSet+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
        strControl+="\t\tjLabel"+strVersionRowGetSet+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        strControl+="\t\tjLabel"+strVersionRowGetSet+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";

		strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strVersionRow+"}\"), jLabel"+strVersionRowGetSet+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
		strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
		strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
		strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jLabel"+strVersionRowGetSet+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
		strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	
	}
	else
	{
		if(!column.IsForeignKeyMember)
		{
		
		
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
			String strMaxLength="";
			String strRows="";
			int numRows=0;
			int numCols=0;
			
			strName=" name=\""+GetNameControlHtml(column)+"\"";
			
				if(column.Size<51)
				{
					strMaxLength=" maxlength=\""+column.Size.ToString()+"\"";					
					
					strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";						
					
					strControl="jTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JTextField();\r\n\r\n";
					
					strControl+="\t\tjTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
       				strControl+="\t\tjTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        			strControl+="\t\tjTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";
					
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strFieldColumnaclase+"}\"), jTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
					strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
					strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	

				}
				else if(column.Size<200)
				{
					numRows=column.Size/15;
					numCols=30;
					
					strRows=" rows=\""+numRows.ToString()+"\"";
					strMaxLength=" cols=\""+numCols.ToString()+"\"";
					
					strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";						
				
					strControl="jTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JTextArea();\r\n\r\n";
					
					strControl+="\t\tjTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
       				strControl+="\t\tjTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        			strControl+="\t\tjTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";

					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strFieldColumnaclase+"}\"), jTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
					strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
					strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	
				}
				else
				{
					numRows=GetWebNumeroFilasColumnFromPropertiesC(column);
					numCols=50;
					
					strRows=" rows=\""+numRows.ToString()+"\"";
					strMaxLength=" cols=\""+numCols.ToString()+"\"";
					
					
					strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";						
					
					strControl="jTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JTextArea();\r\n\r\n";
					
					strControl+="\t\tjTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
       				strControl+="\t\tjTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        			strControl+="\t\tjTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";
					
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strFieldColumnaclase+"}\"), jTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
					strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
					strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jTextArea"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
					strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	
				}
			}
			
			else if(column.DataType==DbType.Boolean)
			{
									
				strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";					
				
				strControl="jCheckBox"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JCheckBox();\r\n\r\n";
				
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strFieldColumnaclase+"}\"), jCheckBox"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"selected\"));\r\n";	
				//strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
				strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jCheckBox"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
				strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	
	
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
				String strMaxLength=" maxlength=\""+column.Size.ToString()+"\"";				
				
				strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";					
				
				strControl="jTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JTextField();\r\n";
				
				strControl+="\t\tjTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
       			strControl+="\t\tjTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        		strControl+="\t\tjTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";

				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strFieldColumnaclase+"}\"), jTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
				strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
				strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	
	
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strControl="jFormattedTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JFormattedTextField();\r\n";
				
				strControl+="\t\tjFormattedTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMinimumSize(new Dimension(100,20));\r\n";
       			strControl+="\t\tjFormattedTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setMaximumSize(new Dimension(100,20));\r\n";
        		strControl+="\t\tjFormattedTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+".setPreferredSize(new Dimension(100,20));\r\n\r\n";
				
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ_WRITE, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement."+strFieldColumnaclase+"}\"), jFormattedTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"text\"));\r\n";	
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+".setSourceUnreadableValue(null);\r\n";	
				strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";	
				strControl+="\t\tbinding"+GetNombreClaseC(column.Table.ToString())+" = org.jdesktop.beansbinding.Bindings.createAutoBinding(org.jdesktop.beansbinding.AutoBinding.UpdateStrategy.READ, jTableDatos"+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.ELProperty.create(\"${selectedElement != null}\"), jFormattedTextField"+strColumna+GetNombreClaseC(column.Table.ToString())+", org.jdesktop.beansbinding.BeanProperty.create(\"enabled\"));\r\n";	
				strControl+="\t\tbindingGroup"+GetNombreClaseC(column.Table.ToString())+".addBinding(binding"+GetNombreClaseC(column.Table.ToString())+");\r\n";											
	
			}
		}
		else
		{

				
			//strControl="<h:selectOneMenu "+strName.Replace("name=","id=")+" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" >\r\n";
			//strControl+="\t\t\t\t\t\t\t\t<f:selectItems value=\"#{"+strObjectFace+"."+GetNombreClaseRelacionadaFromColumn(column).ToLower() +"s"+strForeignKey+"ListSelectItem}\"/>\r\n";		
			//strControl+="\t\t\t\t\t\t\t</h:selectOneMenu>";
			
			strControl="jComboBox"+strColumna+GetNombreClaseC(column.Table.ToString())+"= new JComboBox();\r\n";
			
		}
	}
	
	return strControl;
}

	public string GetControlHtml(ColumnSchema column,bool ConFaces)
	{
	
	String strTipo=GetTipoColumnaClaseC(column);	
	String strPrefijo=" "+GetPrefijoTipoC(column);	
	String strColumna=GetNombreColumnaClaseC(column);
	
	String strControl="";
	String strType="";
	String strName="";
	
	String strObjectFace="";
	String strValueFace="";
	String strPrefijoCampo="";
	
	if(ConFaces)
	{
		strObjectFace=GetNombreClaseObjetoC(column.Table.ToString())+strPrefijoJSFFaces+"."+GetNombreClaseObjetoC(column.Table.ToString());
		strPrefijoCampo=GetPrefijoTablaC().ToLower();
	}
	
	if(column.Name.Equals(strIsActive)|| column.Name.Equals(strIsExpired))
	{
			return string.Empty;
	}
		
	if(column.Name.Equals(strId))
	{
		strType="\"hidden\"";
		strName=" name=\""+GetNameControlHtml(column)+"\"";
		
		if(!ConFaces)
		{
			strControl="<input type="+strType+strName+">";
		}
		else
		{
			strControl="<h:inputHidden "+strName.Replace("name=","id=")+" />";
		}
		
	}
	if(column.Name.Equals(strVersionRow))
	{
		strType="\"hidden\"";
		strName=" name=\""+GetNameControlHtml(column)+"\"";
				
		if(!ConFaces)
		{
			strControl="<input type="+strType+strName+">";
		}
		else
		{
			strValueFace=" value=\"#{"+strObjectFace+"."+strVersionRow+"}\" ";
			
			strControl="<h:inputHidden "+strName.Replace("name=","id=")+strValueFace+" />";
		}
	}
	else
	{
		if(!column.IsForeignKeyMember)
		{
		
		
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
			String strMaxLength="";
			String strRows="";
			int numRows=0;
			int numCols=0;
			
			strName=" name=\""+GetNameControlHtml(column)+"\"";
			
				if(column.Size<30)
				{
					strType="\"text\"";
					strMaxLength=" maxlength=\""+column.Size.ToString()+"\"";					
					
					if(!ConFaces)
					{
						strControl="<input type="+strType+strMaxLength+strName+GetOnBlurEventValidationControlHtml(column)+">";			
					}
					else
					{
						strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";
						
						strControl="<h:inputText "+strName.Replace("name=","id=")+strValueFace+strMaxLength+" />";
					}
				}
				else if(column.Size<200)
				{
					numRows=column.Size/15;
					numCols=30;
					
					strType="<textarea";
					strRows=" rows=\""+numRows.ToString()+"\"";
					strMaxLength=" cols=\""+numCols.ToString()+"\"";
					
					if(!ConFaces)
					{
						strControl=strType+strName+strMaxLength+strRows+GetOnBlurEventValidationControlHtml(column)+"></textarea>";
					
					}
					else
					{
						strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";
						
						strControl="<h:inputTextarea "+strName.Replace("name=","id=")+strValueFace+strRows+strMaxLength+" />";
					}
				}
				else
				{
					numRows=GetWebNumeroFilasColumnFromPropertiesC(column);
					numCols=50;
					
					strType="<textarea";
					strRows=" rows=\""+numRows.ToString()+"\"";
					strMaxLength=" cols=\""+numCols.ToString()+"\"";
					
					
					if(!ConFaces)
					{
						strControl=strType+strName+strMaxLength+strRows+GetOnBlurEventValidationControlHtml(column)+"></textarea>";
					}
					else
					{
						strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";
						
						strControl="<h:inputTextarea "+strName.Replace("name=","id=")+strValueFace+strRows+strMaxLength+" />";
					}
				}
			}
			
			else if(column.DataType==DbType.Boolean)
			{
				strType="\"checkbox\"";
				strName=" name=\""+GetNameControlHtml(column)+"\"";
								
				if(!ConFaces)
				{
					strControl="<input type="+strType+strName+">";
				}
				else
				{
					strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";
					
					strControl="<h:selectBooleanCheckbox "+strName.Replace("name=","id=")+strValueFace+" />";
				}
	
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
				strType="\"text\"";
				strName=" name=\""+GetNameControlHtml(column)+"\"";
				String strMaxLength=" maxlength=\""+column.Size.ToString()+"\"";				
				
				if(!ConFaces)
				{
					strControl="<input type="+strType+strMaxLength+strName+GetOnBlurEventValidationControlHtml(column)+">";
				}
				else
				{
					strValueFace=" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" ";
					
					strControl="<h:inputText "+strName.Replace("name=","id=")+strValueFace+strMaxLength+" />";
				}
	
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				strType="\"dojo.dropdowndatepicker\"";
				strName=" id=\""+GetNameControlHtml(column)+"\"";
				strControl="<a:widget  name="+strType+strName+"/>";
	
			}
		}
		else
		{

			strName=" id=\""+GetNameControlHtml(column)+"\"";
		
		
			if(!ConFaces)
			{
				strControl="<a:widget"+strName+"name=\"dojo.combobox\""+"/>";
			}
			else
			{
				strControl="<h:selectOneMenu "+strName.Replace("name=","id=")+" value=\"#{"+strObjectFace+"."+strPrefijoCampo+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column)+"}\" >\r\n";
				strControl+="\t\t\t\t\t\t\t\t<f:selectItems value=\"#{"+strObjectFace+"."+GetNombreClaseRelacionadaFromColumn(column).ToLower() +"s"+strForeignKey+"ListSelectItem}\"/>\r\n";		
				strControl+="\t\t\t\t\t\t\t</h:selectOneMenu>";
			}	
	
		}
	}
	
	return strControl;
}
		
	
	public String GetEsquemasImportClasesRelacionadas(String strEsquemaIni,String strEsquemaFin) {
		String strImportRel="";
		
		if(arrayListEsquemasRel!=null) {
			foreach(String strEsquema in arrayListEsquemasRel) {			
				//strImportRel+="\r\n"+strEsquemaIni+"."+strEsquema.ToLower()+"."+strEsquemaFin+"";
				strImportRel+=GetEsquemasImportClasesRelacionadas("",strEsquemaIni,strEsquema,strEsquemaFin);
			}
		}
		
		return strImportRel;
	}
	
	public String GetEsquemasImportClasesRelacionadas(String strEsquemaIni,String strEsquemaFin,bool ConEjb) {
		String strImportRel="";
		String strComment="";
		
		if(ConEjb){
			strComment="//";
		}
		
		if(arrayListEsquemasRel!=null) {
			foreach(String strEsquema in arrayListEsquemasRel) {			
				//strImportRel+="\r\n"+strComment+strEsquemaIni+"."+strEsquema.ToLower()+"."+strEsquemaFin+"";
				strImportRel+=GetEsquemasImportClasesRelacionadas(strComment,strEsquemaIni,strEsquema,strEsquemaFin);
			}
		}
		
		return strImportRel;
	}
	
	public String GetEsquemasImportClasesRelacionadas(String strComment,String strEsquemaIni,String strEsquema,String strEsquemaFin) {
		String strReturn="";
		
		strReturn="\r\n"+strComment+strEsquemaIni+"."+strEsquema.ToLower()+"."+strEsquemaFin+"";
		
		return strReturn;
	}
	
	public ArrayList GetEsquemasClasesRelacionadas(TableSchema TablaBase) {
			//Trace.WriteLine("_____________________________");
		
			String strTablaClaseRelacionada=string.Empty;
			TableSchema tableSchemaFK=null;
			TableSchema tableSchemaRel=null;
			
			System.Collections.Hashtable tablasRelacionadas=GetTablasRelacionadas(TablaBase);
			ArrayList arrayListEsquemas=new ArrayList();
			
			//FOREIGN KEY
			//blnNoStandardTableFromProperties=true;
			if(!blnNoStandardTableFromProperties) {										
				foreach(ColumnSchema columnSchema in TablaBase.Columns)	{						
					if(columnSchema.IsForeignKeyMember)	{
						if(ExisteTablaEnTablasRelacionadasC(TablaBase,GetNombreClaseRelacionadaFromColumn(columnSchema)))	{
							continue;
						}
						//Trace.WriteLine(columnSchema.Name);
						
						//strTablaClaseRelacionada+="\r\n\tpublic "+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(columnSchema))+" "+GetPrefijoRelacionC().ToLower()+GetNombreClaseC("dbo."+GetNombreCompletoClaseRelacionadaFromColumn(columnSchema))+";";
						tableSchemaFK=GetTableSchemaFromColumnForeignKey(columnSchema);
						//Trace.WriteLine(tableSchemaFK.Owner);						
						AgregarEsquema(arrayListEsquemas,tableSchemaFK.Owner,TablaBase);
					}
				}
			} else {
				ArrayList arrayListForeignKeys =GetArrayListForeignKeys(TablaBase);
								
				//foreach(TableSchema tableSchemaForeignKey in arrayListForeignKeys) {
					//strTablaClaseRelacionada+="\r\n\tpublic "+GetNombreClaseC(tableSchemaForeignKey.ToString())+" "+GetPrefijoRelacionC().ToLower()+GetNombreClaseC(tableSchemaForeignKey.ToString())+";";					
				//}
			}
				
			
			
			//RELACIONADAS
			//String strTablaClaseRelacionada=string.Empty;
			/*System.Collections.Hashtable*/ tablasRelacionadas=GetTablasRelacionadas(TablaBase);
			
			ArrayList arrayListRelaciones=new ArrayList();
			String strNombreAdicional="";
			
			//Trace.WriteLine("TABLAS-RELACIONADAS");
			foreach(CollectionInfo collectionInfo in tablasRelacionadas.Values) {
				strNombreAdicional=GetNombreAdicionalClaseRelacionadaFromRelation(collectionInfo);					
				//NO FUNCIONA EN DESORDEN DE RELACIONES
								
				if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
					//strTablaClaseRelacionada+="\r\n\tpublic "+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+ " "+GetPrefijoRelacionC().ToLower()+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+";";
					tableSchemaRel=collectionInfo.SecondaryTableSchema;
					AgregarEsquema(arrayListEsquemas,tableSchemaRel.Owner,TablaBase);
				} else {
					//strTablaClaseRelacionada+="\r\n\tpublic "+strTypeCollection+"<"+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+"> "+GetPrefijoRelacionC().ToLower()+GetNombreClaseC("dbo."+collectionInfo.SecondaryTable)+strNombreAdicional+ "s;";
					tableSchemaRel=collectionInfo.SecondaryTableSchema;
					AgregarEsquema(arrayListEsquemas,tableSchemaRel.Owner,TablaBase);
				}
				
				//Trace.WriteLine(tableSchemaRel.Owner);
			}			
			
		return arrayListEsquemas; 
	}
		
	public static void AgregarEsquema(ArrayList arrayListEsquemas,String strEsquemaNuevo,TableSchema TablaBase) {
		bool blnExiste=false;
		
		foreach(String strEsquema in arrayListEsquemas) {
			//Trace.WriteLine(strEsquema+"="+strEsquemaNuevo);
			
			if(strEsquema.Equals(strEsquemaNuevo)) {
				blnExiste=true;
				
				break;
			}
		}
		
		if(!blnExiste && !strEsquemaNuevo.Equals(TablaBase.Owner)) {
			//Trace.WriteLine(strEsquemaNuevo);
			arrayListEsquemas.Add(strEsquemaNuevo);
		}
	}
	
	public bool ExisteTablaEnTablasRelacionadasC(SchemaExplorer.TableSchema table,String tabla) 
		{
			bool existe= false;
			
			SchemaExplorer.TableSchemaCollection tablasRelacionadas=new SchemaExplorer.TableSchemaCollection();
			System.Collections.Hashtable tablasRelacionadasFinales=new System.Collections.Hashtable();
			
			for (int i = 0; i < table.Database.Tables.Count; i++)
			{ 	
         	if(table.Database.Tables[i].Equals(table))
			{
				continue;
			}
			tablasRelacionadas.Add(table.Database.Tables[i]);
			}
								
			tablasRelacionadasFinales=GetChildrenCollections(table, tablasRelacionadas); 
			
			foreach(CollectionInfo collectionInfo in tablasRelacionadasFinales.Values)
			{	//BYDAN-RECURSIVO
				if(collectionInfo.SecondaryTable==tabla&&collectionInfo.SecondaryTable!=table.Name)
				{
				existe= true;
				}
			}
		return existe;
		}
		
   public static string GetNombreClaseC(string strTablaBase)
	{
	//strTablaBase=strTablaBase.Replace("dbo.",string.Empty);
	string [] strTabla = strTablaBase.Split('.');
	string strNombreClase=string.Empty;
	
	strNombreClase=strTabla[1];		
	strNombreClase=strNombreClase.Replace("\"","");		
	return strNombreClase;
	}
	
	 public static string GetNombreClasePersistenceC(string strTablaBase)
	{
	//strTablaBase=strTablaBase.Replace("dbo.",string.Empty);
	string [] strTabla = strTablaBase.Split('.');
	string strNombreClase=string.Empty;
	
	strNombreClase=strTabla[1];		
	strNombreClase=strNombreClase.Replace("\"","");		
	
	strNombreClase=strNombreClase.Substring(0,1).ToUpper()+strNombreClase.Substring(1,strNombreClase.Length-1).ToLower();	
		
	
	return strNombreClase;
	}
	
	public static string GetNombreClaseObjetoC(string strTablaBase)
	{
	//strTablaBase=strTablaBase.Replace("dbo.",string.Empty);
	string [] strTabla = strTablaBase.Split('.');
	string strNombreClase=string.Empty;
	
	strNombreClase=strTabla[1].Substring(0, 1).ToLower()+strTabla[1].Substring(1, strTabla[1].Length-1).ToLower();		
	strNombreClase=strNombreClase.Replace("\"","");		
	return strNombreClase;
	}
	
	public static string GetPrefijoTablaC()
	{
	string strPrefijoTabla="";//"Field"+"_";
	return strPrefijoTabla;
	}
	
	public static string GetPrefijoRelacionC()
	{
	string strPrefijoRelacion="";//"Relationship"+"_";
	return strPrefijoRelacion;
	}
	
	public static string GetPrefijoTipoC(ColumnSchema column)
	{
		return "";
		
		string strPrefijoTipo = column.NativeType.ToString().Substring(0, 3).ToLower();
		
		if(GetTipoColumnaFromColumn(column)!="")
		{
			strPrefijoTipo =GetTipoColumnaFromColumn(column).Substring(0, 3).ToLower();
		}
		
	switch (strPrefijoTipo)
	{
		case "big":
		{
			strPrefijoTipo="l" ;
			break;
		}
		case "int":
		{
			strPrefijoTipo="i" ;
			break;
		}
		case "bit":
		{
			strPrefijoTipo="is" ;
			break;
		}
		case "dec":
		{
			strPrefijoTipo="d" ;
			break;
		}
		case "nva":
		{
			strPrefijoTipo="s" ;
			break;
		}
		case "var":
		{
			strPrefijoTipo="s" ;
			break;
		}
		case "dat":
		{
			strPrefijoTipo="s" ;
			break;
		}
		case "tim":
		{
			if(column.Name==strVersionRow)
			{
				strPrefijoTipo=  "s";
			}
			
			break;
		}
		
		
	}
	return strPrefijoTipo;
	}
	
	public static string GetPrefijoTipoToGetSetC(ColumnSchema column)
	{
		string strPrefijoTipo =column.NativeType.ToString().Substring(0, 1).ToUpper()+ column.NativeType.ToString().Substring(1, 2).ToLower();
	
		if(GetTipoColumnaFromColumn(column)!="")
		{
			strPrefijoTipo =GetTipoColumnaFromColumn(column).Substring(0, 1).ToUpper()+GetTipoColumnaFromColumn(column).Substring(1, 2).ToLower();
		}
	
	switch (strPrefijoTipo)
	{
		case "Nva":
		{
			strPrefijoTipo="Str" ;
			break;
		}
		case "Var":
		{
			strPrefijoTipo="Str" ;
			break;
		}
		case "Dat":
		{
			strPrefijoTipo="Str" ;
			break;
		}
		case "Tim":
		{
			if(column.Name==strVersionRow)
			{
				strPrefijoTipo=  "Str";
			}
			
			break;
		}
		
		
	}
	return strPrefijoTipo;
	}
	
	public  string GetXmlColumnaFuncionDescripcionC(ColumnSchema column,String tablaBase)
	{
	string strInitFuncion="\r\n\tpublic static String get"+GetPrefijoRelacionC();
	string strNombre="";

		
	if(column.IsForeignKeyMember)
	{
		
	strNombre+=strInitFuncion+ GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"Descripcion("+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+" "+GetNombreClaseObjetoC("dbo."+GetNombreClaseRelacionadaFromColumn(column)) +") {\r\n";
	strNombre+="\t\tString sDescripcion=\"none\";\r\n\r\n";
	strNombre+="\t\tif("+GetNombreClaseObjetoC("dbo."+GetNombreClaseRelacionadaFromColumn(column)) +"!=null&&"+GetNombreClaseObjetoC("dbo."+GetNombreClaseRelacionadaFromColumn(column)) +".get"+strIdGetSet+"()!=0) {\r\n";
	strNombre+="\t\t\tsDescripcion="+GetNombreClaseObjetoC("dbo."+GetNombreClaseRelacionadaFromColumn(column)) +"."+GetXmlColumnaFuncionDescripcionFromPropertiesC(column)+";";
	strNombre+="\r\n\t\t}\r\n\r\n";
	strNombre+="\t\treturn sDescripcion;\r\n\t}";	
	}
		
	return strNombre;
	}
	
	
	public  String GetXmlColumnaFuncionDescripcionFromPropertiesC(ColumnSchema column)
	{
	
	TableSchema tablaRelacionada=column.Table;//GetNombreTablaRelacionadaFromColumn(column);
	
	String strNombreTabla=GetNombreClaseRelacionadaFromColumn(column);
	
	foreach(TableSchema tableForeignKey in column.Database.Tables)
	{
		if(tableForeignKey.Name.Equals(strNombreTabla))
		{
			tablaRelacionada=tableForeignKey;
		}
	}
	
	
	string strPrefijoTabla="";
	string strPrefijoTipo =""; 
	string strNombre = "";
	
	String strColumnaDetalle="get"+strIdGetSet+"()"+GetTipoColumnaToString(column);
	String[] descripciones;
	String[] tipo;
	
	foreach(ColumnSchema columnSchema in tablaRelacionada.Columns)
	{
		if(columnSchema.Name!=strId)
		{
			foreach(ExtendedProperty extendedProperty in columnSchema.ExtendedProperties)
			{
				if(extendedProperty.Name=="CS_Description")
				{
				descripciones=((String)extendedProperty.Value).Split('|');
						
						foreach(String descripcion in descripciones)
						{
							tipo=descripcion.Split('=');
							
							if(tipo[0].Equals("WEBCOMBO"))
							{
								//if(tipo[1]=="true")
								//{
									strPrefijoTabla=GetPrefijoTablaC();
									strPrefijoTipo = GetPrefijoTipoC(columnSchema);
									strNombre = GetNombreColumnaClaseC(columnSchema);	
									
									strColumnaDetalle="get"+strPrefijoTabla+strPrefijoTipo+strNombre+"()"+GetTipoColumnaToString(columnSchema);
									break;
								//}
								
								
							}
						}
				}
						
			}
		}
	}
				
	return strColumnaDetalle;
	}
	
	public string GetXmlColumnaC(ColumnSchema column,String tablaBase,bool blnEsParaCompuesto)
	{
	string strNombre="\r\n";
	string strGetColumn="";
	string strPrefijoTabla="";
	string strPrefijoTipo="";
	string strNombreColumna="";
	
		strPrefijoTabla=GetPrefijoTablaC();
	    strPrefijoTipo=GetPrefijoTipoC(column);
	    strNombreColumna=GetNombreColumnaClaseC(column);
		strGetColumn="get"+GetNombreCompletoColumnaClaseC(column)+"()"+GetTipoColumnaToString(column);
	
	if(column.DataType==DbType.Binary&&column.Name!=strVersionRow)
	{
		return "";
	}
	
	if(column.Name==strId)
	{
	  strNombre="\txml.append(\"<item code=\\\"\"+"+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+"+\"\\\">\");\r\n\r\n";
	}

	
	if(!blnEsParaCompuesto)
	{
		strNombre+= "\t\t\t\txml.append(\"<";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
		strNombre+=">\");\r\n";
		
		strNombre+="\t\t\t\txml.append("+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+");\r\n";
		
		strNombre+="\t\t\t\txml.append(\"</";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
		strNombre+=">\");";
	}
	else
	{
		strNombre+= "\t\t\t\txml.append(\"<";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
		strNombre+=">\");\r\n";
		
		if(column.Name==strId)
		{
			strNombre+="\r\n\t\t\t\tif("+GetNombreClaseObjetoC(tablaBase)+".get"+strIdGetSet+"()!=0&&"+GetNombreClaseObjetoC(tablaBase)+".get"+strIdGetSet+"()!=null)\r\n\t\t\t\t{\r\n";
			strNombre+="\t\t\t\t\txml.append("+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+");\r\n";		
			
			strNombre+="\t\t\t\t}\r\n\t\t\t\telse\r\n\t\t\t\t{\r\n\t\t\t\t\txml.append("+strId+"Temporal.toString());\r\n\t\t\t\t\t"+strId+"Temporal--;\r\n\t\t\t\t}\r\n\r\n";
			
		}
		else
		{
			strNombre+="\t\t\t\txml.append("+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+");\r\n";
		}
		
		strNombre+="\t\t\t\txml.append(\"</";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
		strNombre+=">\");";
	}
	
	if(column.IsForeignKeyMember)
	{
		strNombre+= "\r\n\t\t\t\txml.append(\"<";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
		strNombre+=">\");\r\n";
		
		strNombre+="\t\t\t\txml.append("+GetNombreClaseC(tablaBase)+"ConstantesFunciones.get"+GetPrefijoRelacionC()+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"Descripcion("+GetNombreClaseObjetoC(tablaBase) +".get"+GetPrefijoRelacionC() +GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"()));\r\n";
		
		strNombre+="\t\t\t\txml.append(\"</";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion"+strSeparadorXml+GetNombreClaseObjetoC(column.Table.ToString()).ToLower();
		strNombre+=">\");\r\n";
		
		return strNombre;
	}
		
	return strNombre;
	}
	
	public string GetXmlColumnaCompuestoC(ColumnSchema column,String tablaBase)
	{
	string strNombre="\r\n";
	string strGetColumn="";
	string strPrefijoTabla="";
	string strPrefijoTipo="";
	string strNombreColumna="";
	
		strPrefijoTabla=GetPrefijoTablaC();
	    strPrefijoTipo=GetPrefijoTipoC(column);
	    strNombreColumna=GetNombreColumnaClaseC(column);
		strGetColumn="get"+GetNombreCompletoColumnaClaseC(column)+"()"+GetTipoColumnaToString(column);
	
	if(column.DataType==DbType.Binary&&column.Name!=strVersionRow)
	{
		return "";
	}
	
	if(column.Name==strId)
	{
	  ;//strNombre="\txml.append(\"<item code=\\\"\"+"+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+"+\"\\\">\\r\n\");\r\n\r\n";
	}


	strNombre+= "\t\t\t\txml.append(\"<";
	strNombre+=GetNombreClaseObjetoC(tablaBase)+ column.Name.Substring(0, column.Name.Length).ToLower();
	strNombre+=">\");\r\n";
	
	strNombre+="\t\t\t\txml.append("+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+");\r\n";
	
	strNombre+="\t\t\t\txml.append(\"</";
	strNombre+=GetNombreClaseObjetoC(tablaBase)+column.Name.Substring(0, column.Name.Length).ToLower();
	strNombre+=">\");";
	
	if(column.IsForeignKeyMember)
	{
		strNombre+= "\r\n\t\t\t\txml.append(\"<";
		strNombre+=GetNombreClaseObjetoC(tablaBase)+column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion";
		strNombre+=">\");\r\n";
		
		strNombre+="\t\t\t\txml.append("+GetNombreClaseC(tablaBase)+"ConstantesFunciones.get"+GetPrefijoRelacionC()+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"Descripcion("+GetNombreClaseObjetoC(tablaBase) +".get"+GetPrefijoRelacionC() +GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"()));\r\n";
		
		strNombre+="\t\t\t\txml.append(\"</";
		strNombre+=GetNombreClaseObjetoC(tablaBase)+column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion";
		strNombre+=">\");\r\n";
		
		return strNombre;
	}
		
	return strNombre;
	}
	
	public string GetXmlColumnaAdditionalC(ColumnSchema column,String tablaBase)
	{
	string strNombre="\r\n";
	string strGetColumn="";
	string strPrefijoTabla="";
	string strPrefijoTipo="";
	string strNombreColumna="";
	
		strPrefijoTabla=GetPrefijoTablaC();
	    strPrefijoTipo=GetPrefijoTipoC(column);
	    strNombreColumna=GetNombreColumnaClaseC(column);
		strGetColumn="get"+GetNombreCompletoColumnaClaseC(column)+"()"+GetTipoColumnaToString(column);
	
	if(column.DataType==DbType.Binary&&column.Name!=strVersionRow)
	{
		return "";
	}
	
	if(column.Name==strId)
	{
	  strNombre="\txml.append(\"<item code=\\\"\"+"+GetNombreClaseObjetoC(tablaBase)+"."+strGetColumn+"+\"\\\">\\r\n\");\r\n\r\n";
	}


	strNombre+= "\t\t\t\txml.append(\"<";
	strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
	strNombre+=">\");\r\n";
	
	strNombre+="\t\t\t\txml.append("+GetNombreClaseObjetoC(tablaBase)+"A."+strGetColumn+");\r\n";
	
	strNombre+="\t\t\t\txml.append(\"</";
	strNombre+= column.Name.Substring(0, column.Name.Length).ToLower();
	strNombre+=">\\r\n\");";
	
	if(column.IsForeignKeyMember)
	{
		strNombre+= "\r\n\t\t\txml.append(\"<";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcinn";
		strNombre+=">\");\r\n";
		
		strNombre+="\t\t\txml.append(this.get"+GetPrefijoRelacionC()+GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"Descripcion("+GetNombreClaseObjetoC(tablaBase) +"A.get"+GetPrefijoRelacionC() +GetNombreClaseC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+"()));\r\n";
		
		strNombre+="\t\t\txml.append(\"</";
		strNombre+= column.Name.Substring(0, column.Name.Length).ToLower()+"Descripcion";
		strNombre+=">\\r\n\");\r\n";
		
		return strNombre;
	}
		
	return strNombre;
	}
	
	public static string GetNombreColumnaClaseMinusculaC(ColumnSchema column)
	{
string strNombre = column.Name.Substring(0, column.Name.Length).ToLower();
	return strNombre;
	}
	
	public static string GetNombreColumnaClaseC(ColumnSchema column)
	{string strNombre = "";
	if(!column.IsForeignKeyMember) {
			if(!column.Name.Equals(strId)) {
				strNombre =GetNombreColumnFromProperties(column);//column.Name;//.Substring(0, 1).ToUpper() + column.Name.Substring(1, column.Name.Length-1).ToLower();
			} else {
				strNombre =strId;
			}
		} else {
			if(!column.Name.Equals(strId)) {
				strNombre =GetNombreColumnFromProperties(column);//column.Name;//column.Name.Substring(0, 1).ToUpper() + column.Name.Substring(1, column.Name.Length-1);
			} else {
				//CUANDO RELACION ES DE UNO A UNO
				TableSchema tableSchema=GetTableSchemaFromColumnForeignKey(column);
			
				//strNombre =strIdGetSet+GetNombreClaseC(tableSchema.ToString());
				strNombre ="";//column.Name;//GetNombreColumnFromProperties(column);
				strNombre =GetParameterClaseFkRelacionadoC(tableSchema);
				
				//strNombre =GetNombreColumnFromProperties(column);
			}						
		}
		
		return strNombre;
	}
	
	public static string GetNombreTablaRelacionMappedByClaseC(TableSchema TablaBase) {
		string strNombre =GetPrefijoRelacionC().ToLower();//+GetNombreClaseObjetoC(TablaBase.ToString());
		
		string strNombreClase =GetNombreClaseC(TablaBase.ToString());
		
		strNombre =strNombreClase.Substring(0,1).ToLower()+strNombreClase.Substring(1,strNombreClase.Length-1);
		
		return strNombre;
	}
	
	public static  string GetParameterClaseFkRelacionadoC(TableSchema tableSchema)	{
		String strNombre="";
		
		strNombre =strId+"_"+GetNombreTableFromProperties(tableSchema);//GetNombreClaseObjetoC(tableSchema.ToString());
		
		if(blnEsLowerCaseDBNames) {
			strNombre=strId+"_"+GetNombreTableFromProperties(tableSchema);//GetNombreClaseObjetoC(tableSchema.ToString());
			strNombre=strNombre.ToLower();
		} else {	
			if(blnEsMixedCaseDBNames) {
				strNombre=strIdGetSet+"_"+GetNombreClaseC(tableSchema.ToString());
			} else {
				strNombre=strId+"_"+GetNombreTableFromProperties(tableSchema);//GetNombreClaseC(tableSchema.ToString());
				strNombre=strNombre.ToUpper();
			}
		}
		
		return strNombre;
	}
	/*
	TableKeySchema TableKey;
	TableKey.ForeignKeyMemberColumns
	if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany)
	*/
	
	public static  string GetParameterClaseDeepRelacionadoC(CollectionInfo collectionInfo)	{
		String strColumnName="";
		string strPrefijo=String.Empty;
		string strPrefijoTabla="";
		string strPrefijoTipo ="";					
					
		TableKeySchema tableKey=collectionInfo.TableKey;
		ColumnSchemaCollection columnSchemaCollection; 
		
		
		if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToOne) {
			columnSchemaCollection=tableKey.ForeignKeyMemberColumns;
		} else {
			if(collectionInfo.CollectionRelationshipType==RelationshipType.OneToMany) {
				columnSchemaCollection=tableKey.ForeignKeyMemberColumns;
			} else {
				columnSchemaCollection=tableKey.ForeignKeyMemberColumns;
			}
		}	
		
		if(columnSchemaCollection!=null) {
			foreach(ColumnSchema columnSchema in columnSchemaCollection) {
					/*
					strPrefijo=String.Empty;
					strPrefijoTabla=GetPrefijoTablaC().ToLower();
					strPrefijoTipo =GetPrefijoTipoC(columnSchema);
				
					strPrefijo=strPrefijoTabla+strPrefijoTipo;
					
					strColumnName = GetNombreColumnaClaseC(columnSchema);
					strColumnName=strPrefijo+strColumnName;
					*/
					strColumnName=GetNombreColumnaClaseC(columnSchema);
					//Trace.WriteLine(strColumnName);
					break;
			}
		}
		return strColumnName;
	}
	
	public string GetParameterClaseC(ColumnSchema column)
	{
	
	if(column.Name==strId||column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
	{
		return"";
	}
	
	string strPrefijo=String.Empty;
	string strPrefijoTabla=GetPrefijoTablaC().ToLower();
	string strPrefijoTipo =GetPrefijoTipoC(column);

	strPrefijo=strPrefijoTabla+strPrefijoTipo;
	
	string strNombre = GetNombreColumnaClaseC(column);
	strPrefijo+=strNombre;
		
	string param =  GetTipoColumnaClaseC(column);
	
	return "private "+param+" "+strPrefijo+";";
	}
	
	public string GetValueDefaultParameterClaseC(ColumnSchema column)
{
	
	if(column.Name==strId||column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
	{
		return"";
	}
	string strPrefijo=String.Empty;
	string strValor=String.Empty;
	
	string strPrefijoTabla=GetPrefijoTablaC().ToLower();
	string strPrefijoTipo =GetPrefijoTipoC(column);

	strPrefijo=strPrefijoTabla+strPrefijoTipo;
	
	switch(column.Name)
	{
		case strIsActive:
		strValor="";
		break;
		
		case strIsExpired:
		strValor="";
		break;
		
		case strVersionRow:
		strValor="";
		break;
		
		default:
		strValor=GetDefaultValueColumna(column);
		break;
	}
	
	
	
	string strNombre = GetNombreColumnaClaseC(column);
	strPrefijo+=strNombre;

			
	return strPrefijo+"="+strValor+";";
}
	
	public string GetParameterClaseMethodC(ColumnSchema column,bool EsTotalSimple,int i)
{
	
	if(column.Name==strId||column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
	{
		return"";
	}
	string strPrefijo=String.Empty;
	string strPrefijoTabla=GetPrefijoTablaC();
	string strPrefijoFuncion="\r\n\t"+GetPersistenciaColumnaClaseC(column)+"\r\n\tpublic ";
	string strPrefijoTipo = GetPrefijoTipoC(column);

	if(EsTotalSimple && i.Equals(0)) {
		strPrefijoFuncion="@Id"+strPrefijoFuncion;
	}
	
	strPrefijo=strPrefijoFuncion;	
	string strNombre = GetNombreColumnaClaseC(column);		
	string param =  GetTipoColumnaClaseC(column);
		
	
	strPrefijo+=param+" get"+strPrefijoTabla+strPrefijoTipo+strNombre+"() {";
	strPrefijo+="\r\n\t\treturn "+strPrefijoTabla.ToLower()+strPrefijoTipo+strNombre+";\r\n\t}";

	return strPrefijo;
}

public string GetParameterClaseSetMethodC(ColumnSchema column,bool EsTotalSimple)
{
	if(column.Name==strId||column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
	{
		return"";
	}
	string strPrefijo=String.Empty;
	string strPrefijoTabla=GetPrefijoTablaC();
	string strPrefijoFuncion="\r\n\tpublic void set";
	string strPrefijoTipo = GetPrefijoTipoC(column);
	
	strPrefijo=strPrefijoFuncion;
	
	string strNombre = GetNombreColumnaClaseC(column);
		
	string param =  GetTipoColumnaClaseC(column);
	
	string strCommentException="";
	
	if(EsTotalSimple) {
		strCommentException="//";
	}
	
	if(column.AllowDBNull)
	{
		strPrefijo+=strPrefijoTabla+strPrefijoTipo+strNombre+"("+param+ " new"+strPrefijoTabla+strPrefijoTipo+strNombre +")";
		
		if(column.DataType==DbType.AnsiString||column.DataType==DbType.AnsiStringFixedLength||
		   column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
		{
			strPrefijo+="throws Exception";
		}
		
		strPrefijo+=" {";
		
		strPrefijo+="\r\n\t\tif(this."+strPrefijoTabla.ToLower()+strPrefijoTipo+strNombre+"!="+"new"+strPrefijoTabla+strPrefijoTipo+strNombre+") {";			
		
		if(column.DataType==DbType.AnsiString||column.DataType==DbType.AnsiStringFixedLength||
		   column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
		{
			strPrefijo+="\r\n\r\n\t\t\t\tif("+"new"+strPrefijoTabla+strPrefijoTipo+strNombre+".length()>"+column.Size+") {\r\n\t\t\t\t\ttry {\r\n\t\t\t\t\t\t";
			strPrefijo+=strCommentException+"throw new Exception(\""+GetNombreClaseC(column.Table.ToString())+":Ha sobrepasado el numero maximo de caracteres permitidos,Maximo="+GetNombreColumnaClaseC(column)+" en columna "+column.Name+"\");";
			strPrefijo+="\r\n\t\t\t\t\t} catch(Exception e) {\r\n\t\t\t\t\t\tthrow e;\r\n\t\t\t\t\t}\r\n\t\t\t\t}";
		}
		
		strPrefijo+="\r\n\r\n\t\t\tthis."+strPrefijoTabla.ToLower()+strPrefijoTipo+strNombre+"=new"+strPrefijoTabla+strPrefijoTipo+strNombre+";"+ "\r\n\t\t\tthis.setIsChanged(true);\r\n\t\t}\r\n\t}";
	}
	else
	{
		strPrefijo+=strPrefijoTabla+strPrefijoTipo+strNombre+"("+param+ " new"+strPrefijoTabla+strPrefijoTipo+strNombre +")throws Exception\r\n\t{";
		strPrefijo+="\r\n\t\ttry {\r\n\t\t\tif(this."+strPrefijoTabla.ToLower()+strPrefijoTipo+strNombre+"!="+"new"+strPrefijoTabla+strPrefijoTipo+strNombre+") {\r\n\t\t\t\tif("+"new"+strPrefijoTabla+strPrefijoTipo+strNombre+"==null) {\r\n\t\t\t\t\tthrow new Exception(\""+GetNombreClaseC(column.Table.ToString())+":Valor nulo no permitido en columna " +GetNombreColumnaClaseC(column)+"\");\r\n\t\t\t\t}";			
		
		if(column.DataType==DbType.AnsiString||column.DataType==DbType.AnsiStringFixedLength||
		   column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
		{
			strPrefijo+="\r\n\r\n\t\t\t\tif("+"new"+strPrefijoTabla+strPrefijoTipo+strNombre+".length()>"+column.Size+") {\r\n\t\t\t\t\t";
			strPrefijo+=strCommentException+"throw new Exception(\""+GetNombreClaseC(column.Table.ToString())+":Ha sobrepasado el numero maximo de caracteres permitidos,Maximo="+column.Size+" en columna "+GetNombreColumnaClaseC(column)+"\");";
			strPrefijo+="\r\n\t\t\t\t}";
		}
		
		strPrefijo+="\r\n\r\n\t\t\t\tthis."+strPrefijoTabla.ToLower()+strPrefijoTipo+strNombre+"=new"+strPrefijoTabla+strPrefijoTipo+strNombre+";"+ "\r\n\t\t\t\tthis.setIsChanged(true);\r\n\t\t\t}\r\n\t\t} catch(Exception e) {\r\n\t\t\tthrow e;\r\n\t\t}\r\n\t}";
	}
	
	
	return strPrefijo;
}

	public static string GetNombreColumnaClaseTituloC(ColumnSchema column)
	{
	string strNombre =  column.Name.Substring(0, column.Name.Length-1).ToUpper();
	return strNombre;
	}
	
	public static bool GetExistTagColumnaFromColumnPropiertiesC(ColumnSchema column,String strTag)
	{
	bool blnExistTag=false;
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals(strTag))
					{
						blnExistTag=true;
						//nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return blnExistTag;
	}
	
	public static string GetTipoColumnaFromColumn(ColumnSchema column)
	{
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPO"))
					{
						nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return nombreClase;
	}
	
	public static string GetTipoColumnaStoreProcedureFromColumn(ColumnSchema column)
	{
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPOSQL"))
					{
						nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return nombreClase;
	}
	
	public static string GetTipoParseColumnaFromColumn(ColumnSchema column,String data)
	{
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPO"))
					{
						nombreClase=tipo[1];//+".valueOf("+data+")";
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return nombreClase;
	}
	
	public static string GetTipoResultSetColumnaFromColumn(ColumnSchema column)
	{
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPO"))
					{
						nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return nombreClase;
	}
	
	public static string GetToStringFromTipoColumnaFromColumn(ColumnSchema column)
	{
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPO"))
					{
						nombreClase=".toString()";
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return nombreClase;
	}
	
	public static int GetValorHorizontalWebColumnaClaseC(ColumnSchema column)
	{
		//Trace.WriteLine("H3123");
	int tipoColumna = 0;// GetTipoColumnaFromColumn(column);
	//System.Windows.Forms.MessageBox.Show("a");
	//Trace.WriteLine(column.Name+"-"+tipoColumna);
	if(tipoColumna>0)
	{
		return tipoColumna;
	}
		
	int param =  10;
	string paramType =  column.DataType.ToString();
	
	switch (column.DataType)
	{
		case DbType.Boolean:
		{
			param =  10;
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  10;
				//param =  "Date";
			}
			else
			{	param =  10;
				//param =  "byte []";
			}
			
			break;
		}
		case DbType.DateTime:
		{
			param =  10;
			//param =  strTipoParaFecha;
			break;
		}
		case DbType.AnsiString:
		{
			param =  10;
			break;
		}
		case DbType.Int32:case DbType.UInt32:
		{
			param =  10;
			break;
		}
		case DbType.Int64:case DbType.UInt64:
		{
			param =  10;
			break;
		}
		case DbType.Int16:case DbType.UInt16:
		{
			param =  10;
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  10;
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  10;
			break;
		}
		
		
		case DbType.String:
		{
			param =  10;
			break;
		}
		
		case DbType.Decimal:case DbType.Double:
		{
			param =  10;
			break;
		}
		default:
		{
			param =  10;
			//param =  "None:"+paramType ;
			break;
		}

	}
	
	return param;
	}
	
	public static int GetValorVerticalWebColumnaClaseC(ColumnSchema column)
	{
		//Trace.WriteLine("H3123");
	int tipoColumna = 0;// GetTipoColumnaFromColumn(column);
	//System.Windows.Forms.MessageBox.Show("a");
	//Trace.WriteLine(column.Name+"-"+tipoColumna);
	if(tipoColumna>0)
	{
		return tipoColumna;
	}
		
	int param =  10;
	string paramType =  column.DataType.ToString();
	
	switch (column.DataType)
	{
		case DbType.Boolean:
		{
			param =  10;
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  10;
				//param =  "Date";
			}
			else
			{	param =  10;
				//param =  "byte []";
			}
			
			break;
		}
		case DbType.DateTime:
		{
			param =  10;
			//param =  strTipoParaFecha;
			break;
		}
		case DbType.AnsiString:
		{
			param =  10;
			break;
		}
		case DbType.Int32:case DbType.UInt32:
		{
			param =  10;
			break;
		}
		case DbType.Int64:case DbType.UInt64:
		{
			param =  10;
			break;
		}
		case DbType.Int16:case DbType.UInt16:
		{
			param =  10;
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  10;
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  10;
			break;
		}
		
		
		case DbType.String:
		{
			param =  10;
			break;
		}
		
		case DbType.Decimal:case DbType.Double:
		{
			param =  10;
			break;
		}
		default:
		{
			param =  10;
			//param =  "None:"+paramType ;
			break;
		}

	}
	
	return param;
	}
	
	public static bool EsTipoColumnaStringParaTrimClaseC(ColumnSchema column)
	{
		bool esTipoColumnaParaRegularExpresion=false;
	
		string param =  column.NativeType;
		string paramType =  column.DataType.ToString();
		
		switch (column.DataType) {
			 case DbType.AnsiString: case DbType.AnsiStringFixedLength:case DbType.StringFixedLength:case DbType.String: {
				esTipoColumnaParaRegularExpresion=true;
				break;

			} default: {
				esTipoColumnaParaRegularExpresion=false;
				break;
			}
		}
		
		return esTipoColumnaParaRegularExpresion;
	}
	
	public  bool EsTipoColumnaNumeroParaClaseC(ColumnSchema column)
	{
		bool esTipoColumnaParaRegularExpresion=false;
	
		if(EsSmallIntColumn(column)||EsIntColumn(column)||EsBigIntColumn(column)||EsDecimalColumn(column)) {
			esTipoColumnaParaRegularExpresion=true;
		}
		
		return esTipoColumnaParaRegularExpresion;
	}
	
	public static bool EsTipoColumnaParaRegularExpresionClaseC(ColumnSchema column)
	{
		bool esTipoColumnaParaRegularExpresion=false;
		
		esTipoColumnaParaRegularExpresion=EsTipoColumnaStringParaTrimClaseC(column);
		/*
		string param =  column.NativeType;
		string paramType =  column.DataType.ToString();
		
		switch (column.DataType) {
			case DbType.Boolean:{			
				break;
				
			} case DbType.Binary: {				
				break;
				
			} case DbType.DateTime:	{
				break;
				
			} case DbType.AnsiString: {
				esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.Int32:case DbType.UInt32: {
				//esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.Int64:case DbType.UInt64: {
				//esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.Int16:case DbType.UInt16: {
				//esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.AnsiStringFixedLength: {
				esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.StringFixedLength: {
				esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.String: {
				esTipoColumnaParaRegularExpresion=true;
				break;
				
			} case DbType.Decimal:case DbType.Double: {
				//esTipoColumnaParaRegularExpresion=true;
				break;
				
			} default: {
				esTipoColumnaParaRegularExpresion=false;
				break;
			}
		}
		*/
		
		return esTipoColumnaParaRegularExpresion;
	}
	
	public String GetTipoColumnaRegularExpresionClaseC(ColumnSchema column)
	{
		String strTipoColumnaRegularExpresion="SREGEXTODOS";
	
		string param =  column.NativeType;
		string paramType =  column.DataType.ToString();
		
		
		String strTodos="";
		
		if(blnTieneValidacionTodo||EsPathImagenDocumentoColumn(column)) {
			strTodos="TODOS";
		}
		
		
		
		switch (column.DataType) {
			case DbType.Boolean:{			
				break;
				
			} case DbType.Binary: {				
				break;
				
			} case DbType.DateTime:	{
				break;
				
			} case DbType.AnsiString: {
				strTipoColumnaRegularExpresion="SREGEXCADENA"+strTodos;
				break;
				
			} case DbType.Int32:case DbType.UInt32: {
				strTipoColumnaRegularExpresion="SREGEXNUMEROENTERO";
				break;
				
			} case DbType.Int64:case DbType.UInt64: {
				strTipoColumnaRegularExpresion="SREGEXNUMEROENTERO";
				break;
				
			} case DbType.Int16:case DbType.UInt16: {
				strTipoColumnaRegularExpresion="SREGEXNUMEROENTERO";
				break;
				
			} case DbType.AnsiStringFixedLength: {
				strTipoColumnaRegularExpresion="SREGEXCADENA"+strTodos;
				break;
				
			} case DbType.StringFixedLength: {
				strTipoColumnaRegularExpresion="SREGEXCADENA"+strTodos;
				break;
				
			} case DbType.String: {
				strTipoColumnaRegularExpresion="SREGEXCADENA"+strTodos;
				break;
				
			} case DbType.Decimal:case DbType.Double: {
				strTipoColumnaRegularExpresion="SREGEXNUMERODOUBLE";
				break;
				
			} default: {
				strTipoColumnaRegularExpresion="SREGEXTODOS";
				break;
			}
		}
		
		return strTipoColumnaRegularExpresion;
	}
	
	public String GetTipoColumnaMensajeRegularExpresionClaseC(ColumnSchema column)
	{
		String strTipoColumnaRegularExpresion="SVALIDACIONTODOS";
	
		string param =  column.NativeType;
		string paramType =  column.DataType.ToString();
		
		switch (column.DataType) {
			case DbType.Boolean:{			
				break;
				
			} case DbType.Binary: {				
				break;
				
			} case DbType.DateTime:	{
				break;
				
			} case DbType.AnsiString: {
				strTipoColumnaRegularExpresion="SVALIDACIONCADENA";
				break;
				
			} case DbType.Int32:case DbType.UInt32: {
				strTipoColumnaRegularExpresion="SVALIDACIONNUMEROENTERO";
				break;
				
			} case DbType.Int64:case DbType.UInt64: {
				strTipoColumnaRegularExpresion="SVALIDACIONNUMEROENTERO";
				break;
				
			} case DbType.Int16:case DbType.UInt16: {
				strTipoColumnaRegularExpresion="SVALIDACIONNUMEROENTERO";
				break;
				
			} case DbType.AnsiStringFixedLength: {
				strTipoColumnaRegularExpresion="SVALIDACIONCADENA";
				break;
				
			} case DbType.StringFixedLength: {
				strTipoColumnaRegularExpresion="SVALIDACIONCADENA";
				break;
				
			} case DbType.String: {
				strTipoColumnaRegularExpresion="SVALIDACIONCADENA";
				break;
				
			} case DbType.Decimal:case DbType.Double: {
				strTipoColumnaRegularExpresion="SVALIDACIONNUMERODOUBLE";
				break;
				
			} default: {
				strTipoColumnaRegularExpresion="SVALIDACIONTODOS";
				break;
			}
		}
		
		if(EsPathImagenDocumentoColumn(column)) {
			strTipoColumnaRegularExpresion="SVALIDACIONTODOS";
		}
		
		return strTipoColumnaRegularExpresion;
	}
	
	public static string GetTipoColumnaClaseC(ColumnSchema column)
	{
	string tipoColumna =  GetTipoColumnaFromColumn(column);
	
	if(tipoColumna!="")
	{
		return tipoColumna;
	}
	string param =  column.NativeType;
	string paramType =  column.DataType.ToString();
	
	switch (column.DataType)
	{
		case DbType.Boolean:
		{
			param =  "Boolean";
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  "Date";
			}
			else
			{
				param =  "byte []";
			}
			
			break;
		}
		case DbType.DateTime:
		{
			param =  "String";
			break;
		}
		case DbType.AnsiString:
		{
			param =  "String";
			break;
		}
		case DbType.Int32:
		{
			param =  "Integer";
			break;
		}
		case DbType.Int64:
		{
			param =  "Long";
			break;
		}
		case DbType.Int16:
		{
			param =  "Short";
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  "String";
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  "String";
			break;
		}
		
		
		case DbType.String:
		{
			param =  "String";
			break;
		}
		
		case DbType.Decimal:
		{
			param =  "Double";
			break;
		}
		
		case DbType.Double:
		{
			param =  "Double";
			break;
		}
		case DbType.Currency:
		{
			param =  "Double";
			break;
		}
		default:
		{
			param =  "None:"+paramType ;
			break;
		}

	}
	
	return param;
	}
	
	public static string GetTipoColumnaStoreProcedureC(ColumnSchema column)
	{
	string tipoColumna =  GetTipoColumnaStoreProcedureFromColumn(column);
	//System.Windows.Forms.MessageBox.Show(tipoColumna);
	if(tipoColumna!="")
	{
		return tipoColumna;
	}
		
	string param =  column.NativeType;
	string paramType =  column.DataType.ToString();
	
	switch (column.DataType)
	{
		case DbType.Boolean:
		{
			param = "SMALLINT"; //"tinyint(4)";
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  "TIMESTAMP";//"timestamp";
			}
			else
			{
				param =  "BLOB("+column.Size+")";//"binary("+column.Size+")";
			}
			
			break;
		}
		case DbType.DateTime:
		{
			param =  "TIMESTAMP";//"datetime";
			break;
		}
		case DbType.AnsiString:
		{
			param =  "VARCHAR("+column.Size+")";// "varchar("+column.Size+")";
			break;
		}
		case DbType.Int32:case DbType.UInt32:
		{
			param =  "INTEGER";//"int("+column.Size+")";
			break;
		}
		case DbType.Int64:case DbType.UInt64:
		{
			param ="BIGINT";//  "bigint("+column.Size+")";
			break;
		}
		case DbType.Int16:case DbType.UInt16:
		{
			param =  "SMALLINT";//"int("+column.Size+")";
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  "CHAR("+column.Size+")";//"char("+column.Size+")";
			break;
		}
		case DbType.StringFixedLength:
		{
			param = "CHAR("+column.Size+")";// "char("+column.Size+")";
			break;
		}
		
		
		case DbType.String:
		{
			param =  "VARCHAR("+column.Size+")";//"varchar("+column.Size+")";
			break;
		}
		
		case DbType.Decimal:case DbType.Double:
		{
			param =  "DECIMAL("+column.Size+","+column.Scale+")";//"decimal("+column.Size+","+column.Precision+")";
			break;
		}
		default:
		{
			param =  "NONE:"+paramType ;
			break;
		}

	}
	
	return param;
	}
	
	public static int GetWidthXmlCabeceraReporteColumnaClaseC(ColumnSchema column)
	{
		int intWidth=0;
		
		if(!column.IsForeignKeyMember)
		{
			if(column.DataType==DbType.AnsiString ||column.DataType==DbType.AnsiStringFixedLength ||column.DataType==DbType.String||column.DataType==DbType.StringFixedLength)
			{
				intWidth=100;
			}
			else if(column.DataType==DbType.Boolean)
			{
				intWidth=50;
			}
			else if(column.DataType==DbType.Decimal||column.DataType==DbType.Double||column.DataType==DbType.Int16||column.DataType==DbType.Int32||column.DataType==DbType.Int64||column.DataType==DbType.Single||column.DataType==DbType.UInt16||column.DataType==DbType.UInt32||column.DataType==DbType.UInt64)
			{
				intWidth=50;
			}
			else if(column.DataType==DbType.Date||column.DataType==DbType.DateTime)
			{
				intWidth=50;
			}
		}
		else
		{
			intWidth=100;
		}
		
		return intWidth;
	}
	
	public static string GetTipoXmlCabeceraReporteColumnaClaseC(ColumnSchema column,int totalWidth,int intSobrante,bool blnNormalOrientation,int contador)
	{					
	
	if(!blnNormalOrientation)
	{
		if(totalWidth>782)
		{
			return "";
		}
	}
		
	string param =  column.NativeType;
	string paramType =  column.DataType.ToString();
	int intWidth=0;
	
	intWidth=GetWidthXmlCabeceraReporteColumnaClaseC(column);
	
	int x=totalWidth;
	
	if(totalWidth!=0)
	{
		if(contador>1)
		{
			x+=intSobrante*contador;	
		}
		else
		{
			x+=intSobrante;	
		}
		
	}
	
	intWidth+=intSobrante;
	
	String strNombre = GetWebNombreTituloColumnFromPropertiesC(column);
	
				param="\r\n\t\t\t\t<staticText>";
				param+="\r\n\t\t\t\t\t<reportElement";
				param+="\r\n\t\t\t\t\t\tx=\""+x.ToString()+"\"";
				param+="\r\n\t\t\t\t\t\ty=\"1\"";
				param+="\r\n\t\t\t\t\t\twidth=\""+intWidth.ToString()+"\"";
				param+="\r\n\t\t\t\t\t\theight=\"15\"";
				param+="\r\n\t\t\t\t\t\tforecolor=\"#FFFFFF\"";
				param+="\r\n\t\t\t\t\t\tkey=\"element-90\"/>";
				param+="\r\n\t\t\t\t\t<box leftPadding=\"2\" rightPadding=\"2\">					<topPen lineWidth=\"0.0\" lineStyle=\"Solid\" lineColor=\"#000000\"/>";
				param+="\r\n\t\t\t\t\t<leftPen lineWidth=\"0.0\" lineStyle=\"Solid\" lineColor=\"#000000\"/>";
				param+="\r\n\t\t\t\t\t<bottomPen lineWidth=\"0.0\" lineColor=\"#000000\"/>";
				param+="\r\n\t\t\t\t\t<rightPen lineWidth=\"0.0\" lineStyle=\"Solid\" lineColor=\"#000000\"/>";
				param+="\r\n\t\t\t\t\t</box>";
				param+="\r\n\t\t\t\t\t<textElement>";
				param+="\r\n\t\t\t\t\t\t<font/>";
				param+="\r\n\t\t\t\t\t</textElement>";
				param+="\r\n\t\t\t\t\t<text><![CDATA["+strNombre+"]]></text>";
				param+="\r\n\t\t\t\t</staticText>";
				
	
	return param;
	}
		
	public static string GetTipoXmlReporteColumnaClaseC(ColumnSchema column,int totalWidth,int intSobrante,bool blnNormalOrientation,bool GenerarRelacionesMaestro,int contador)
	{					
	
		int y=1;
		
		if(GenerarRelacionesMaestro)
		{
			y+=intDesplazamientoReporteMaestro;
		}
	
	String strWidthLine="535";
	
	string strPrefijo=String.Empty;
		string strPrefijoTabla=GetPrefijoTablaC().ToLower();
		string strPrefijoTipo =GetPrefijoTipoC(column);
	
		strPrefijo=strPrefijoTabla+strPrefijoTipo;
		
		string strNombre = GetNombreColumnaClaseC(column);
		strPrefijo+=strNombre;
		
	if(!blnNormalOrientation)
	{
		if(totalWidth>782)
		{
			return "";
		}
		else
		{
			strWidthLine="782";
		}
	}
	
	string param =  column.NativeType;
	string paramType =  column.DataType.ToString();
	
	int intWidth=0;
	
	intWidth=GetWidthXmlCabeceraReporteColumnaClaseC(column);
	
	int x=totalWidth;
	
	if(totalWidth!=0)
	{
		if(contador>1)
		{
			x+=intSobrante*contador;	
		}
		else
		{
			x+=intSobrante;	
		}
		
	}
	
	intWidth+=intSobrante;
	
	//String strNombre = GetNombreColumnaClaseC(column);
				
				param="\r\n\t\t\t\t<line direction=\"TopDown\">";
				param+="\r\n\t\t\t\t\t<reportElement";
				param+="\r\n\t\t\t\t\t\tx=\"0\"";
				param+="\r\n\t\t\t\t\t\ty=\"17\"";
				param+="\r\n\t\t\t\t\t\twidth=\""+strWidthLine+"\"";
				param+="\r\n\t\t\t\t\t\theight=\"0\"";
				
				if(!GenerarRelacionesMaestro)
				{
					param+="\r\n\t\t\t\t\t\tforecolor=\"#808080\"";
				}
				else
				{
					param+="\r\n\t\t\t\t\t\tforecolor=\"#FFFFFF\"";
				}
				
				param+="\r\n\t\t\t\t\t\tkey=\"line\"";
				param+="\r\n\t\t\t\t\t\tpositionType=\"FixRelativeToBottom\"/>";
				param+="\r\n\t\t\t\t\t<graphicElement stretchType=\"NoStretch\">";
				param+="\r\n\t\t\t\t\t<pen lineWidth=\"0.25\" lineStyle=\"Solid\"/>";
				param+="\r\n\t\t\t\t\t</graphicElement>";
				param+="\r\n\t\t\t\t</line>";
			
			
				param+="\r\n\t\t\t\t<textField isStretchWithOverflow=\"false\" isBlankWhenNull=\"false\" evaluationTime=\"Now\" hyperlinkType=\"None\"  hyperlinkTarget=\"Self\" >";
				param+="\r\n\t\t\t\t\t<reportElement";
				param+="\r\n\t\t\t\t\t\tx=\""+x.ToString()+"\"";
				param+="\r\n\t\t\t\t\t\ty=\""+y.ToString()+"\"";
				param+="\r\n\t\t\t\t\t\twidth=\""+intWidth.ToString()+"\"";
				param+="\r\n\t\t\t\t\t\theight=\"15\"";
				param+="\r\n\t\t\t\t\t\tkey=\"textField\"/>";
				param+="\r\n\t\t\t\t\t<box topBorder=\"None\" topBorderColor=\"#000000\" leftBorder=\"None\" leftBorderColor=\"#000000\" rightBorder=\"None\" rightBorderColor=\"#000000\" bottomBorder=\"None\" bottomBorderColor=\"#000000\"/>";
				param+="\r\n\t\t\t\t\t<textElement>";
				param+="\r\n\t\t\t\t\t\t<font/>";
				param+="\r\n\t\t\t\t\t</textElement>";
				
				String strTipo="java.lang.String";
				
				if(column.DataType==DbType.Boolean||column.IsForeignKeyMember)
				{
					param+="\r\n\t\t\t\t<textFieldExpression   class=\""+strTipo +"\"><![CDATA[$F{"+strPrefijo+strClaseDetalleBean+"}]]></textFieldExpression>";
					
				}
				else
				{
					param+="\r\n\t\t\t\t<textFieldExpression   class=\""+GetTipoReporteColumnaClaseC(column) +"\"><![CDATA[$F{"+strPrefijo+"}]]></textFieldExpression>";
				}
				
				
				
				
				param+="\r\n\t\t\t\t</textField>";
				
	
	return param;
	}
	
	public static string GetTipoReporteColumnaClaseC(ColumnSchema column)
	{
	string tipoColumna =  GetTipoReporteColumnaFromColumn(column);
	
	if(tipoColumna!="")
	{
		return tipoColumna;
	}
		
	string param =  column.NativeType;
	string paramType =  column.DataType.ToString();
	
	switch (column.DataType)
	{
		case DbType.Boolean:
		{
			param =  "java.lang.Boolean";
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  "java.lang.Timestamp";
			}
			else
			{
				param =  "byte []";
			}
			
			break;
		}
		case DbType.DateTime:
		{
			param =  "java.lang.String";
			break;
		}
		case DbType.AnsiString:
		{
			param =  "java.lang.String";
			break;
		}
		case DbType.Int32:case DbType.UInt32:
		{
			param =  "java.lang.Integer";
			break;
		}
		case DbType.Int64:case DbType.UInt64:
		{
			param =  "java.lang.Long";
			break;
		}
		case DbType.Int16:case DbType.UInt16:
		{
			param =  "java.lang.Short";
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  "java.lang.String";
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  "java.lang.String";
			break;
		}
		
		
		case DbType.String:
		{
			param =  "java.lang.String";
			break;
		}
		
		case DbType.Decimal:case DbType.Double:
		{
			param =  "java.lang.Double";
			break;
		}
		default:
		{
			param =  "None:"+paramType ;
			break;
		}

	}
	
	return param;
	}
	
	public static string GetTipoReporteColumnaFromColumn(ColumnSchema column)
	{
	String nombreClase="";
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPOREPORTE"))
					{
						nombreClase=tipo[1];
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return nombreClase;
	}
	
	public static string GetTipoColumnaClaseGetResulsetC(ColumnSchema column)
	{
		string tipoColumna =  GetTipoResultSetColumnaFromColumn(column);
	
	if(tipoColumna!="")
	{
		return tipoColumna;
	}
string param =  column.NativeType;
	
	switch (column.DataType)
	{
		case DbType.DateTime:
		{
			param =  "String";
			break;
		}
		case DbType.AnsiString:
		{
			param =  "String";
			break;
		}
		case DbType.Boolean:
		{
			param =  "Boolean";
			break;
		}
		case DbType.Int32:case DbType.UInt32:
		{
			param =  "Int";
			break;
		}
		case DbType.Int64:case DbType.UInt64:
		{
			param =  "Long";
			break;
		}
		case DbType.Int16:case DbType.UInt16:
		{
			param =  "Short";
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  "String";
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  "String";
			break;
		}
		case DbType.Decimal:case DbType.Double:case DbType.Currency:
		{
			param =  "Double";
			break;
		}
		
		case DbType.String:
		{
			param =  "String";
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  "String";
			}
			else
			{
				param =  "Bytes";
			}
			
			break;
		}
		default:
		{
			param =  "None";
			break;
		}

	}
	
	return param;
	}
	
	public static string GetTipoControlColumnaClaseC(ColumnSchema column)
	{
string param =  column.NativeType;
	
	switch (column.DataType)
	{
		case DbType.DateTime:
		{
			param =  "dtp";
			break;
		}
		case DbType.Boolean:
		{
			param =  "chk";
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
			param =  "hdn";
			}
			else
			{
			param =  "chk";
			}
			break;
		}
		case DbType.AnsiString:
		{
			param =  "txt";
			break;
		}
		case DbType.Int32:
		{
			param =  "ddl";
			break;
		}
		case DbType.Int64:
		{
			param =  "txt";
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  "txt";
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  "txt";
			break;
		}		
		case DbType.String:
		{
			param =  "txt";
			break;
		}
		default:
		{
			param =  "NONE";
			break;
		}
	}	
	return param;
	}
	
	public static string GetTipoColumnaClaseEnumC(ColumnSchema column)
	{
string param =  column.NativeType;
	
	switch (column.DataType)
	{
		case DbType.DateTime:
		{
			param =  "TIMESTAMP";
			break;
		}
		case DbType.Date:
		{
			param =  "DATE";
			break;
		}
		case DbType.Time:
		{
			param =  "TIME";
			break;
		}
		case DbType.Boolean:
		{
			param =  "BOOLEAN";
			break;
		}
		case DbType.Binary:
		{
			param =  "BYTES";
			break;
		}
		case DbType.AnsiString:
		{
			param =  "STRING";
			break;
		}
		case DbType.Int16:
		{
			param =  "SHORT";
			break;
		}
		case DbType.Int32:
		{
			param =  "INT";
			break;
		}		
		case DbType.Int64:
		{
			param =  "LONG";
			break;
		}
		case DbType.AnsiStringFixedLength:
		{
			param =  "STRING";
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  "STRING";
			break;
		}
		
		case DbType.Decimal:
		{
			param =  "DOUBLE";
			break;
		}
		case DbType.String:
		{
			param =  "STRING";
			break;
		}
		default:
		{
			param =  "NONE";
			break;
		}

	}
	
	param=GetTipoColumnaEnumFromColumn(column,param);
	
	return param;
	}
	
	public static string GetTipoColumnaEnumFromColumn(ColumnSchema column,String strEnum)
	{
	String nombreClase="";
	String nombreEnum=strEnum;
	String[] descripciones;
	String[] tipo;
				
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("TIPO"))
					{
						
						nombreClase=tipo[1];
						
						if(nombreClase.Equals("Date"))
						{
							strEnum="DATE";
						}
						else if(nombreClase.Equals("Timestamp"))
						{
							strEnum="TIMESTAMP";
						}
						break;
					}
				}
				break;
			}
					
		}
			
	
	
	return strEnum;
	}
	
	public static string GetTipoColumnaToString(ColumnSchema column)
	{
		string tipoColumna =  GetToStringFromTipoColumnaFromColumn(column);
	
		if(tipoColumna!="")
		{
			return tipoColumna;
		}
	
string param = ".toString()";
	
	switch (column.DataType)
	{
		case DbType.DateTime:
		{
			param = string.Empty;
			break;
		}
		case DbType.AnsiString:
		{
			param =   string.Empty;
			break;
		}
		
		
		case DbType.AnsiStringFixedLength:
		{
			param =   string.Empty;
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  string.Empty;
			break;
		}
		
		
		case DbType.String:
		{
			param =   string.Empty;
			break;
		}
		/*default:
		{
			param =  "None";
			break;
		}
		*/
	}
	
	return param;
	}
	
	public static string GetTipoColumnaParse(ColumnSchema column,String data)
	{
		string param = string.Empty;		
		string paramInit = string.Empty;
		string paramEnd = string.Empty;;
		string tipoColumna =  GetTipoParseColumnaFromColumn(column,data);
	
	if(tipoColumna!="")
	{
		if(tipoColumna=="Date") {
			paramInit =  "Funciones.ConvertToDate(";
			paramEnd=")";
			param=paramInit+data+paramEnd;
		} else {
			param=tipoColumna+".valueOf("+data+")";
		}
		
		return param;
	}


	switch (column.DataType)
	{
		case DbType.Boolean:
		{
			paramInit =  "Boolean.parseBoolean(";
			paramEnd=")";
			param=paramInit+data+paramEnd;
			break;
		}
		case DbType.Int32:
		{
			paramInit =  "Integer.parseInt(";
			paramEnd=")";
			param=paramInit+data+paramEnd;
			break;
		}
		case DbType.Int64:
		{
			paramInit =  "Long.parseLong(";
			paramEnd=")";
			param=paramInit+data+paramEnd;
			break;
		}
		case DbType.Int16:
		{
			paramInit =  "Short.parseShort(";
			paramEnd=")";
			param=paramInit+data+paramEnd;
			break;
		}
		case DbType.Decimal:
		{
			paramInit =  "Double.parseDouble(";
			paramEnd=")";
			param=paramInit+data+paramEnd;
			break;
		}
		case DbType.DateTime:
		{
			param =   string.Empty;
			break;
		}
		case DbType.AnsiString:
		{
			param =   string.Empty;
			break;
		}
		
		
		case DbType.AnsiStringFixedLength:
		{
			param =   string.Empty;
			break;
		}
		case DbType.StringFixedLength:
		{
			param =  string.Empty;
			break;
		}
		
		
		case DbType.String:
		{
			param =   string.Empty;
			break;
		}
		case DbType.Binary:
		{
			if(column.Name!=strVersionRow)
			{
				param =  "None";
			}
			else
			{
				paramInit =  "Funciones.ConvertToDate(";
				paramEnd=")";
				param=paramInit+data+paramEnd;
			}
			break;
		}
		default:
		{
			param =  "None";
			break;
		}

	}
	if(param==string.Empty)
	{
		param=data;
	}
	else if(param=="None")
	{
		param="null";
	}
	return param;
	}
	
	public static string GetDefaultValueColumna(ColumnSchema column)
	{
string param = string.Empty;		
	
		if(column.AllowDBNull)
		{
			return "null";
		}
		
 String[] descripciones;
	String[] tipo;
	String strDefault="";
	
		foreach(ExtendedProperty extendedProperty in column.ExtendedProperties)
		{
			if(extendedProperty.Name=="CS_Description")
			{
				descripciones=extendedProperty.Value.ToString().Split('|');
				
				foreach(String descripcion in descripciones)
				{
					tipo=descripcion.Split('=');
					
					if(tipo[0].Equals("DEFAULT"))
					{
						strDefault=tipo[1];
						break;
					}
				}
				break;
			}
					
		}

	if(strDefault!="")
	{
		return strDefault;
	}
	
	
	switch (column.DataType)
	{
		case DbType.Int32:
		{
			
			param="-1";
			break;
		}
		case DbType.Int64:
		{
			param="-1L";
			break;
		}
		case DbType.Int16:
		{
			param="-1";
			break;
		}
		case DbType.Boolean:
		{
			param="false";
			break;
		}
		case DbType.DateTime:
		{
			param="null";
			break;
		}
		
		case DbType.Decimal:
		{
			param="-1.1";
			break;
		}
		
		case DbType.Double:
		{
			param="-1.1";
			break;
		}
		
		case DbType.Currency:
		{
			param="-1.1";
			break;
		}
		
		case DbType.AnsiString:
		{
			param ="\"\"";
			break;
		}
		
		
		case DbType.AnsiStringFixedLength:
		{
			param ="\"\"";
			break;
		}
		case DbType.StringFixedLength:
		{
			param ="\"\"";
			break;
		}
		
		
		case DbType.String:
		{
			param ="\"\"";
			break;
		}
		case DbType.Binary:
		{
			if(column.Name==strVersionRow)
			{
				param =  "\"\"";
			}
			else
			{
				param =  "null";
			}
			
			break;
		}
		default:
		{
			param =  "None:"+column.DataType;
			break;
		}

	}
	
	return param;
	}
	
	public static string GetNombreCompletoColumnaClaseC(ColumnSchema column)
{
	if(column.Name==strId)
	{
		return strIdGetSet;
	}
	else if(column.Name==strIsActive)
	{
		return strIsActive;
	}
	else if(column.Name==strIsExpired)
	{
		return strIsExpired;
	}
	else if(column.Name==strVersionRow)
	{
		return strVersionRowGetSet;
	}
	
	string strPrefijo =GetPrefijoTablaC()+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column);
	
	
	return strPrefijo;
}
	
	public static string GetNombreCompletoLowerColumnaClaseC(ColumnSchema column)
{
	if(column.Name==strId)
	{
		return strIdGetSet;
	}
	else if(column.Name==strIsActive)
	{
		return strIsActiveGetSet;
	}
	else if(column.Name==strIsExpired)
	{
		return strIsExpiredGetSet;
	}
	else if(column.Name==strVersionRow)
	{
		return strVersionRowGetSet;
	}
	
	string strPrefijo =GetPrefijoTablaC().ToLower()+GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column);
	
	
	return strPrefijo;
}

	public static string GetNombreCompletoColumnaClaseSinPrefijoTablaC(ColumnSchema column)
{
	string strPrefijo =GetPrefijoTipoC(column)+GetNombreColumnaClaseC(column);
	
	
	return strPrefijo;
}
	/*	
	public static string GetNombreCampoTablaC(ColumnSchema column)
	{
	string strPrefijo =column.Name;
	
	
	return strPrefijo;
	}
	*/
	#region NetTiersFunctions

// [ab 012605] convenience array for checking if a datatype is an integer 
		private readonly static DbType[] aIntegerDbTypes = new DbType[] {DbType.Int16,DbType.Int32, DbType.Int64 };
		
		private string entityFormat 		= "{0}";
		private string componentServiceFormat = "{0}Service";
		private string entityDataFormat 	= "{0}EntityData";
		private string collectionFormat 	= "{0}Collection";
		private string genericListFormat 	= "TList<{0}>";
		private string genericViewFormat 	= "VList<{0}>";
		private string providerFormat 		= "{0}Provider";
		private string interfaceFormat	 	= "I{0}";
		private string baseClassFormat 		= "{0}Base";
		private string unitTestFormat		= "{0}Test";
		private string enumFormat 			= "{0}List";
		private string manyToManyFormat		= "{0}From{1}";
		private string strippedTablePrefixes		= "tbl;tbl_";
		private string customProcedureStartsWith = "_{0}_";
		private string aliasFilePath 		= "";
		private string procedurePrefix = "";
		private string auditUserField = "";
		private string auditDateField = "";
		private bool cspUseDefaultValForNonNullableTypes = false;
		private bool parseDbColDefaultVal  = false;
		private bool changeUnderscoreToPascalCase  = false;
		private bool includeCustoms = true;

		private MethodNamesProperty methodNames = null;
		private Hashtable aliases = null;
		
		#region CSharpKeywords
		
		private string[] csharpKeywords = new string[77] 
		{
				"abstract","event", "new", "struct", 
				"as", "explicit", "null", "switch",
				"base", "extern", "object", "this",
				"bool", "false", "operator", "throw",
				"break", "finally", "out", "true",
				"byte", "fixed", "override", "try",
				"case", "float", "params", "typeof",
				"catch", "for", "private", "uint",
				"char", "foreach", "protected", "ulong",
				"checked", "goto", "public", "unchecked",
				"class", "if", "readonly", "unsafe",
				"const", "implicit", "ref", "ushort",
				"continue","in","return","using",
				"decimal","int","sbyte","virtual",
				"default","interface","sealed","volatile",
				"delegate","internal","short","void",
				"do","is","sizeof","while",
				"double","lock","stackalloc",
				"else","long","static",
				"enum","namespace", "string"
		}; 
		
		#endregion 
		
		/// <summary>
		/// Return a specified number of tabs
		/// </summary>
		/// <param name="n">Number of tabs</param>
		/// <returns>n tabs</returns>
		public string Tab(int n)
		{
			return new String('\t', n);
		}
		
		#region Diagnostics
		
		/// <summary>
		/// Gets or sets a value that indicates if output during generation should
		/// be verbose or not.
		/// </summary>
		protected bool Verbose { get { return verbose; } set { verbose = value; } }
		private bool verbose = false;

		
		/// <summary>
		/// Write a message to the debug log.
		/// </summary>
		protected void DebugWriteLine(string msg)
		{
			if (Verbose && msg != null && msg.Length > 0)
				System.Diagnostics.Debug.WriteLine(msg);
		}
		#endregion
		
		
		#region "9. Code Style public properties"
		MethodNamesProperty MethodNames
		{
			get
			{
				if ( methodNames == null )
				{
					methodNames = new MethodNamesProperty();
				}
				
				return methodNames;
			}
			set { methodNames = value; }
		}
		
		/// <summary>
		/// This property is used to set the MethodNames property from NetTiers.cst
		/// due to runtime error when trying to set it directly using an object value.
		/// </summary>
		string MethodNamesValues
		{
			get { return MethodNames.ToStringList(); }
			set { MethodNames = new MethodNamesProperty(value); }
		}
		
		string StrippedTablePrefixes
		{
			get {return this.strippedTablePrefixes;}
			set	{this.strippedTablePrefixes = value;}
		}
		
		string EntityFormat
		{
			get {return this.entityFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "EntityFormat");
				}
				this.entityFormat = value;
			}
		}
		
		string CollectionFormat
		{
			get {return this.collectionFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "CollectionFormat");
				}
				this.collectionFormat = value;
			}
		}
		
		string GenericViewFormat
		{
			get {return this.genericViewFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "GenericViewFormat");
				}
				this.genericViewFormat = value;
			}
		}
		
		string GenericListFormat
		{
			get {return this.genericListFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "GenericListFormat");
				}
				this.genericListFormat = value;
			}
		}
		
		
		
		string ProviderFormat
		{
			get {return this.providerFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "ProviderFormat");
				}
				this.providerFormat = value;
			}
		}
		
		string InterfaceFormat
		{
			get {return this.interfaceFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "InterfaceFormat");
				}
				this.interfaceFormat = value;
			}
		}
		
		string BaseClassFormat
		{
			get {return this.baseClassFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "BaseClassFormat");
				}
				this.baseClassFormat = value;
			}
		}
		
		string EnumFormat
		{
			get {return this.enumFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "EnumFormat");
				}
				this.enumFormat = value;
			}
		}
		
			string ManyToManyFormat
		{
			get {return this.manyToManyFormat;}
			set
			{
				if (value.IndexOf("{0}") == -1) 
				{
					throw new ArgumentException("This parameter must contains the pattern {0} to be valid.", "ManyToManyFormat");
				}
				this.manyToManyFormat = value;
			}
		}
				
		bool ParseDbColDefaultVal
		{
			get { return this.parseDbColDefaultVal; }
			set { this.parseDbColDefaultVal = value; }
		}
		
	bool ChangeUnderscoreToPascalCase
		{
			get { return this.changeUnderscoreToPascalCase; }
				set { this.changeUnderscoreToPascalCase = value; }
		}
		
		
				string AliasFilePath
		{
			get {return this.aliasFilePath;}
			set	{this.aliasFilePath = value;}
		}
		
		string ProcedurePrefix
		{
			get {return this.procedurePrefix;}
			set
			{
				if (value == null || value == string.Empty)
					return;
				this.procedurePrefix = value;
			}
		}

		string CustomProcedureStartsWith
		{
			get { return this.customProcedureStartsWith; }
			set { this.customProcedureStartsWith = value; }
		}
		
		bool IncludeCustoms
		{
			get { return this.includeCustoms; }
			set { this.includeCustoms = value; }
		}		
		
			bool CSPUseDefaultValForNonNullableTypes
		{
			get { return this.cspUseDefaultValForNonNullableTypes; }
			set { this.cspUseDefaultValForNonNullableTypes = value; }
		}
		
		public enum CustomNonMatchingReturnType
		{
			DataSet,
			IDataReader
		}
		#endregion

		/// <summary>
		/// Get the safe name for a data object by determining if it contains spaces or other illegal
		/// characters - if so wrap with []
		/// </summary>
		/// <param name="schemaObject">Database schema object (e.g. a table, stored proc, etc)</param>
		/// <returns>The safe name of the object</returns>
		public string GetSafeName(SchemaObjectBase schemaObject)
		{
			return GetSafeName(schemaObject.Name);
		}

		/// <summary>
		/// Get the safe name for a data object by determining if it contains spaces or other illegal
		/// characters - if so wrap with []
		/// </summary>
		/// <param name="objectName">The name of the database schema object</param>
		/// <returns>The safe name of the object</returns>
		public string GetSafeName(string objectName)
		{
			return objectName.IndexOfAny(new char[]{' ', '@', '-', ',', '!'}) > -1 ? "[" + objectName + "]" : objectName;
		}

		/// <summary>
		/// Get the camel cased version of a name.  
		/// If the name is all upper case, change it to all lower case
		/// </summary>
		/// <param name="name">Name to be changed</param>
		/// <returns>CamelCased version of the name</returns>
        public string GetCamelCaseName(string name)
        {
            if (name.Equals(name.ToUpper()) && name.IndexOf("_") == -1)
                return name.ToLower().Replace(" ", "");
            else
            {
                // first get the PascalCase version of the name (DRY)
                string pascalName = GetPascalCaseName(name);
                // now lowercase the first character to transform it to camelCase
                return pascalName.Substring(0, 1).ToLower() + pascalName.Substring(1);
            }
        }

        /// <summary>
        /// Get the Pascal cased version of a name.  
        /// </summary>
        /// <param name="name">Name to be changed</param>
        /// <returns>PascalCased version of the name</returns>
        public string GetPascalCaseName(string name)
        {
			string[] splitNames;
			if (ChangeUnderscoreToPascalCase)
			{
				char[] splitter = {'_', ' '};
				splitNames = name.Split(splitter);
			}	
			else
			{
				char[] splitter =  {' '};
				splitNames = name.Split(splitter);
			}
			
            string pascalName = "";
            foreach (string s in splitNames)
            {
                if (s.Length > 0)
                    pascalName += s.Substring(0, 1)/*.ToUpper()*/ + s.Substring(1);
            }

            return pascalName;
        }

        /// <summary>
        /// Get the Pascal spaced version of a name.  
        /// </summary>
        /// <param name="name">Name to be changed</param>
        /// <returns>PascalSpaced version of the name</returns>
        public string PascalToSpaced(string name)
        {
            Regex regex = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return regex.Replace(name, " ${x}");
        }

        /// <summary>
        /// Get the Pascal spaced version of a name.  
        /// </summary>
        /// <param name="name">Name to be changed</param>
        /// <returns>PascalSpaced version of the name</returns>
        public string GetPascalSpacedName(string name)
        {
            return PascalToSpaced(GetClassName(name));
        }		

		/// <summary>
		/// Remove any non-word characters from a SchemaObject's name (word characters are a-z, A-Z, 0-9, _)
		/// so that it may be used in code
		/// </summary>
		/// <param name="schemaObject">DB Object whose name is to be cleaned</param>
		/// <returns>Cleaned up object name</returns>
		public string GetCleanName(SchemaObjectBase schemaObject)
		{
			return GetCleanName(schemaObject.Name);
		}
		
		
		/// <summary>
		/// Applies the configured string format to the table module
		/// </summary>
		private string ApplyBaseClassFormat(string className)
		{
			return string.Format(baseClassFormat, className);
		}
		
		#region Business object class name
		/// <summary>
		/// Gets the abstract class name of a table.
		/// </summary>
		public string GetAbstractClassName(string tableName)
		{
			return ApplyBaseClassFormat(GetClassName(tableName));
		}
		
		/// <summary>
		/// Get the name of the IEntityKey implementation for the specified table.
		/// </summary>
		public string GetKeyClassName(string tableName)

		{
			return String.Format("{0}Key", GetClassName(tableName));
		}
		
		/// <summary>
		/// Get a partial class name from a standard class name.
		/// </summary>
		/// <param name="className">The normal class name.</param>
		public string GetPartialClassName(string className)
		{
			return string.Format("{0}.generated", className);
		}
		
		
		/// <summary>
		/// Get a service based class name from a standard class name.
		/// </summary>
		/// <param name="className">The normal class name.</param>
		public string GetServiceClassName(string className)
		{
			return string.Format("{0}Service", GetClassName(className));
		}

		/// <summary>
		/// Get a partial class name from a standard class name.
		/// </summary>
		/// <param name="className">The normal class name.</param>
		public string GetAbstractServiceClassName(string className)
		{
			return string.Format("{0}ServiceBase", GetClassName(className));
		}
		
		/// <summary>
		/// Get the proxy class name of the Data Repository.
		/// </summary>
		/// <param name="className">The normal class name.</param>
		public string GetProxyClassName(string className)
		{
			return string.Format("{0}Services", className);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string GetEnumName(string tableName)
		{
			return string.Format(this.enumFormat, GetClassName(tableName));
		}
				
		/// <summary>
		/// 
		/// </summary>
		public string GetStructName(string tableName)
		{
			return string.Format(this.entityDataFormat, GetClassName(tableName));
		}
				
		
		/// <summary>
		/// This function get the alias name for this object name.
		/// </summary>
		/// <remark>This function should not be called directly, but via the GetClassName.</remark>
		public string GetAliasName(string tableName)
		{
			tableName = GetCleanName(tableName);
			
			// If the aliases aren't loaded yet, and the filepath exists, then load the hashtable of aliases.
			if (aliases == null && File.Exists(this.aliasFilePath))
			{				
				//Debugger.Break();
				aliases = new Hashtable();
				using (StreamReader sr = new StreamReader(this.aliasFilePath))
				{
					string line;
					while ((line = sr.ReadLine()) != null)	
					{
						if (line.IndexOf(":") > 0)
						{
							aliases.Add(line.Split(':')[0], (line.Split(':')[1]));
						}
					}
				}
			}
			
			// See if our tablename is in the aliases hashtable, and if so, replace it.
			if (aliases != null)
			{
				//Debugger.Break();
				IDictionaryEnumerator alias = aliases.GetEnumerator();
				while (alias.MoveNext())
				{
					if (tableName.ToLower() == alias.Key.ToString().ToLower())
					{
						tableName = alias.Value.ToString();
						break;
					}
				}
			}
			return tableName;
		}
				
		/// <summary>
		///  Create a class name from a table name, for a business object.
		/// Is an alias file is present, use the defined mapping.
		/// Otherwise, use the cleaned table name.
		/// </summary>
		public string GetClassName(TableSchema tableName)
		{
			return GetClassName(tableName.Name);
		}
		
		/// <summary>
		///  Create a class name from a table name, for a business object.
		/// Is an alias file is present, use the defined mapping.
		/// Otherwise, use the cleaned table name.
		/// </summary>
		public string GetClassName(string tableName)
		{
			
			if (File.Exists(this.aliasFilePath))
			{			
				//See newName there is any alias for this table name
				string tableAlias = GetAliasName(tableName);
				// see if the alias and original table name are the different
				if ( string.Compare(tableName, tableAlias, true) != 0 )
					return tableAlias;

				// ok, just fall thru and allow normal stripping of prefixes
			}
						
			
			// Otherwise just use the old good method ;-) (strip prefix, remove bad char, Pascal case)
			
			// 1. strip prefix
			string newName = tableName;
			
			string[] strips = this.strippedTablePrefixes.Split(new char[] {',', ';'});
			foreach(string strip in strips)
			{
            if (newName.StartsWith(strip))
				{
					newName = newName.Remove(0, strip.Length);
					continue;
				}
			}
			
			// 2.remove space or bad characters
			newName = GetCleanName(string.Format(this.entityFormat, newName));
			
			if (Regex.IsMatch(newName, @"^[\d]"))
				newName="Entity" + newName;
				
			// 3. Set Pascal case
			return GetPascalCaseName(newName);
			
			/*
			// 3. Remove any plural - Experimental, need more grammar analysis//ref: http://www.gsu.edu/~wwwesl/egw/crump.htm
			ArrayList invariants = new ArrayList();
			invariants.Add("alias");
							
			if (invariants.Contains(name.ToLower()))
			{
				return name;
			}
			else if (name.EndsWith("ies"))
			{
				return name.Substring(0, name.Length-3) + "y";
			}
			else if (name.EndsWith("s") && !(name.EndsWith("ss") || name.EndsWith("us")))
			{
				return name.Substring(0, name.Length-1);
			}
			else
				return name;	
			*/		
		}		
		#endregion
		
		
		#region collection class name
		/// <summary>
		/// 
		/// </summary>
		public string GetAbstractCollectionClassName(string tableName)
		{
			return ApplyBaseClassFormat(GetCollectionClassName(tableName));
		}
		/// <summary>
		/// 
		/// </summary>
		public string GetCollectionClassName(string tableName)
		{
			return string.Format(genericListFormat, GetClassName(tableName));
		}
		
		public string GetViewCollectionClassName(string tableName)
		{
			return string.Format(genericViewFormat, GetClassName(tableName));
		}
		
		public string GetCollectionPropertyName(string tableName)
		{
			return string.Format(collectionFormat, GetClassName(tableName));
		}
		
		#endregion

		#region Provider class name
		/// <summary>
		/// 
		/// </summary>
		public string GetProviderName(string tableName)
		{
			return string.Format(providerFormat, GetClassName(tableName));
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string GetProviderClassName(string tableName)
		{
			return GetProviderName(tableName);
		}
		
		/*public string GetProviderDecoratorClassName(string tableName)
		{
			return string.Format(decoratorFormat, GetProviderClassName(tableName));
		}*/
		/// <summary>
		/// 
		/// </summary>
		public string GetIProviderName(string tableName)
		{
			return string.Format(interfaceFormat, GetProviderClassName(tableName));
		}
		/// <summary>
		/// 
		/// </summary>
		public string GetProviderBaseName(string tableName)
		{
			return ApplyBaseClassFormat(GetProviderClassName(tableName));
		}
		/// <summary>
		/// 
		/// </summary>
		public string GetProviderTestName(string tableName)
		{
			return string.Format(unitTestFormat, GetClassName(tableName));
		}
		#endregion
		
		#region Factory class name
				
		/// <summary>
		/// 
		/// </summary>
		public string GetAbstractRepositoryClassName(string tableName)
		{
			return ApplyBaseClassFormat(GetRepositoryClassName(tableName));
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string GetRepositoryClassName(string tableName)
		{
			return GetProviderName(tableName);
		}		
		
		/// <summary>
		/// 
		/// </summary>
		public string GetRepositoryTestClassName(string tableName)
		{
			return string.Format(unitTestFormat, GetClassName(tableName));
		}
		#endregion
		
        #region 6b - Web Advanced Options
        /// <summary>
        /// Build and return a concatened list of columns that are contained in the specified key. (ex: Column1, Column2() )
        /// </summary>
        /// <param name="keys"> the key instance.</param>
        public string GetDataKeyNames(ColumnSchemaCollection keys)
        {
            StringBuilder Name = new StringBuilder();
            for (int x = 0; x < keys.Count; x++)
            {
                Name.Append(GetPropertyName(keys[x].Name));
                if (x < keys.Count - 1)
                {
                    Name.Append(", ");
                }
            }
            return Name.ToString();
        }

        /// <summary>
        /// Returns TableSchemaCollection of tables by a fk
        /// </summary>
        /// <param name="col"></param>
        /// <param name="sourceTables"></param>
        /// <returns></returns>
        public TableSchemaCollection GetTablesCollectionByFk(ColumnSchema col, TableSchemaCollection sourceTables)
        {
            TableSchemaCollection SourceTablesRelated = new TableSchemaCollection();

            for (int x = 0; x < sourceTables.Count; x++)
            {
                TableSchema SourceTable = sourceTables[x];
                foreach (ColumnSchema tCol in SourceTable.Columns)
                {
                    if (col.Name == tCol.Name && col.SystemType == tCol.SystemType && tCol.IsForeignKeyMember && !tCol.IsPrimaryKeyMember)
                        SourceTablesRelated.Add(SourceTable);
                }
            }

            return SourceTablesRelated;
        }

        #endregion

		/// <summary>
		/// Remove any non-word characters from a name (word characters are a-z, A-Z, 0-9, _)
		/// so that it may be used in code
		/// </summary>
		/// <param name="name">name to be cleaned</param>
		/// <returns>Cleaned up object name</returns>
		public string GetCleanName(string name)
		{
			return Regex.Replace(name, @"[\W]", "");
		}
		
		/// <summary>
		/// Remove any non-word characters from a name (word characters are a-z, A-Z, 0-9, _)
		/// with the exception of a period (.)
		/// so that it may be used in code
		/// </summary>
		/// <remarks>
		///		Meant to be used to format things like namespaces and database names.
		///	</remarks>
		/// <param name="name">name to be cleaned</param>
		/// <returns>Cleaned up object name</returns>
		public string GetCleanName2(string name)
		{
			return Regex.Replace(name, @"[^A-Za-z0-9_\.]", "");
		}
		
		/// <summary>
		/// Transform the name of a column into a public class property name.
		/// </summary>
		public string GetPropertyName(ColumnSchema column)
		{
			if (column == null)
				return "";
				
		   	return GetPropertyName(column.Name);
		}
		
		/// <summary>
		/// Transform a name into a public class property name.
		/// </summary>
		public string GetPropertyName(string name)
		{
		   	name = Regex.Replace(name, @"[\W]", "");
		   	name = name.TrimStart(new char[] {'_', '-', '+', '=', '*'});
			name = GetPascalCaseName(name);
			
			if (Regex.IsMatch(name, @"^[\d]"))
				name="Data" + name;
			return name;
		}
		
		/// <summary>
		/// Gets the expression used to set the property value in an entity.  Specificly used to handle nullable columns.
		/// </summary>
		/// <param name="column">The column object </param>
		/// <param name="containerName">The object that has a string indexer for the column (DataRow, IDataReader, etc)</param>
		/// <param name="objectName">The object instance name.</param>
		/// <param name="indent">How tabs should the code be indented</param>
		/// <returns>An expression that sets the property based on contents of the column in the container.</returns>
		/// <remarks>This method should not append the trailing semicolon.</remarks>
		public string GetObjectPropertySetExpression(ColumnSchema column, string containerName, string objectName, int indent)
		{
			if ( column.AllowDBNull )
			{
				return string.Format("{2} = ({1}.IsDBNull({1}.GetOrdinal(\"{0}\")))?null:({3}){1}[\"{0}\"]",
						/*0*/column.Name,
						/*1*/containerName,
						/*2*/GetObjectPropertyAccessor(column,objectName),
						/*3*/GetCSType(column));
			}
			else
			{
				// regular NOT NULL data types, set to default value for type if null
				return string.Format("{2} = ({3}){1}[\"{0}\"]",
					/*0*/column.Name,
					/*1*/containerName,
					/*2*/GetObjectPropertyAccessor(column,objectName),
					/*3*/GetCSType(column),
					/*4*/GetCSDefaultByType(column));
			}
		}
		
		/// <summary>
		/// Gets the expression used to set the property value in an entity.  Specificly used to handle nullable columns.
		/// </summary>
		/// <param name="column">The column object </param>
		/// <param name="containerName">The object that has a string indexer for the column (DataRow, IDataReader, etc)</param>
		/// <param name="objectName">The object instance name.</param>
		/// <param name="indent">How tabs should the code be indented</param>
		/// <returns>An expression that sets the property based on contents of the column in the container.</returns>
		/// <remarks>This method should not append the trailing semicolon.</remarks>
		public string GetOriginalObjectPropertySetExpression(ColumnSchema column, string containerName, string objectName, int indent)
		{
			if ( column.AllowDBNull )
			{
				return string.Format("{2} = ({1}.IsDBNull({1}.GetOrdinal(\"{0}\")))?null:({3}){1}[\"{0}\"]",
						/*0*/column.Name,
						/*1*/containerName,
						/*2*/GetOriginalObjectPropertyAccessor(column,objectName),
						/*3*/GetCSType(column));
			}
			else
			{
				// regular NOT NULL data types, set to default value for type if null
				return string.Format("{2} = ({3}){1}[\"{0}\"]",
					/*0*/column.Name,
					/*1*/containerName,
					/*2*/GetOriginalObjectPropertyAccessor(column,objectName),
					/*3*/GetCSType(column),
					/*4*/GetCSDefaultByType(column));
			}
		}

		/// <summary>
		/// Gets the expression used to set the property value in an entity.  Specificly used to handle nullable columns.
		/// </summary>
		/// <param name="column">The column object </param>
		/// <param name="containerName">The object that has a string indexer for the column (DataRow, IDataReader, etc)</param>
		/// <param name="objectName">The object instance name.</param>
		/// <param name="indent">How tabs should the code be indented</param>
		/// <returns>An expression that sets the property based on contents of the column in the container.</returns>
		/// <remarks>This method should not append the trailing semicolon.</remarks>
		public string GetDatasetPropertySetExpression(ColumnSchema column, string containerName, string objectName, int indent)
		{
			if ( column.AllowDBNull )
			{
				return string.Format("{2} = (Convert.IsDBNull({1}[\"{0}\"]))?null:({3}){1}[\"{0}\"]",
						/*0*/column.Name,
						/*1*/containerName,
						/*2*/objectName,
						/*3*/GetCSType(column));
			}
			else
			{
				// regular NOT NULL data types, set to default value for type if null
				return string.Format("{2} = ({3}){1}[\"{0}\"]",
					/*0*/column.Name,
					/*1*/containerName,
					/*2*/objectName,
					/*3*/GetCSType(column),
					/*4*/GetCSDefaultByType(column));
			}
		}
		
		/// <summary>
		/// Gets the expression used to set the property value in an entity.  Specificly used to handle nullable columns.
		/// </summary>
		/// <param name="column">The column object </param>
		/// <param name="containerName">The object that has a string indexer for the column (DataRow, IDataReader, etc)</param>
		/// <param name="objectName">The object instance name.</param>
		/// <param name="indent">How tabs should the code be indented</param>
		/// <returns>An expression that sets the property based on contents of the column in the container.</returns>
		/// <remarks>This method should not append the trailing semicolon.</remarks>
		public string GetObjectPropertySetExpression(ViewColumnSchema column, string containerName, string objectName, int indent)
		{
			if ( column.AllowDBNull )
			{
				// nullable reference types (strings), set to null if null retrieved from database
				return string.Format("{2} = ({1}.IsDBNull({1}.GetOrdinal(\"{0}\")))?null:({3}){1}[\"{0}\"]",
					/*0*/column.Name,
					/*1*/containerName,
					/*2*/GetObjectPropertyAccessor(column,objectName),
					/*3*/GetCSType(column));
			}
			else
			{
				// regular NOT NULL data types, set to default value for type if null
				return string.Format("{2} = ({3}){1}[\"{0}\"]",
					/*0*/column.Name,
					/*1*/containerName,
					/*2*/GetObjectPropertyAccessor(column,objectName),
					/*3*/GetCSType(column),
					/*4*/GetCSDefaultByType(column));
			}
		}

		/// <summary>
		/// Gets the expression used to set the property value in an entity.  Specificly used to handle nullable columns.
		/// </summary>
		/// <param name="column">The column object </param>
		/// <param name="containerName">The object that has a string indexer for the column (DataRow, IDataReader, etc)</param>
		/// <param name="objectName">The object instance name.</param>
		/// <param name="indent">How tabs should the code be indented</param>
		/// <returns>An expression that sets a temporary variable with a null value if possible.</returns>
		/// <remarks>This method should not append the trailing semicolon.</remarks>
		public string GetKeyIfNullable(ColumnSchema column, string objectName)
		{
			if ( column.AllowDBNull )
			{
				// nullable reference types (strings), set to null if null retrieved from database
				return string.Format("{2} tmp = {1} ?? {1}",
					/*0*/GetObjectPropertyAccessor(column,objectName),
					/*1*/GetCSDefaultByType(column));
			}
			return "";
		}
		
		/// <summary>
		/// Creates a string that reprensents an entity and its property.
		/// </summary>
		/// <param name="objectName">Name of the object.</param>
		/// <param name="column">Name of the column that define the property.</param>
		public string GetObjectPropertyAccessor(ColumnSchema column, string objectName)
		{
			return objectName + "." + GetPropertyName(column.Name);
		}
		
		/// <summary>
		/// Creates a string that reprensents an entity and its property.
		/// </summary>
		/// <param name="objectName">Name of the object.</param>
		/// <param name="column">Name of the column that define the property.</param>
		public string GetObjectPropertyAccessorWithDefault(ColumnSchema column, string objectName)
		{
			
			if ( column.AllowDBNull )
			{
				// nullable reference types (strings), set to null if null retrieved from database
				return string.Format("({0} ?? {1})",
					/*0*/GetObjectPropertyAccessor(column,objectName),
					/*1*/GetCSDefaultByType(column));
			}
			return objectName + "." + GetPropertyName(column.Name);
		}
		
		/// <summary>
		/// Creates a string that reprensents an entity and its property.
		/// </summary>
		/// <param name="objectName">Name of the object.</param>
		/// <param name="column">Name of the column that define the property.</param>
		public string GetOriginalObjectPropertyAccessor(ColumnSchema column, string objectName)
		{
			return objectName + ".Original" + GetPropertyName(column.Name);
		}

		/// <summary>
		/// Creates a string that reprensents an entity and its property.
		/// </summary>
		/// <param name="objectName">Name of the object.</param>
		/// <param name="column">Name of the column that define the property.</param>
		public string GetObjectPropertyAccessor(ViewColumnSchema column, string objectName)
		{
			return objectName + "." + GetPropertyName(column.Name);
		}
		
		/// <summary>
		/// Creates a string that retpresents a column as a class private member.
		/// </summary>
		/// <param name="column">the database column from which we want the generate a private member.</param>
		public string GetPrivateName(ColumnSchema column)
		{
			return GetPrivateName(column.Name);
		}
		

		
		/// <summary>
		/// Creates a string that retpresents a column as a class private member.
		/// </summary>
		/// <param name="name">the name from which we want the generate a private member.</param>
		public string GetPrivateName(string name)
		{		
		   	name = Regex.Replace(name, @"[\W]", "");
			name = GetCamelCaseName(name);
			
			foreach(string keyword in csharpKeywords)
			{
				if (keyword == name)
				{
					name = "@" + name;
				}
			}	
			
			if (Regex.IsMatch(name, @"^[\d]"))
				name="data" + name;
			
			return name;
		}

		/// <summary>
		/// Creates a string that represents a many to many relation name.
		/// </summary>
		/// <param name="junctionTableKey">The key that make the relationship.</param>
		/// <param name="junctionTableName">the table that store the relation.</param>
		public string GetManyToManyName(TableKeySchema junctionTableKey, string junctionTableName)
		{			
			return GetManyToManyName(junctionTableKey.ForeignKeyMemberColumns, junctionTableName);
		}
		
		/// <summary>
		/// Creates a string that represents a many to many relation name.
		/// </summary>
		/// <param name="columns">The columns that belong to the relationship.</param>
		/// <param name="junctionTableName">the table that store the relation.</param>
		public string GetManyToManyName(ColumnSchemaCollection columns, string junctionTableName)
		{
			StringBuilder result = new StringBuilder();
			foreach(ColumnSchema pCol in columns)
			{
				result.Append(GetCleanName(pCol.Name));
			}
			
			//See if there is any alias for this table name (check include in GetClassName)
			junctionTableName = GetClassName(junctionTableName);
			
			return string.Format(this.manyToManyFormat, result.ToString(), junctionTableName);
		}
		
		/// <summary>
		/// Check that a given key has all foreign's columns into the primary key.
		/// </summary>
		/// <param name="key">The key to check.</param>
		public bool IsJunctionKey(TableKeySchema key)
		{
			foreach(ColumnSchema col in key.ForeignKeyMemberColumns)
			{
				if (!col.IsPrimaryKeyMember)
				{
					//BYDAN_NETTIERS
					//System.Windows.Forms.MessageBox.Show(col.Name);
					//return false;
				}
			}
			return true;
		}
		
		/// <summary>
		/// Check that a given table has a primary key.
		/// </summary>
		/// <param name="table">The table to check.</param>
		public bool HasPrimaryKey(TableSchema table)
		{
            if (table.GetType().GetProperty("HasPrimaryKey") != null)
            {
                if (!(bool)table.GetType().GetProperty("HasPrimaryKey").GetValue(table, null)) return false;
            }
			if (table.PrimaryKey == null || table.PrimaryKey.MemberColumns.Count == 0) return false;
			return true;
		}
		
		/// <summary>
		/// Check that a given index has all it's columns into the primary key.
		/// </summary>
		/// <param name="index">The index to check.</param>
		public bool IsPrimaryKey(IndexSchema index)
		{
			foreach(ColumnSchema col in index.MemberColumns)
			{
				if (!col.IsPrimaryKeyMember)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Get the cleaned, camelcased name of a parameter
		/// </summary>
		/// <param name="par">Command Parameter</param>
		/// <returns>the cleaned, camelcased name </returns>
		public string GetCleanParName(ParameterSchema par)
		{
			return GetCleanParName(par.Name);
		}

		/// <summary>
		/// Get the cleaned, camelcased version of a name
		/// </summary>
		/// <param name="name">name to be cleaned</param>
		/// <returns>the cleaned, camelcased name </returns>
		public string GetCleanParName(string name)
		{
			return GetCamelCaseName(GetCleanName(name));
		}

		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="column">The ColumnSchema with the name to be cleaned</param>
		/// <returns>the cleaned, camelcased name with a _ prefix</returns>
		public string GetMemberVariableName(ColumnSchema column)
		{
			return "_" + GetCleanParName(column.Name);
		}

		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="name">name to be cleaned</param>
		/// <returns>the cleaned, camelcased name with a _ prefix</returns>
		public string GetMemberVariableName(string name)
		{
			return "_" + GetCleanParName(name);
		}
		
		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="column">The column with the name to be cleaned</param>
		/// <returns>the cleaned, pascal cases name with a _Original prefix</returns>
		public string GetOriginalMemberVariableName(ColumnSchema column)
		{
			return GetOriginalMemberVariableName(column.Name);
		}
		
		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="name">name to be cleaned</param>
		/// <returns>the cleaned, camelcased name with a _ prefix</returns>
		public string GetOriginalMemberVariableName(string name)
		{
			return "_" + GetOriginalPropertyName(name);
		}
		
		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="column">the column from which we want the name to be cleaned</param>
		/// <returns>the cleaned, camelcased name with a _ prefix</returns>
		public string GetOriginalPropertyName(ColumnSchema column)
		{
			return GetOriginalPropertyName(column.Name);
		}
		
		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="name">name to be cleaned</param>
		/// <returns>the cleaned, camelcased name with a _ prefix</returns>
		public string GetOriginalPropertyName(string name)
		{
			return "Original" + GetPropertyName(name);
		}

		/// <summary>
		/// Get the description ext. property of a column and return as inline SQL comment
		/// </summary>
		/// <param name="schemaObject">Any database object, but typically a column</param>
		/// <returns>Object description, as inline SQL comment</returns>
		public string GetColumnSqlComment(SchemaObjectBase schemaObject)
		{
			return schemaObject.Description.Length > 0 ? "-- " + schemaObject.Description.Replace(Environment.NewLine, Environment.NewLine + "-- ") : "";
		}
		
		#region GetColumnXmlComment
		/// <summary>
		/// Gets the table's description as a well formatted string for C# XML comments.
		/// </summary>
		public string GetColumnXmlComment(TableSchema table, int indentLevel)
		{
			return GetColumnXmlComment(table.Description, indentLevel);
		}

		/// <summary>
		/// Gets the column's description as a well formatted string for C# XML comments.
		/// </summary>
		public string GetColumnXmlComment(ColumnSchema column, int indentLevel)
		{
			return GetColumnXmlComment(column.Description, indentLevel);
		}

		/// <summary>
		/// Gets the view's description as a well formatted string for C# XML comments.
		/// </summary>
		public string GetColumnXmlComment(ViewSchema view, int indentLevel)
		{
			return GetColumnXmlComment(view.Description, indentLevel);
		}

		/// <summary>
		/// Gets the table key's description as a well formatted string for C# XML comments.
		/// </summary>
		public string GetColumnXmlComment(TableKeySchema key, int indentLevel)
		{
			return GetColumnXmlComment(key.Description, indentLevel);
		}
		
		/// <summary>
		/// Internal implementation.  Gets the description text as a clean C# XML comment line.
		/// </summary>
		private string GetColumnXmlComment(string description, int indentLevel)
		{
			string linePrefix = new string('\t', indentLevel) + "/// ";
			return description.Replace(Environment.NewLine, Environment.NewLine + linePrefix);
		}
		#endregion GetColumnXmlComment
		
		#region Component/Composition Helper Methods
			/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="column">The ColumnSchema with the name to be cleaned</param>
		/// <returns>the cleaned, camelcased name </returns>
		public string GetComponentMemberVariableName(ColumnSchema column)
		{
			return GetCleanParName(column.Name);
		}
		
		/// <summary>
		/// Get the member variable styled version of a name
		/// </summary>
		/// <param name="name">name to be cleaned</param>
		/// <returns>the cleaned, camelcased name</returns>
		public string GetComponentMemberVariableName(string name)
		{
			return GetCleanParName(name);
		}
		
		public string GetForeignKeyCompositeName (string fk, TableKeySchemaCollection fkeys)
		{
			foreach (TableKeySchema key in fkeys)
			{
				foreach (ColumnSchema col in key.ForeignKeyMemberColumns)
				{
					if (col.Name == fk)
					{
						return GetPropertyName(GetClassName(key.PrimaryKeyTable.Name));
					}
				}
			}
			return "//TODO: UNKNOWN, COULD NOT FIND FOREIGN KEY COMPOSITE NAME \t";
		}
				
		public string GetCompositeClassName(string fk, TableKeySchemaCollection fkeys)
		{
			foreach (TableKeySchema key in fkeys)
			{
				foreach (ColumnSchema col in key.ForeignKeyMemberColumns)
				{
					if (col.Name == fk)
					{
						return GetClassName(key.PrimaryKeyTable.Name);
					}
				}
			}
			return "//TODO: UNKNOWN, COULD NOT FIND COMPOSITE CLASS NAME \t" ;
		}
		
		public string GetCompositeMemberVariableName(string fk, TableKeySchemaCollection fkeys)
		{
			foreach (TableKeySchema key in fkeys)
			{
				foreach (ColumnSchema col in key.ForeignKeyMemberColumns)
				{
					if (col.Name == fk)
					{
						return GetComponentMemberVariableName(GetClassName(key.PrimaryKeyTable.Name));
					}
				}
			}
			return "//TODO: UNKNOWN, COULD NOT FIND COMPOSITE MEMBER VARIABLE NAME\t ";
		}
		
				
		public string GetCompositePropertyName(string fk, TableKeySchemaCollection fkeys)
		{
			foreach (TableKeySchema key in fkeys)
			{
				foreach (ColumnSchema col in key.ForeignKeyMemberColumns)
				{
					if (col.Name == fk)
					{
						return GetPropertyName(GetClassName(key.PrimaryKeyTable.Name));
					}
				}
			}
			return "//TODO: UNKNOWN, COULD NOT FIND COMPOSITE PROPERTY NAME\t ";
		}

		public string GetFKPropertyName(string fk, TableKeySchemaCollection fkeys)
		{
			foreach (TableKeySchema key in fkeys)
			{
				foreach (ColumnSchema col in key.ForeignKeyMemberColumns)
				{
					if (col.Name == fk)
					{
						return GetPropertyName(GetClassName(key.PrimaryKeyMemberColumns[0].Name));
					}
				}
			}
			return "//TODO: UNKNOWN, COULD NOT FIND FK COLUMN PROPERTY NAME\t ";
		}
		#endregion 

/*
		/// <summary>
		/// Transform the list of sql parameters to a list of comment param for a method
		/// </summary>
		public string TransformStoredProcedureInputsToMethodComments(ParameterSchemaCollection inputParameters)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<inputParameters.Count; i++)
			{
				temp.AppendFormat("{2}\t/// <param name=\"{0}\"> A <c>{1}</c> instance.</param>", GetPrivateName(inputParameters[i].Name.Substring(1)), GetCSType(inputParameters[i]).Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine);
			}
			
			return temp.ToString();
		}
		
*/

		/// <summary>
		/// Cleans the given text so that it can be used in a [DescriptionAttribute] attribute in C# code.
		/// </summary>
		public string GetDescriptionAttributeText(string text)
		{
			return text.Replace(Environment.NewLine, " ").Replace("\"", "'");
		}
		
		/// <summary>
		/// Determines if the given column has a user defined data type.  
		/// </summary>
		/// <remarks>
		/// User defined data types are dynamically loaded from the database where the column is from.
		/// </remarks>
		public bool IsUserDefinedType(ColumnSchema column)
		{
			// lazy load the user defined user type list
			if ( userDefinedTypes == null )
			{
				userDefinedTypes = GetUserDefinedTypes(column.Database);
			}
			
			foreach (string userDefinedType in userDefinedTypes)
			{
				// compare the data types case ignoring the case.
				if ( String.Compare(column.NativeType, userDefinedType, true) == 0 )
					return true;
			}
			return false;
		}
		
		private string[] userDefinedTypes = null;

		/// <summary>
		/// Determines if the given schema object has a user defined data type.  
		/// -- [ab] this is a generic ver of IsUserDefinedType(ColumnSchema column)
		/// </summary>
		/// <remarks>
		/// User defined data types are dynamically loaded from the database where the schema object is from.
		/// </remarks>
		public bool IsUserDefinedType<TSchemaType>(TSchemaType schemaItem) where TSchemaType:DataObjectBase
		{
			// lazy load the user defined user type list
			if ( userDefinedTypes == null )
			{
				userDefinedTypes = GetUserDefinedTypes(schemaItem.Database);
			}
			
			foreach (string userDefinedType in userDefinedTypes)
			{
				// compare the data types case ignoring the case.
				if ( String.Compare(schemaItem.NativeType, userDefinedType, true) == 0 )
					return true;
			}
			return false;
		}



		/// <summary>
		/// Get the user defined data types from the specified database.
		/// </summary>
		protected string[] GetUserDefinedTypes(DatabaseSchema database)
		{
			switch (database.Provider.Name)
			{
				case "SqlSchemaProvider":
					return GetSqlUserDefinedTypes(database);
				default:
					return new string[0];
			}
		}
		
		/// <summary>
		/// Get the user defined data types from the specified Sql Server database.
		/// </summary>
		protected string[] GetSqlUserDefinedTypes(DatabaseSchema database)
		{
			try
			{
				SqlCommand	command = new SqlCommand();
				command.CommandText = "sp_MShelptype";
				command.CommandType = CommandType.StoredProcedure;
				command.Connection = new SqlConnection(database.ConnectionString);
	   
				command.Parameters.Add("@typename", SqlDbType.NVarChar, 517);
				command.Parameters.Add("@flags", SqlDbType.NVarChar, 10);

				command.Parameters[0].Value = System.DBNull.Value;
				command.Parameters[1].Value = "uddt";  // look in user defined datatypes

				ArrayList udt = new ArrayList();
				command.Connection.Open();
				using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
				{
					while(reader.Read()) 
					{
						udt.Add(reader["UserDatatypeName"]);
					}
				}
				
				string[] userDatatypeNames = new string[ udt.Count ];
				udt.CopyTo(userDatatypeNames,0);
				return userDatatypeNames;
			}
			catch 
			{
				return new string[0]; // oh oh, should handle this better.
			}
		}

		/// <summary>
		/// Check if a column is an identity column
		/// </summary>
		/// <param name="column">DB table column to be checked</param>
		/// <returns>Identity?</returns>
		public bool IsIdentityColumn(ColumnSchema column)
		{
			// for sql server
			if (column.ExtendedProperties["CS_IsIdentity"] != null)
				return (bool)column.ExtendedProperties["CS_IsIdentity"].Value;
			
			// for access
			if (column.ExtendedProperties["Autoincrement"] != null)
				return (bool)column.ExtendedProperties["Autoincrement"].Value;
			
			// test mysql
			
			
			return false;
			
			//Autoincrement: 
			//return (bool)column.ExtendedProperties["CS_IsIdentity"].Value;
		} 
		
		/// <summary>
		/// Get's the default value of a column
		/// </summary>
		/// <param name="column">DB table column to be checked</param>
		/// <returns>string representation of the default value</returns>
		public string GetColumnDefaultValue(ColumnSchema column)
		{
			/*
			return "";
			
			// for sql server
			if (column.ExtendedProperties["CS_Default"] != null)
			{
				string value = column.ExtendedProperties["CS_Default"].Value.ToString().ToLower();
				value = value.Replace("getdate()", "DateTime.Now");
				value = value.Replace("newid()", "Guid.NewGuid()");
				
				while(value.StartsWith("(") && value.EndsWith(")"))
    			 	value= value.TrimStart('(').TrimEnd(')');
	
				if (column.DataType == DbType.Boolean)
					value = value.Contains("1") ? "true" : "false";
				else if (!IsNumericType(column) || value.IndexOf("DateTime.Now") > -1 || value.IndexOf("Guid.NewGuid()") > -1)
					value = string.Format("\"{0}\"", value);
					
				return value;
	
				
			// for access
			if (column.ExtendedProperties["DefaultValue"] != null)
				return column.ExtendedProperties["DefaultValue"].Value.ToString();
			
			// test mysql
			}
			
			*/

			return "";			
		} 
		
		/// <summary>
		/// Determines if the column is a numeric column or not.
		/// </summary>
		/// <param name="column">DB table column to be checked</param>
		/// <returns>true when Numeric, otherwise false</returns>
		public bool IsNumericType(ColumnSchema column)
		{
			switch (column.NativeType.ToLower())
			{
				case "bigint":
				case "bit":
				case "decimal":
				case "float":
				case "int":
				case "money":
				case "numeric":
				case "real":
				case "smallint":
				case "smallmoney":
				case "tinyint": return true;
				default: return false;
			}
		}

		/// <summary>
		/// Check if a column is read-only.
		/// </summary>
		public bool IsReadOnlyColumn(ColumnSchema column)
		{
			// sql server
			if (column.ExtendedProperties["CS_ReadOnly"].Value != null && (bool)column.ExtendedProperties["CS_ReadOnly"].Value)
				return true;
			
			// access, if auto inco
			if (column.ExtendedProperties["Autoincrement"] != null && (bool)column.ExtendedProperties["Autoincrement"].Value)
				return true;
				
			// Jet: if auto generate
			if (column.ExtendedProperties["Jet OLEDB:AutoGenerate"] != null && (bool)column.ExtendedProperties["Jet OLEDB:AutoGenerate"].Value)
				return true;
				
			// default
			return false;
			
			//return column.ExtendedProperties.Count == 0 || (bool)column.ExtendedProperties["CS_ReadOnly"].Value;
		}
		
		/// <summary>
		///  Check if a column is computed.
		/// </summary>
		/// <param name="column"></param>
		public bool IsComputed(ColumnSchema column)
		{
			// Sql server extended property
			if (column.ExtendedProperties["CS_IsComputed"] != null && (bool)column.ExtendedProperties["CS_IsComputed"].Value)
				return true;
			
			// sqlserver timestamp field are computed
			if (column.NativeType.ToLower() == "timestamp")
				return true;
				
			// access, if auto inco
			if (column.ExtendedProperties["Autoincrement"] != null && (bool)column.ExtendedProperties["Autoincrement"].Value)
				return true;
				
			// Jet: if auto generate
			if (column.ExtendedProperties["Jet OLEDB:AutoGenerate"] != null && (bool)column.ExtendedProperties["Jet OLEDB:AutoGenerate"].Value)
				return true;
			
			
			return false;
			
			//return (bool)column.ExtendedProperties["CS_IsComputed"].Value == true || column.NativeType.ToLower() == "timestamp";
		}
		
		/// <summary>
		///  Check if a column is a guid (uniqueidentifier).
		/// </summary>
		/// <param name="column"></param>
		public bool IsGuidColumn( ColumnSchema column )
		{
			return column.SystemType.ToString() == typeof(System.Guid).ToString();
		}

		/// <summary>
		/// Get the owner of a table.
		/// </summary>
		/// <param name="table">The table to check</param>
		/// <returns>The safe name of the owner of the table</returns>
		public string GetOwner(TableSchema table)
		{
			return (table.Owner.Length > 0) ? GetSafeName(table.Owner) + "." : "";
		}
		
		/// <summary>
		/// Get the owner of a view
		/// </summary>
		/// <param name="view">The view to check</param>
		/// <returns>The safe name of the owner of the view</returns>
		public string GetOwner(ViewSchema view)
		{
			return (view.Owner.Length > 0) ? GetSafeName(view.Owner) + "." : "";
		}

		/// <summary>
		/// Get the owner of a command
		/// </summary>
		/// <param name="command">The command to check</param>
		/// <returns>The safe name of the owner of the command</returns>
		public string GetOwner(CommandSchema command)
		{
			return (command.Owner.Length > 0) ? GetSafeName(command.Owner) + "." : "";
		}

		/// <summary>
		/// Does the command have a resultset?
		/// </summary>
		/// <param name="cmd">Command in question</param>
		/// <returns>Resultset?</returns>
		public bool HasResultset(CommandSchema cmd)
		{
			return cmd.CommandResults.Count > 0;
		}
		
		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="columns">Columns for which to get the Sql parameter statement</param>
		/// <returns>Sql Parameter statement</returns>
		public string GetSqlParameterStatement(ColumnSchemaCollection columns)
		{
			StringBuilder result = new StringBuilder();
			
			for(int i=0; i<columns.Count; i++)
			{
				if (i>0) result.Append(", ");
				
				result.Append(GetSqlParameterStatement(columns[i]));
				result.Append(Environment.NewLine);
				
			}	
			return result.ToString();
		}

		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="column">Column for which to get the Sql parameter statement</param>
		/// <returns>Sql Parameter statement</returns>
		public string GetSqlParameterStatement(ColumnSchema column)
		{
			return GetSqlParameterStatement(column, false);
		}
		
		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="column">Column for which to get the Sql parameter statement</param>
		/// <param name="isOutput">Is this an output parameter?</param>
		/// <returns>Sql Parameter statement</returns>
		public string GetSqlParameterStatement(ColumnSchema column, bool isOutput)
		{
			StringBuilder param = new StringBuilder();
			param.AppendFormat("@{0} {1}", GetPropertyName(column.Name), column.NativeType);
			
			// user defined data types do not need size components
			if ( ! IsUserDefinedType(column) )
			{
			switch (column.DataType)
			{
				case DbType.Decimal:
				{
					if (column.NativeType != "real")
						param.AppendFormat("({0}, {1})", column.Precision, column.Scale);
				
					break;
				}
				// [ab 022205] now handles xxxbinary data type
				case DbType.Binary:
				// 
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:
				{
					if (column.NativeType != "text" && 
						column.NativeType != "ntext" && 
						column.NativeType != "timestamp" &&
						column.NativeType != "image"
						)

					{
						if (column.Size > 0)
						{
							param.AppendFormat("({0})", column.Size);
						}
					}
					break;
				}
			}
			}
			if (isOutput)
			{
				param.Append(" OUTPUT");
			}
			
			return param.ToString();
		}
		
		
		public bool IsColumnFindable(ColumnSchema column)
		{
			if (column.DataType == DbType.Binary || column.NativeType == "text" || 
					column.NativeType == "ntext" || 
					column.NativeType == "timestamp" ||
					column.NativeType == "image"
				)
				return false;
			
			return true;
		}
		
		
		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="column">Column for which to get the Sql parameter statement</param>
		/// <param name="Name">the name of the parameter?</param>
		/// <returns>Sql Parameter statement</returns>
		public string GetSqlParameterStatement(ColumnSchema column, string Name)
		{
			string param = "@" + GetPropertyName(Name) + " " + column.NativeType;
			
			// user defined data types do not need size components
			if ( ! IsUserDefinedType(column) )
			{
			switch (column.DataType)
			{
				case DbType.Decimal:
				{
					param += "(" + column.Precision + ", " + column.Scale + ")";
					break;
				}
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:
				{
					if (column.NativeType != "text" && column.NativeType != "ntext")
					{
						if (column.Size > 0)
						{
							param += "(" + column.Size + ")";
						}
					}
					break;
				}
			}	
			}
			return param;
		}
		
		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="column">Column for which to get the Sql parameter statement</param>
		/// <param name="isOutput">indicates the direction</param>
		/// <returns>Sql Parameter statement</returns>
		public string GetSqlParameterXmlNode(ColumnSchema column, bool isOutput)
		{
			return GetSqlParameterXmlNode(column, column.Name, isOutput, false);
		}
		
		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="column">Column for which to get the Sql parameter statement</param>
		/// <param name="parameterName">the name of the parameter?</param>
		/// <param name="isOutput">indicates the direction</param>
		/// <returns>the xml Sql Parameter statement</returns>
		public string GetSqlParameterXmlNode(ColumnSchema column, string parameterName, bool isOutput)
		{
			return GetSqlParameterXmlNode(column, parameterName, isOutput, false);
		}
		
		/// <summary>
		/// Get a SqlParameter statement for a column
		/// </summary>
		/// <param name="column">Column for which to get the Sql parameter statement</param>
		/// <param name="parameterName">the name of the parameter?</param>
		/// <param name="isOutput">indicates the direction</param>
		/// <param name ="nullDefaults">indicates whether to give each parameter a null or empty default.  (used mainly for Find sp's)</param>
		/// <returns>the xml Sql Parameter statement</returns>
		public string GetSqlParameterXmlNode(ColumnSchema column, string parameterName, bool isOutput, bool nullDefaults)
		{
			string formater = "<parameter name=\"@{0}\" type=\"{1}\" direction=\"{2}\" size=\"{3}\" precision=\"{4}\" scale=\"{5}\" param=\"{6}\" nulldefault=\"{7}\"/>";			
			
			string nullDefaultValue = "";
			if (nullDefaults)
			{
				nullDefaultValue = "null";
			}

			bool isReal = false;
			if (column.NativeType.ToLower() == "real") // SQL doesn't like precision or scale on Real
			{
				isReal = true;
			}

			return string.Format(formater, GetPropertyName(parameterName), column.NativeType, isOutput ? "Output" : "Input", column.Size, column.Precision, column.Scale, isReal ? "" : GetSqlParameterParam(column), nullDefaultValue);
		}
		
		/// <summary>
		/// Get an xml representation for a stored procedure parameter - this is for pre-existing (most likely, custom) Stored Procedures
		/// </summary>
		/// <param name="parameter">SP Parameter for which to get the Sql parameter statement</param>
		/// <returns>the xml Sql Parameter statement</returns>
		public string GetSqlParameterXmlNode(ParameterSchema parameter)
		{
			string formater = "<parameter name=\"@{0}\" type=\"{1}\" direction=\"{2}\" size=\"{3}\" precision=\"{4}\" scale=\"{5}\" param=\"{6}\" nulldefault=\"{7}\"/>";			
			
			return string.Format(	formater, 
									parameter.Name.TrimStart('@'),
									parameter.NativeType, 
									parameter.Direction.ToString(), 
									parameter.Size, 
									parameter.Precision, 
									parameter.Scale, 
									parameter.NativeType.ToLower() == "real" ? String.Empty : GetSqlParameterParam<ParameterSchema>(parameter), 
									parameter.AllowDBNull? "null":String.Empty );
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		public string GetSqlParameterParam(ColumnSchema column)
		{
			string param = string.Empty;
			
			// user defined data types do not need size components
			if ( ! IsUserDefinedType(column) )
			{
			switch (column.DataType)
			{
				case DbType.Decimal:
				{
					param = "(" + column.Precision + ", " + column.Scale + ")";
					break;
				}
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:
				case DbType.Binary:
				{
					if (column.NativeType != "text" && column.NativeType != "ntext" && column.NativeType != "image" && column.NativeType != "timestamp")
					{
						if (column.Size > 0)
						{
							param = "(" + column.Size + ")";
						}
						else if (column.Size == -1)
						{
							param = "(MAX)";
						}
					}
					break;
				}
			}	
			}
			return param;
		}

		/// <summary>
		/// 	[ab] This is a somewhat generic :) version of the singly typed GetSqlParameterParam
		/// </summary>
		/// <param name="column"></param>
		/// <remarks>
		///	
		/// </remarks>
		public string GetSqlParameterParam<TSchemaType>(TSchemaType schemaItem) where TSchemaType:DataObjectBase
		{
			string param = string.Empty;
			
			// user defined data types do not need size components
			if ( ! IsUserDefinedType<TSchemaType>(schemaItem) )
			{
			switch (schemaItem.DataType)
			{
				case DbType.Decimal:
				{
					param = "(" + schemaItem.Precision + ", " + schemaItem.Scale + ")";
					break;
				}
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:
				case DbType.Binary:				
				{
					if (schemaItem.NativeType != "text" && schemaItem.NativeType != "ntext")
					{
						if (schemaItem.Size > 0)
						{
							param = "(" + schemaItem.Size + ")";
						}
						else if (schemaItem.Size == -1)
						{
							param = "(MAX)";
						}
					}
					break;
				}
			}	
			}
			return param;
		}
		

		/// <summary>
		/// Parse the text of a stored procedure to retrieve any comment prior to the CREATE PROC construct
		/// </summary>
		/// <param name="commandText">Command Text of the procedure</param>
		/// <returns>The procedure header comment</returns>
		public string GetSqlProcedureComment(string commandText)
		{
			string comment = "";
			// Find anything upto the CREATE PROC statement
			Regex regex = new Regex(@"CREATE\s+PROC(?:EDURE)?", RegexOptions.IgnoreCase);	
			comment = regex.Split(commandText)[0];
			//remove comment characters
			regex = new Regex(@"(-{2,})|(/\*)|(\*/)");
			comment = regex.Replace(comment, string.Empty);
			//trim and return
			return comment.Trim();
		}

		/// <summary>
		/// Get any in-line SQL comments on the same lines as parameters
		/// </summary>
		/// <param name="commandText">Command Text of the procedure</param>
		/// <returns>Hashtable of parameter comments, with parameter names as keys</returns>
		public Hashtable GetSqlParameterComments(string commandText)
		{
			Hashtable paramComments = new Hashtable();
			//Get parameter names and comments
			string pattern = @"(?<param>@\w*)[^@]*--(?<comment>.*)";
			//loop through the matches and extract the parameter and the comment, ignoring duplicates
			foreach (Match match in Regex.Matches(commandText, pattern))
				if (!paramComments.ContainsKey(match.Groups["param"].Value))
					paramComments.Add(match.Groups["param"].Value, match.Groups["comment"].Value.Trim());
			//return the hashtable
			return paramComments;
		}
		
		
		#region "Stored procedures input transformations"
		
		/// <summary>
		/// Transform the list of sql parameters to a list of method parameters.
		/// </summary>
		public string TransformStoredProcedureInputsToMethod(ParameterSchemaCollection inputParameters)
		{
			return TransformStoredProcedureInputsToMethod(false, inputParameters);
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of method parameters.
		/// </summary>
		public string TransformStoredProcedureInputsToMethod(bool startWithComa, ParameterSchemaCollection inputParameters)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<inputParameters.Count; i++)
			{
				if ((i>0) || startWithComa)
					temp.Append(", ");

				temp.AppendFormat("{0} {1}", GetCSType(inputParameters[i]), GetPrivateName(inputParameters[i].Name.Substring(1)));
			}
			
			return temp.ToString();
		}
		
		public string TransformStoredProcedureInputsToMethod(bool startWithComa, CommandSchema command)
		{
			string temp = string.Empty;
			
			for(int i=0; i<command.InputParameters.Count; i++)
			{
				temp += (temp.Length > 0) || startWithComa ? ", " : "";
				temp += GetCSType(command.InputParameters[i]) + " " + GetPrivateName(command.InputParameters[i].Name.Substring(1));
			}
			for(int j=0; j < command.InputOutputParameters.Count; j++)
			{
				temp += (temp.Length > 0) || (startWithComa)  ? ", out " : " out ";
				temp += GetCSType(command.InputOutputParameters[j]) + " " + GetPrivateName(command.InputOutputParameters[j].Name.Substring(1));
			}
			
			return temp;
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of ExecuteXXXXX parameters.
		/// </summary>
		public string TransformStoredProcedureInputsToDataAccess(ParameterSchemaCollection inputParameters)
		{
			return TransformStoredProcedureInputsToDataAccess(false, inputParameters);
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of ExecuteXXXXX parameters.
		/// </summary>
		public string TransformStoredProcedureInputsToDataAccess(bool alwaysStartWithaComa, ParameterSchemaCollection inputParameters)
		{
			return TransformStoredProcedureInputsToDataAccess(alwaysStartWithaComa, inputParameters, false);
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of ExecuteXXXXX parameters.
		/// </summary>
		public string TransformStoredProcedureInputsToDataAccess(bool alwaysStartWithaComa, ParameterSchemaCollection inputParameters, bool useCustomPrefix)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<inputParameters.Count; i++)
			{
				if ( (i>0) || alwaysStartWithaComa )
					temp.Append(", ");

				if ( useCustomPrefix )
				{
					temp.Append( GetCustomVariableName(inputParameters[i].Name.Substring(1) , inputParameters[i].Command) );
				}
				else
				{
					temp.Append( GetPrivateName(inputParameters[i].Name.Substring(1)) );
				}
			}
			
			return temp.ToString();
		}
						
		/// <summary>
		/// Transform the list of sql parameters to a list of comment param for a method
		/// </summary>
		public string TransformStoredProcedureInputsToMethodComments(ParameterSchemaCollection inputParameters)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<inputParameters.Count; i++)
			{
				temp.AppendFormat("{2}\t\t/// <param name=\"{0}\"> A <c>{1}</c> instance.</param>", GetPrivateName(inputParameters[i].Name.Substring(1)).Replace("@", ""), GetCSType(inputParameters[i]).Replace("<", "&lt;").Replace(">", "&gt;"), "\r\r\n");
			}
			
			return temp.ToString();
		}

		/// <summary>
		/// Transform the list of sql parameters to a list of comment param for a method
		/// </summary>
		public string TransformStoredProcedureInputsToMethodComments(CommandSchema command)
		{
			string temp = string.Empty;
			for(int i=0; i<command.InputParameters.Count; i++)
			{
				temp += string.Format("{2}\t\t\t/// <param name=\"{0}\"> A <c>{1}</c> instance.</param>", GetPrivateName(command.InputParameters[i].Name.Substring(1)), GetCSType(command.InputParameters[i]), "\r\r\n");
			}
			for(int i=0; i<command.InputOutputParameters.Count; i++)
			{
				temp += string.Format("{2}\t\t\t/// <param name=\"{0}\"> An output  <c>{1}</c> instance.</param>", GetPrivateName(command.InputOutputParameters[i].Name.Substring(1)).Replace("@", ""), GetCSType(command.InputOutputParameters[i]), Environment.NewLine);
			}
			
			return temp;
		}

		#endregion
		
		#region "Stored procedures output transformations"
		
		/// <summary>
		/// Transform the list of sql parameters to a list of method parameters.
		/// </summary>
		public string TransformStoredProcedureOutputsToMethod(ParameterSchemaCollection outputParameters)
		{
			return TransformStoredProcedureOutputsToMethod(false, outputParameters);
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of method parameters.
		/// </summary>
		public string TransformStoredProcedureOutputsToMethod(bool startWithComa, ParameterSchemaCollection outputParameters)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<outputParameters.Count; i++)
			{
				if ((i>0) || startWithComa)
					temp.Append(", ");

				temp.AppendFormat("ref {0} {1}", GetCSType(outputParameters[i]), GetPrivateName(outputParameters[i].Name.Substring(1)));
			}
			
			return temp.ToString();
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of ExecuteXXXXX parameters.
		/// </summary>
		public string TransformStoredProcedureOutputsToDataAccess(ParameterSchemaCollection outputParameters)
		{
			return TransformStoredProcedureOutputsToDataAccess(false, outputParameters);
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of ExecuteXXXXX parameters.
		/// </summary>
		public string TransformStoredProcedureOutputsToDataAccess(bool alwaysStartWithaComa, ParameterSchemaCollection outputParameters)
		{
			return TransformStoredProcedureOutputsToDataAccess(alwaysStartWithaComa, outputParameters, false);
		}
		
		/// <summary>
		/// Transform the list of sql parameters to a list of ExecuteXXXXX parameters.
		/// </summary>
		public string TransformStoredProcedureOutputsToDataAccess(bool alwaysStartWithaComa, ParameterSchemaCollection outputParameters, bool useCustomPrefix)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<outputParameters.Count; i++)
			{
				if ( (i>0) || alwaysStartWithaComa )
					temp.Append(", ");

				if ( useCustomPrefix )
				{
					temp.AppendFormat("ref {0}", GetCustomVariableName(outputParameters[i].Name.Substring(1), outputParameters[i].Command) );
				}
				else
				{
					temp.AppendFormat("ref {0}", GetPrivateName(outputParameters[i].Name.Substring(1)) );
				}
			}
			
			return temp.ToString();
		}
						
		/// <summary>
		/// Transform the list of sql parameters to a list of comment param for a method
		/// </summary>
		public string TransformStoredProcedureOutputsToMethodComments(ParameterSchemaCollection outputParameters)
		{
			StringBuilder temp = new StringBuilder();
			for(int i=0; i<outputParameters.Count; i++)
			{
				temp.AppendFormat("{2}\t\t\t/// <param name=\"{0}\"> A <c>{1}</c> instance.</param>", GetPrivateName(outputParameters[i].Name.Substring(1)).Replace("@", ""), GetCSType(outputParameters[i]).Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine);
			}
			
			return temp.ToString();
		}

		#endregion
		
		/// <summary>
		/// Returns a string that reprenst the given columns formated as method parameters definitions. (ex: string param1, int param2)
		/// </summary>
		/// <param name="columns">The columns to transform.</param>
		public string GetFunctionHeaderParameters(ColumnSchemaCollection columns)
		{
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < columns.Count; i++)
			{
				output.AppendFormat("{0} {1}", GetCSType(columns[i]), GetPrivateName(columns[i].Name));
				if (i < columns.Count - 1)
				{
					output.Append(", ");
				}
			}
			return output.ToString();
		}
		
		
		/// <summary>
		/// Returns a string that reprenst the given columns formated as method parameters call. (ex: param1, param2)
		/// </summary>
		/// <param name="columns">The columns to transform.</param>
		public string GetFunctionCallParameters(ColumnSchemaCollection columns)
		{
			return GetFunctionCallParameters(columns, string.Empty, null);
		}
		
		public delegate bool AppendIf(ColumnSchema col);
		
		/// <summary>
		/// Returns a string that reprenst the given columns formated as method parameters call. (ex: param1, param2)
		/// </summary>
		/// <param name="columns">The columns to transform.</param>
		public string GetFunctionCallParameters(ColumnSchemaCollection columns, string appendString, AppendIf condition)
		{			
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < columns.Count; i++)
			{		
				output.Append(GetPrivateName(columns[i].Name));
				if (condition != null)
				{
					if (condition(columns[i]))
					{
						output.Append(appendString);
					}
				}
					
				if (i < columns.Count - 1)
				{
					output.Append(", ");
				}
			}
			return output.ToString();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="columns"></param>
		public string GetFunctionEntityParameters(ColumnSchemaCollection columns)
		{
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < columns.Count; i++)
			{
				output.AppendFormat("entity.{0}", GetPropertyName(columns[i].Name));
				if (i < columns.Count - 1)
				{
					output.Append(", ");
				}
			}
			return output.ToString();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="columns"></param>
		/// <param name="accessor"></param>
		public string GetFunctionThisParametersWithNullable(ColumnSchemaCollection columns, string accessor)
		{
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < columns.Count; i++)
			{
				if (!columns[i].AllowDBNull)
					output.AppendFormat("{1}.{0}", GetPropertyName(columns[i].Name), accessor);
				else
					output.AppendFormat("({1}.{0} ?? {2})", GetPropertyName(columns[i].Name), accessor, GetCSDefaultByType(columns[i]));
				
				if (i < columns.Count - 1)
				{
					output.Append(", ");
				}
			}
			return output.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="columns"></param>
		/// <param name="objectName"></param>
		public string GetFunctionObjectParameters(ColumnSchemaCollection columns, String objectName)
		{
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < columns.Count; i++)
			{
				output.AppendFormat("{1}.{0}", GetPropertyName(columns[i].Name), objectName);

				if (i < columns.Count - 1)
				{
					output.Append(", ");
				}
			}
			return output.ToString();
		}

		/// <summary>
		/// Gets the <see cref="System.ComponentModel.DataObjectField" /> Ctor Params
		/// based on the schema information on a column.
		/// The 4 params are 
		///	1. indicates whether the field is the primary key 
		/// 2. whether the field is a database identity field
		/// 3. whether the field can be null
		/// 4. sets the length of the field
		/// </summary>
		/// <param name="column">Column</param>
		/// <returns>The ctor params for the <see cref="System.ComponentModel.DataObjectField" /></returns>
		public string GetDataObjectFieldCallParams(ColumnSchema column)
		{
			return string.Format("{0}, {1}, {2}{3}",
				/*0*/ column.IsPrimaryKeyMember.ToString().ToLower(),
				/*1*/ IsIdentityColumn(column).ToString().ToLower(),
				/*2*/ column.AllowDBNull.ToString().ToLower(),
				/*3*/ (CanCheckLength(column) ? ", " + column.Size.ToString() : ""));
		}
		
		/// <summary>
		/// Gets the parameters needed for the ColumnEnumAttribute class
		/// for the specified column.
		/// </summary>
		/// <param name="column"></param>
		public string GetColumnEnumAttributeParams(ColumnSchema column)
		{
			return string.Format("\"{0}\", typeof({1}), System.Data.{2}, ",
				column.Name,
				GetCSTypeWithoutNullable(column),
				GetDbType(column)
			) + GetDataObjectFieldCallParams(column);
		}

		/// <summary>
		/// Convert database types to C# types
		/// </summary>
		/// <param name="field">Column or parameter</param>
		/// <returns>The C# (rough) equivalent of the field's data type</returns>
		public string GetCSType(DataObjectBase field)
		{		
			if (field.NativeType.ToLower() == "real")
				return "System.Single" + (field.AllowDBNull?"?":"");
			else if (field.NativeType.ToLower() == "xml")
				return "string";
			//else if (field.NativeType.ToLower() == "xml")
			//	return "System.Xml.XmlNode";
			///Only for Custom Stored Procedures that allow nulls for every param
			else if (CSPUseDefaultValForNonNullableTypes 
					&& (field is ParameterSchema)
					&&	!IsCSReferenceDataType(field))
			{			
				if (!DefaultIsNull(	(ParameterSchema)field ))
					return field.SystemType.ToString();
				else 
					return field.SystemType.ToString() + "?";
			}
			else if (!IsCSReferenceDataType(field) && field.AllowDBNull)
			{				
				return field.SystemType.ToString() + "?";
			}
			else
				return field.SystemType.ToString();
		}
		
		#region Defualt Param Value
		
        public static string parseParameterRegex = @"
CREATE\s+PROC(?:EDURE)?                               # find the start of the stored procedure
.*?                                                   # skip all content until we get to the name of the parameter that we are looking for
{0}                                                   # name of the parameter we are interested in
\s+[\w\.\(\)\[\]]+                                    # parameter data type
(?:\s*\=\s*(?<default>(?:'[^']*' | [\w]+)))?          # parameter default value";

		///<summary>
		/// Checks a Parameter Schema if NULL is set to the default value of that procedure param
		///</summary>
		public bool DefaultIsNull(ParameterSchema param)
		{
			if (param == null)
				return false;
			
			System.Text.RegularExpressions.Regex DefaultParamRegex = new System.Text.RegularExpressions.Regex(String.Format(parseParameterRegex, param.Name), 
				System.Text.RegularExpressions.RegexOptions.IgnoreCase | 
				System.Text.RegularExpressions.RegexOptions.Multiline | 
				System.Text.RegularExpressions.RegexOptions.Singleline | 
				System.Text.RegularExpressions.RegexOptions.CultureInvariant | 
				System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

		
            System.Text.RegularExpressions.Match match = DefaultParamRegex.Match(param.Command.CommandText);
            if (match != null && match.Success)
			{
				if (match.Groups["default"].Value != null && match.Groups["default"].Value.ToString().Trim().ToUpper() == "NULL")
					return true;
			}	
			return false;
		}
		#endregion 
		
		/// <summary>
		/// Convert database types to C# types, withou nullable support.
		/// </summary>
		/// <param name="field">Column or parameter</param>
		/// <returns>The C# (rough) equivalent of the field's data type</returns>
		public string GetCSTypeWithoutNullable(DataObjectBase field)
		{
			if (field.NativeType.ToLower() == "real")
				return "System.Single";
			else if (field.NativeType.ToLower() == "xml")
				return "string";
			//else if (field.NativeType.ToLower() == "xml")
			//	return "System.Xml.XmlNode";
			else
				return field.SystemType.ToString();
			//return GetCSType(field.DataType);
		}
		
		/// <summary>
		/// Return the DbType enum entry of a specified column. It's a hack of the SchemaExplorer property, as it do not support Xml column properly.
		/// </summary>
		/// <param name="field">Column or parameter</param>
		public string GetDbType(DataObjectBase field)
		{
			if (field.NativeType.ToLower() == "xml")
			{
				return "DbType.Xml";
			}
			else
			{
				return "DbType." + field.DataType.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		public string GetCSDefaultByType(DataObjectBase column)
		{
			return GetCSDefaultByType(column, false);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		public string GetCSDefaultByType(DataObjectBase column, bool forceReturnDefault)
		{
			// first attempt to see if the DB defines any default values for this
			// column.  If so, return it.
			string defaultValue;
			if (ParseDbColDefaultVal && !forceReturnDefault)
			{
				defaultValue = GetCSDefaultValueByType(column);
				if (defaultValue != null)
					return defaultValue;
			}

			if (column.NativeType.ToLower() == "real")
				return "0.0F";
			else
			{
				DbType dataType = column.DataType;
				switch (dataType)
				{
					case DbType.AnsiString: 
						return "string.Empty";
						
					case DbType.AnsiStringFixedLength: 
						return "string.Empty";
					
					case DbType.String: 
						return "string.Empty";
						
					case DbType.Boolean: 
						return "false";
					
					case DbType.StringFixedLength: 
						return "string.Empty";
						
					case DbType.Guid: 
						return "Guid.Empty";
					
					
					//Answer modified was just 0
					case DbType.Binary: 
						return "new byte[] {}";
					
					//Answer modified was just 0
					case DbType.Byte:
						return "(byte)0";
						//return "{ 0 }";
					
					case DbType.Currency: 
						return "0";
					
					case DbType.Date: 
						return "DateTime.MinValue";
					
					case DbType.DateTime: 
						return "DateTime.MinValue";
					
					case DbType.Decimal: 
						return "0.0m";
						//return "0M";
						//return "0.0M";
					
					case DbType.Double: 
						return "0.0f";
					
					case DbType.Int16: 
						return "(short)0";
						
					case DbType.Int32: 
						return "(int)0";
						
					case DbType.Int64: 
						return "(long)0";
					
					case DbType.Object: 
						return "null";
					
					case DbType.Single: 
						return "0F";
					
					//case DbType.Time: return "DateTime.MaxValue";
					case DbType.Time: return "new DateTime(1900,1,1,0,0,0,0)";
					case DbType.VarNumeric: return "0";
						//the following won't be used
						//		case DbType.SByte: return "0";
						//		case DbType.UInt16: return "0";
						//		case DbType.UInt32: return "0";
						//		case DbType.UInt64: return "0";
					default: return "null";
				}
			}
		}
		
		/// <summary>
		/// This method returns the default value from the database if it is available.  It returns null
		/// if no default value could be parsed.
		/// 
		/// NOTE: rudimentary support for default values with computations/functions is built in.  Right now th
		///   only supported function is getdate().  Any others can be added below if desired.
		/// </summary>
		/// <param name="column"></param>
		public string GetCSDefaultValueByType(DataObjectBase column)
		{
			if (column == null)
				return null;

			ExtendedProperty defaultValueProperty = column.ExtendedProperties["CS_Default"];
			if (defaultValueProperty == null)
				return null;			
				
			string defaultValue = null;
			
			#region Convert declarations
			bool boolConvert;
			byte byteConvert;
			decimal decimalConvert;
			double doubleConvert;
			float floatConvert;
			int intConvert;
			long longConvert;
			short shortConvert;
			DateTime dateConvert;
			Guid guidConvert; 
			XmlNode xmlNodeConvert;
			#endregion
	
			try
			{
				//Get Default Value 
				defaultValue = defaultValueProperty.Value.ToString();
				
				if (defaultValue == null || defaultValue.Trim().Length == 0)
					return null;
				
				// trim off the surrounding ()'s if they exist (SQL Server)
				while (defaultValue.StartsWith("(") && defaultValue.EndsWith(")") 
					|| defaultValue.StartsWith("\"") && defaultValue.EndsWith("\""))
				{
					defaultValue = defaultValue.Substring(1);
					defaultValue = defaultValue.Substring(0, defaultValue.Length - 1);
				}
				
				if (IsNumericType(column as ColumnSchema))
					defaultValue = defaultValue.TrimEnd('.');
					
				if (defaultValueProperty.DataType == DbType.String)
				{
					// probably a char type.  Let's remove the quotes so parsing is happy
					if (defaultValue.StartsWith("'") && defaultValue.EndsWith("'"))
					{
						defaultValue = defaultValue.Substring(1);
						defaultValue = defaultValue.Substring(0, defaultValue.Length - 1);
					}
		
					//this is probably a custom function, lets handle it sane-like
					if (defaultValue.Contains("()"))
					{
						if ( defaultValue.ToLower() == "getdate()" )
							defaultValue = "DateTime.Now";
						else if ( defaultValue.ToLower() == "newid()" )
							defaultValue = "new Guid()";
						else if ( defaultValue.ToLower() == "getutcdate()" )
							defaultValue = "DateTime.UtcNow";
						else
							return null;
					}
				}

				if (column.NativeType.ToLower() == "real")
				{
					floatConvert = float.Parse(defaultValue);
					if (defaultValue != null)
						return floatConvert.ToString() + "F";
					else
						return null;
				}
				else
				{
					DbType dataType = column.DataType;
					switch (dataType)
					{
						case DbType.AnsiString:
							if (defaultValue != null)
								return "\"" + defaultValue + "\"";
							else
								return null;
			
						case DbType.AnsiStringFixedLength:
							if (defaultValue != null)
								return "\"" + defaultValue + "\"";
							else
								return null;
			
						case DbType.String:
							if (defaultValue != null)
								return "\"" + defaultValue + "\"";
							else
								return null;
			
						case DbType.Boolean:
						
							if (defaultValue != null )
							{
								if (defaultValue == "1") return "true";
								if (defaultValue == "0") return "false";
								if (defaultValue.Trim().ToLower() == "yes") return "true";
								if (defaultValue.Trim().ToLower() == "no") return "false";
								if (defaultValue.Trim().ToLower() == "y") return "true";
								if (defaultValue.Trim().ToLower() == "n") return "false";
								
								boolConvert = bool.Parse(defaultValue);
								return boolConvert.ToString();
							}
							else
								return null;
			
						case DbType.StringFixedLength:
							if (defaultValue != null)
								return "\"" + defaultValue + "\"";
							else
								return null;
			
						case DbType.Guid:
							if (defaultValue == "new Guid()")
								return defaultValue;
								
							guidConvert = new Guid(defaultValue);
							if (defaultValue != null && guidConvert != null)
								return "new Guid(\"" + guidConvert.ToString() + "\")";
							else
								return null;
						case DbType.Xml:
								return null;			
			
						//Answer modified was just 0
						case DbType.Binary:
							return null;
			
						//Answer modified was just 0
						case DbType.Byte:
							if (defaultValue != null )
							{
								byteConvert = byte.Parse(defaultValue);
								return "(byte)" + byteConvert.ToString();
							}
							else
								return null;
			
						case DbType.Currency:
							if (defaultValue != null)
							{
								decimalConvert = decimal.Parse(defaultValue);
								return decimalConvert.ToString() + "m";
							}
							else
								return null;
			
						case DbType.Date:
						case DbType.DateTime:
						
							if (defaultValue == "DateTime.Now")
								return "DateTime.Now";
							if (defaultValue == "DateTime.UtcNow")
								return "DateTime.UtcNow";

							dateConvert = DateTime.Parse(defaultValue);
							if (defaultValue != null )
								return "new DateTime(\"" + dateConvert.ToString() + "\")";
					
							return null;
						
						case DbType.Decimal:
							if (defaultValue != null)
							{
								decimalConvert = decimal.Parse(defaultValue);
								return decimalConvert.ToString() + "m";
							}
							else
								return null;
			
						case DbType.Double:
							if (defaultValue != null)
							{
								floatConvert = float.Parse(defaultValue);
								return floatConvert.ToString() + "f";
							}
							else
								return null;
			
						case DbType.Int16:
							if (defaultValue != null)
							{
								shortConvert = short.Parse(defaultValue);
								return "(short)" + shortConvert.ToString();
							}
							else
								return null;
			
						case DbType.Int32:
							if (defaultValue != null )
							{
								intConvert = int.Parse(defaultValue);
								return "(int)" + intConvert.ToString();
							}
							else
								return null;
			
						case DbType.Int64:
							if (defaultValue != null)
							{
								longConvert = long.Parse(defaultValue);
								return "(long)" + longConvert.ToString();
							}
							else
								return null;
			
						case DbType.Object:
							return null;
			
						case DbType.Single:
							if (defaultValue != null)
							{
								floatConvert = float.Parse(defaultValue);
								return floatConvert.ToString() + "F";
							}
							else
								return null;
			
						case DbType.Time:
							if (defaultValue == "DateTime.Now")
								return defaultValue;
							else if (defaultValue != null)
							{
								dateConvert = DateTime.Parse(defaultValue);
								return "DateTime.Parse(\"" + dateConvert.ToString() + "\")";
							}
							return null;
						case DbType.VarNumeric:
							if (defaultValue != null)
							{	
								decimalConvert = decimal.Parse(defaultValue);
								return decimalConvert.ToString();
							}
							else
								return null;
						//the following won't be used
						//		case DbType.SByte: return "0";
						//		case DbType.UInt16: return "0";
						//		case DbType.UInt32: return "0";
						//		case DbType.UInt64: return "0";
						default: return null;
					}
				}
			}
			catch{}
			return null;
		}
		
		public bool IsLengthType(DataObjectBase column)
		{
			DbType dataType = column.DataType;
			switch (dataType)
			{
				case DbType.AnsiString: 
				case DbType.AnsiStringFixedLength: 
				case DbType.String: 
				case DbType.StringFixedLength: 
				case DbType.Binary: 
					return true;
					
				default: 
						return false;
			}
		}

		/// <summary>
		/// Determines whether base DataObjectBase is a string type
		/// </summary>
		public bool IsStringType(DataObjectBase column)
		{
			DbType dataType = column.DataType;
			switch (dataType)
			{
				case DbType.AnsiString: 
				case DbType.AnsiStringFixedLength: 
				case DbType.String: 
				case DbType.StringFixedLength: 
					return true;
					
				default: 
						return false;
			}
		}
		
		/// <summary>
		/// Determines whether base DataObjectBase is a string type, and not a blob column of text or ntext
		/// </summary>
		public bool CanCheckLength(DataObjectBase column)
		{
			switch (column.DataType)
			{
				case DbType.AnsiString: 
				case DbType.AnsiStringFixedLength: 
				case DbType.String: 
				case DbType.StringFixedLength: 
					return 
					(
						column.NativeType != "text" && 
						column.NativeType != "ntext" && 
						column.Size > 0
					);
					
				default: 
						return false;
			}
		}
		
		
		/// <summary>
		/// Returns true if the column is represented as a reference data type
		/// rather than a value type. In other words, the C# code can set a
		/// column of this data type to \"null\"
		/// </summary>
		public bool IsCSReferenceDataType(DataObjectBase column)
		{
			if (column.NativeType.ToLower() == "real")
				return false;
			else if (column.NativeType.ToLower() == "xml")
				return true;
			else
			{
				DbType dataType = column.DataType;
				switch (dataType)
				{
					case DbType.AnsiString: 
					case DbType.AnsiStringFixedLength: 
					case DbType.String: 
					case DbType.StringFixedLength: 
					case DbType.Binary: 
						return true;
						
					case DbType.Boolean: 
					case DbType.Guid: 
					case DbType.Byte:
					case DbType.Currency: 
					case DbType.Date: 
					case DbType.DateTime: 
					case DbType.Decimal: 
					case DbType.Double:
					case DbType.Int16: 
					case DbType.Int32: 
					case DbType.Int64: 
					case DbType.Object: 
					case DbType.Single: 
					case DbType.Time:
					case DbType.VarNumeric:
						return false;
						
					default: 
						return false;
				}
			}
		}

		
		/// <summary>
		/// Get a mock value for a given data type. Used by the unit test classes.
		/// </summary>
		/// <param name="column">Data type for which to get the default value.</param>
		/// <param name="stringValue">a mock string value.</param>
		/// <param name="bValue">a mock boolean value.</param>
		/// <param name="guidValue">a mock Guid value.</param>
		/// <param name="numValue">a mock numeric value.</param>
		/// <param name="dtValue">a mock datetime value.</param>
		/// <returns>A string representation of the default value.</returns>
		public string GetCSMockValueByType(DataObjectBase column, string stringValue, bool bValue, Guid guidValue, int numValue, DateTime dtValue)
		{	
			if (column.NativeType.ToLower() == "real")
				return numValue.ToString() + "F";
			else if (column.NativeType.ToLower() == "xml")
			{
				return "\"" + "<TEST></TEST>" + "\"";
			}	
			else
			{
				switch (column.DataType)
				{
					case DbType.AnsiString: 
						return "\"" + stringValue + "\"";
						
					case DbType.AnsiStringFixedLength: 
					return "\"" + stringValue + "\"";
					
					case DbType.String: 
						return "\"" + stringValue + "\"";
						
					case DbType.Boolean: 
						return bValue.ToString().ToLower();
					
					case DbType.StringFixedLength: 
						return "\"" + stringValue + "\"";
						
					case DbType.Guid: 
						return "new Guid(\"" + guidValue.ToString() + "\")"; 
					
					
					//Answer modified was just 0
					case DbType.Binary: 
						return "new byte[] {" + numValue.ToString() + "}";
					
					//Answer modified was just 0
					case DbType.Byte:
						return "(byte)" + numValue.ToString() + "";
						//return "{ 0 }";
					
					case DbType.Currency: 
						return numValue.ToString();
					
					case DbType.Date: 
						return string.Format("new DateTime({0}, {1}, {2}, 0, 0, 0, 0)", dtValue.Date.Year, dtValue.Date.Month, dtValue.Date.Day);
					
					case DbType.DateTime: 
						return string.Format("new DateTime({0}, {1}, {2}, {3}, {4}, {5}, {6})", dtValue.Year, dtValue.Month, dtValue.Day, dtValue.Hour, dtValue.Minute, dtValue.Second, dtValue.Millisecond);
					
					case DbType.Decimal: 
						return numValue.ToString() + "m";
						//return "0M";
						//return "0.0M";
					
					case DbType.Double: 
						return numValue.ToString() + ".0f";
					
					case DbType.Int16: 
						return "(short)" + numValue.ToString();
						
					case DbType.Int32: 
						return "(int)" + numValue.ToString();
						
					case DbType.Int64: 
						return "(long)" + numValue.ToString();
					
					case DbType.Object: 
						return "null";
					
					case DbType.Single: 
						return numValue.ToString() + "F";
					
					//case DbType.Time: return "DateTime.MaxValue";
					case DbType.Time: 
						return string.Format("new DateTime({0}, {1}, {2}, {3}, {4}, {5}, {6})", dtValue.Year, dtValue.Month, dtValue.Day, dtValue.Hour, dtValue.Minute, dtValue.Second, dtValue.Millisecond);
						
					case DbType.VarNumeric: 
						return numValue.ToString();
						//the following won't be used
						//		case DbType.SByte: return "0";
						//		case DbType.UInt16: return "0";
						//		case DbType.UInt32: return "0";
						//		case DbType.UInt64: return "0";
					default: return "null";
				}
			}
		}
		
		
		/// <summary>
		/// Generates a random number between the given bounds.
		/// </summary>
		/// <param name="min">lowest bound</param>
		/// <param name="max">highest bound</param>
		public int RandomNumber(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max); 
		}
		
		public string RandomString(ViewColumnSchema column, bool lowerCase)
		{
			//Debugger.Break();
			int size = 2; // default size
			
			// calculate maximum size of the field
			switch (column.DataType)
			{				
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:
				{
					if (column.NativeType != "text" && column.NativeType != "ntext")
					{
						if (column.Size > 0)
						{
							size = column.Size;
						}
						
						if (size > 1000)
						{
							size = 1000;	
						}
					}
					break;
				}
			}
			
			return RandomString((size/2) -1, lowerCase);
		}

		public string RandomString(ColumnSchema column, bool lowerCase)
		{
			//Debugger.Break();
			int size = 2; // default size
			
			// calculate maximum size of the field
			switch (column.DataType)
			{				
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:
				{
					if (column.NativeType != "text" && column.NativeType != "ntext")
					{
						if (column.Size > 0)
						{
							size = column.Size;
						}
						
						if (size > 1000)
						{
							size = 1000;	
						}
					}
					break;
				}
			}
			
			return RandomString((size/2) -1, lowerCase);
		}
		
		/// <summary>
		/// Generates a random string with the given length
		/// </summary>
		/// <param name="size">Size of the string</param>
		/// <param name="lowerCase">If true, generate lowercase string</param>
		/// <returns>Random string</returns>
		/// <remarks>Mahesh Chand  - http://www.c-sharpcorner.com/Code/2004/Oct/RandomNumber.asp</remarks>
		public string RandomString(int size, bool lowerCase)
		{
			StringBuilder builder = new StringBuilder();
			Random random = new Random(size);
			char ch ;
			for(int i=0; i<size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(26 * random.NextDouble() + 65)) ;
				builder.Append(ch); 
			}
			if(lowerCase)
			return builder.ToString().ToLower();
			return builder.ToString();
		}


		/// <summary>
		/// Get the Sql Data type of a column
		/// </summary>
		/// <param name="column">Column for which to get the type</param>
		/// <returns>String representing the SQL data type</returns>
		public string GetSqlDbType(DataObjectBase column)	
		{
			switch (column.NativeType)
			{
				case "bigint": return "BigInt";
				case "binary": return "Binary";
				case "bit": return "Bit";
				case "char": return "Char";
				case "datetime": return "DateTime";
				case "decimal": return "Decimal";
				case "float": return "Float";
				case "image": return "Image";
				case "int": return "Int";
				case "money": return "Money";
				case "nchar": return "NChar";
				case "ntext": return "NText";
				case "numeric": return "Decimal";
				case "nvarchar": return "NVarChar";
				case "real": return "Real";
				case "smalldatetime": return "SmallDateTime";
				case "smallint": return "SmallInt";
				case "smallmoney": return "SmallMoney";
				case "sql_variant": return "Variant";
				case "sysname": return "NChar";
				case "text": return "Text";
				case "timestamp": return "Timestamp";
				case "tinyint": return "TinyInt";
				case "uniqueidentifier": return "UniqueIdentifier";
				case "varbinary": return "VarBinary";
				case "varchar": return "VarChar";
				default: return "__UNKNOWN__" + column.NativeType;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fkey"></param>
		public string FKColumnName(TableKeySchema fkey)
		{
			StringBuilder Name = new StringBuilder();
			for(int x=0;x < fkey.ForeignKeyMemberColumns.Count;x++)
			{
				Name.Append( GetPropertyName(fkey.ForeignKeyMemberColumns[x].Name) );
			}
			return Name.ToString();
		}
		
		
		
		/// <summary>
		/// Build and return a concatened list of columns that are contained in the specified index. (ex: Column1Column2() )
		/// </summary>
		/// <param name="index"> the index instance.</param>
		public string IXColumnName(IndexSchema index)
		{
			StringBuilder Name = new StringBuilder();
			for(int x=0;x < index.MemberColumns.Count;x++)
			{
				Name.Append( GetPropertyName(index.MemberColumns[x].Name) );
			}
			return Name.ToString();
		}
		
		/// <summary>
		/// Build and return a comma separated list of column contained in the specified index. (ex: column1, column2 )
		/// </summary>
		/// <param name="index"> the index instance.</param>
		public string IXColumnNames(IndexSchema index)
		{
			StringBuilder Name = new StringBuilder();
			for(int x=0;x < index.MemberColumns.Count;x++)
			{
				if ( x > 0 )
					Name.Append(", ");

				Name.Append( GetPrivateName(index.MemberColumns[x].Name) );
			}
			return Name.ToString();
		}
		
		/// <summary>
		/// Build and return a concatened list of columns that are contained in the specified key. (ex: Column1Column2() )
		/// </summary>
		/// <param name="keys"> the key instance.</param>
		public string GetKeysName(ColumnSchemaCollection keys)
		{	
			StringBuilder Name = new StringBuilder();
			for(int x=0; x < keys.Count;x++)
			{
				Name.Append( GetPropertyName(keys[x].Name) );
			}
			return Name.ToString();
		}

		/// <summary>
		/// Indicates if the key is containing more than one column.
		/// </summary>
		/// <param name="keys"> <c>true</c> if > 1; false otherwise.</param>
		public bool IsMultiplePrimaryKeys(ColumnSchemaCollection keys)
		{
			if(keys.Count > 1)
				return true;
			return false;
		}
		
		public bool HasCommonColumn(ColumnSchemaCollection cols1, ColumnSchemaCollection cols2)
		{
			foreach(ColumnSchema col1 in cols1)
			{
				foreach(ColumnSchema col2 in cols2)
				{
					if (col1.Equals(col2))
					return true;
				}	
			}
			return false;
		}
		
		/// <summary>
		/// Return a ColumnSchemaCollection of columns that are contained in all of the tables
		/// </summary>
		/// <param name="sourceTables">Tables to search.</param>
		public ColumnSchemaCollection GetCommonTableColumns(TableSchemaCollection sourceTables)
		{
			ColumnSchemaCollection commonColumns = new ColumnSchemaCollection();
			
			if (sourceTables.Count > 0)
			{
				foreach(ColumnSchema col in sourceTables[0].Columns)
				{
					bool isInEveryTable = true;
					
					//System.Diagnostics.Debug.Write (col.Name + ":" + Environment.NewLine);
					
					for (int k = 1; k < sourceTables.Count ; k++)
					{
						TableSchema table = sourceTables[k];
						bool isInThisTable = false;
							
						// scan each column of this table to find this column
						foreach (ColumnSchema tCol in table.Columns)
						{					
							if (col.Name == tCol.Name && col.SystemType == tCol.SystemType && col.AllowDBNull == tCol.AllowDBNull)
							{
								isInThisTable= true;
							}
						}
						
						//System.Diagnostics.Debug.Write ("\t" + table.Name + " : " + isInThisTable.ToString() + Environment.NewLine);					
						isInEveryTable = isInEveryTable && isInThisTable;			
					}
										
					// If this column is present in every table, put it in the IEnity interface.
					if (isInEveryTable)
					{
						commonColumns.Add(col);
					}
				}
				
			}
			return commonColumns;
		}
		
		/// <summary>
		/// Check a table for enum eligibility
		/// </summary>
		/// <param name="table">the table instance to check.</param>
		/// <exception name="ApplicationException"/>
		public void ValidForEnum(TableSchema table)
		{
			#region "Primary key validation"
			
			// No primary key
			if (!HasPrimaryKey(table))
			{
				throw new ApplicationException("table has no primary key.");
			}
			
			// Multiple column in primary key
			if (table.PrimaryKey.MemberColumns.Count != 1)
			{
				throw new ApplicationException("table primary key contains more than one column.");
			}
			
			// Primary key column is not an integer
			if (!isIntXX(table.PrimaryKey.MemberColumns[0]))
			{
				throw new ApplicationException("table primary key column is not an integer. (used for enum value)");
			}
			
			#endregion
			
			#region "Second column must be a string"
			
			// The table must have 2 columns at least
			if (table.Columns.Count < 2)
			{
				throw new ApplicationException("table must at least contains two columns, an integer primary key, and a string.");
			}
			
			// The second column must be a string (char, varchar) 
			if (table.Columns[1].SystemType != typeof(string))
			{
				throw new ApplicationException("table 2nd column must be a string.");
			}
						
			// The second column must have a unique constraint (index with unique constraint)
			if (!table.Columns[1].IsUnique)
			{
				throw new ApplicationException("table 2nd column must be unique (used for the enum label).");
			}
									
			#endregion
			
			#region "Check relations"
			// the table mustn't have foreign relation
			//if (table.ForeignKeys.Count > 0)
			//{
			//	throw new ApplicationException("table cannot have relations where it is the foreign table.");
			//}
			
			// relation with table as primary key can only be on the first column 
			foreach(TableKeySchema key in table.PrimaryKeys)
			{
				if (key.PrimaryKeyMemberColumns[0].Name != table.Columns[0].Name || key.PrimaryKeyMemberColumns.Count > 1)
				{
					throw new ApplicationException("table cannot have relations where it is the foreign table.");
				}
				
				
			}
			
			#endregion
		}
	
		/// <summary>
		/// Indicates if the output rowset of the command is compliant with the table rowset.
		/// </summary>
		/// <param name="command">The stored procedure</param>
		/// <param name="table">The table</param>
		public bool IsMatching(CommandSchema command, TableSchema table)
		{
			try
			{
				if (command.CommandResults.Count == 0)
					return false;
					
				if (command.CommandResults[0].Columns.Count != table.Columns.Count)
				{
					return false;
				}
				
				for(int i=0; i<table.Columns.Count; i++)
				{
					if (IsComputed(table.Columns[i]))
						continue;
				
					if (!command.CommandResults[0].Columns.Contains(table.Columns[i].Name.ToLower()))
					{
						return false;
					}
					
					// manage the xml column type separately
					if ( table.Columns[i].NativeType == "xml" && (command.CommandResults[0].Columns[i].NativeType == "sql_variant" || command.CommandResults[0].Columns[i].NativeType == "ntext"))
					{
						continue;
					}
					else if (!SqlTypesAreEquivalent(command.CommandResults[0].Columns[i].NativeType, table.Columns[i].NativeType))
					{
						return false;
					}
				}
				return true;
			}	
			catch(Exception ec)
			{
				System.Diagnostics.Debug.WriteLine("Procedure Threw Exception: " + command.Name);
				return false;	
			}
		}
		
		/// <summary>
		/// Indicates if the output rowset of the command is compliant with the view rowset.
		/// </summary>
		/// <param name="command">The stored procedure</param>
		/// <param name="view">The view</param>
		public bool IsMatching(CommandSchema command, ViewSchema view)
		{
			try
			{
				if (command.CommandResults.Count == 0)
					return false;
					
				if (command.CommandResults[0].Columns.Count != view.Columns.Count)
				{
					return false;
				}
				
				for(int i=0; i<view.Columns.Count; i++)
				{	
					if (!command.CommandResults[0].Columns.Contains(view.Columns[i].Name.ToLower()))
					{
						return false;
					}
					
					// manage the xml column type separately
					if ( view.Columns[i].NativeType == "xml" && (command.CommandResults[0].Columns[i].NativeType == "sql_variant" || command.CommandResults[0].Columns[i].NativeType == "ntext"))
					{
						continue;
					}
					else if (!SqlTypesAreEquivalent(command.CommandResults[0].Columns[i].NativeType, view.Columns[i].NativeType))
					{
						return false;
					}
				}
				return true;
			}	
			catch(Exception ec)
			{
				System.Diagnostics.Debug.WriteLine("!!ERROR!! - Procedure Threw Exception: " + command.Name);
				return false;	
			}
		}
		
		/// <summary>
		/// Compares two sql types and determines if they are syntax equivalent.
		/// </summary>
		/// <param name="type1">The first sql type to compare.</param>
		/// <param name="type2">The second sql type to compare.</param>
		public bool SqlTypesAreEquivalent(string type1, string type2)
		{
			type1 = type1.ToLower();
			type2 = type2.ToLower();
			
			if ((type1 == "numeric" && type2 == "decimal") || (type2 == "numeric" && type1 == "decimal"))
			{
				return true;
			}
			else if ((type1 == "varchar" && type2 == "nvarchar") || (type2 == "varchar" && type2 == "nvarchar"))
			{
					return true;   
			}
			return (type1 == type2);
		}
		

		public bool isIntXX(DataObjectBase column)
		{
			bool result = false;

			for(int i = 0; i < aIntegerDbTypes.Length; i++)
			{
				if (aIntegerDbTypes[i] == column.DataType) result=true;
			}
			
			return result;
		}

		/// <summary>
		///	Indicates if a column is an int.
		/// </summary>
		/// <author>ab</author>
		/// <date>01/26/05</date>
		public bool isIntXX(ColumnSchema column)
		{
			bool result = false;

			for(int i = 0; i < aIntegerDbTypes.Length; i++)
			{
				if (aIntegerDbTypes[i] == column.DataType) result=true;
			}
			
			return result;		
		}
		
		#region Long Line Wrapping Handling
		// EntityBase.cst and EntityCollectionBase.cst render constructs with every column
		// in a table as arguments.  For very long tables, the C# compliler complains with
		// "CS1034: Compiler limit exceeded: Line cannot exceed 2046 characters"
		// Data warehouses can have very long tables.
		
		/// <summary>
		/// Stores the current column were are at.
		/// </summary>
		private int wrapCurrentColumn;
		
		/// <summary>
		/// Inititalizes the line wrapping to column 50.
		/// </summary>
		protected void WrapInit()
		{
			wrapCurrentColumn = 50;
		}

		/// <summary>
		/// Increment the wrap column by the normal amount.
		/// </summary>
		/// <remarks>
		/// This is not meant to be exact, rough estimate only.  This is called by
		/// EntityBase.cst and EntityCollectionBase.cst.
		/// </remarks>
		protected void WrapIncr(ColumnSchema column)
		{
			wrapCurrentColumn += GetCSType(column).Length + 1 /*space*/ + column.Table.Name.Length + GetPropertyName(column.Name).Length + 2; /*comma, space*/;
		}

		/// <summary>
		/// Wrap the line of text if the line exceeds 130 columns long.
		/// </summary>
		/// <remarks>
		/// CS1034: Compiler limit exceeded: Line cannot exceed 2046 characters
		/// </remarks>
		protected void WrapLine(int indentLevel)
		{
			if ( wrapCurrentColumn >= 130 ) // keep this reasonable, people do like printing code too
			{
				Response.Write(Environment.NewLine);
				for (int i = 0; i < indentLevel; i++)
					Response.Write("\t");
				wrapCurrentColumn = indentLevel * 4; // most people use 4 space tabs
			}
		}
		#endregion
		
		#region Column Comparer
		// [ab 013105] column name sorting comparer
		public class columnSchemaComparer : IComparer  
		{
	      	int IComparer.Compare( Object x, Object y )  
			{
				if (x is ColumnSchema && y is ColumnSchema)
	          		return( (new CaseInsensitiveComparer()).Compare( ((ColumnSchema)x).Name,  ((ColumnSchema)y).Name ) );
					
				throw new ArgumentException("one or both object(s) are not of type ColumnSchema");
			}
				
      	}
      	#endregion
		
		#region Utils
		
		#region Recursive Copy Code
		///<summary>
		/// Safely Copies all files from one directory to another
		///</summary>
		public void SafeCopyAll(string path, string destination) 
		{ 
			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path); 
			SafeCopyAll(dir, destination); 
		} 
		
		///<summary>
		/// Safely Copies all files from one directory to another
		///</summary>
		public void SafeCopyAll(System.IO.DirectoryInfo dir, string destination) 
		{ 
			string path; 
			foreach ( System.IO.FileInfo f in dir.GetFiles() ) 
			{ 
				f.CopyTo(System.IO.Path.Combine(destination, f.Name), true); 
			} 
			
			foreach ( System.IO.DirectoryInfo d in dir.GetDirectories() ) 
			{ 
				path = System.IO.Path.Combine(destination, d.Name); 
				SafeCreateDirectory(path); 
				SafeCopyAll(d, path); 
			} 
		} 
		
		/// <summary>
		/// Copy the specified file.
		/// </summary>
		public void SafeCreateDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		
		/// <summary>
		/// Copy the specified file.
		/// </summary>
		public void SafeCopyFile(string path, string destination)
		{
			FileInfo file1 = new FileInfo(path);
			file1.CopyTo(destination, true);
		}

		#endregion 
		
		#region Recursive Get Files
		///<summary>
		/// Get's all available files with the proper extensions for inclusion into a project
		/// NOTE: Not Tested
		///</summary>
		public System.Collections.IList  GetFiles(string path) 
		{ 
				System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path); 
			
			return GetFiles(dir, new System.Collections.ArrayList()); 
		} 
		
		///<summary>
		/// Get's all available files with the proper extensions for inclusion into a project
		/// NOTE: Not Tested
		///</summary>
		public System.Collections.IList GetFiles(System.IO.DirectoryInfo dir, System.Collections.ArrayList files) 
		{ 
			string path; 
			foreach (System.IO.FileInfo f in dir.GetFiles() ) 
			{
				if (Array.IndexOf(IncludeExtensions, f.Extension) >= 0)
					files.Add(f);
			} 
			
			foreach (System.IO.DirectoryInfo d in dir.GetDirectories() ) 
			{ 
				path = System.IO.Path.Combine(dir.FullName, d.Name); 
				files.AddRange(GetFiles(d, files)); 
			} 
			
			return files;
		} 
		#endregion 
		
      	#region File Extensions
		private static string[] IncludeExtensions = new string[]{".arj", ".asa",".asax", ".ascx", ".asmx", ".ashx", ".asp", ".aspx", ".au", ".avi", ".bat", ".bmp", 
													  ".cab", ".chm", ".com", ".config", ".cs", ".css", ".disco", ".dll", ".doc", 
													  ".exe", ".png", ".gif", ".hlp", ".htm", ".html", ".jpg", ".inc", ".ini", 
													  ".log", ".mdb", ".mid", ".midi", ".mov", ".mp3", ".mpg", ".mpeg", ".fla", ".swf",
													  ".cur", ".ico", ".resx", ".jsl", ".cd", ".rdlc", ".js", ".vbs", ".wsf", ".master", 
													  ".skin", ".pdf", ".ppt", ".psd", ".sys", ".txt", ".tif", ".vb", ".vbs", ".vsdisco", 
													  ".wav", ".wri", ".xls", ".xml", ".xsd", ".xslt", ".zip", ".rpt", ".java",
													  ".settings", ".cfm", ".cfmx", ".jsp", ".mdf", ".ldf" };
													
		#endregion 
		
		#endregion 
		
		#region Custom Stored Procedures
		
		public IDictionary GetCustomProcedures(TableSchema table)
		{
			return GetCustomProcedures(table.Name, table.Database.Commands);
		}
		
		public IDictionary GetCustomProcedures(ViewSchema view)
		{
			return GetCustomProcedures(view.Name, view.Database.Commands);
		}
		
		public IDictionary GetCustomProcedures(string objectName, CommandSchemaCollection allCommands)
		{		
			string customPrefix = string.Format(CustomProcedureStartsWith, objectName, ProcedurePrefix);
			IDictionary procs = new Hashtable();
			string customName;
			bool discover = true;
			System.Collections.ArrayList invalids = new System.Collections.ArrayList();
			string current = string.Empty;
			
			while(discover)
			{
				try
				{
					procs.Clear();
					foreach ( CommandSchema proc in allCommands )
					{
						if (proc == null)
							continue;
							
						current = proc.Name;
						if (invalids.Contains(proc.Name))
							continue;
							
						if ( proc.Name.ToLower().StartsWith(customPrefix.ToLower()) )
						{
							customName = proc.Name.Substring(customPrefix.Length);
							procs.Add(customName, proc);
						}
					}
					discover = false;
				}
				catch(Exception ec)
				{
					System.Diagnostics.Debug.WriteLine("Stored Procedure Command Failed: " + current);	
					invalids.Add(current);
				}	
			}
		
			return procs;
		}

		public string GetReturnCustomProcReturnType(CustomNonMatchingReturnType nonMatchingReturnType, SchemaExplorer.TableSchema table, SchemaExplorer.CommandSchema command)
		{
			string returnType = "void";
			try
			{
				if (IsMatching(command, table))
				{
					returnType = GetCollectionClassName(table.Name);
				}
				else if (command.CommandResults != null && command.CommandResults.Count > 0)
				{
					returnType = nonMatchingReturnType.ToString();				
				}
			}
			catch(Exception ec)
			{
				System.Diagnostics.Debug.WriteLine("!!ERROR!!: Could not get return type from " + command.Name);	
			}	
			return returnType;	
		}

		
		public string GetReturnCustomProcReturnType(CustomNonMatchingReturnType nonMatchingReturnType, SchemaExplorer.ViewSchema view, SchemaExplorer.CommandSchema command)
		{
			string returnType = "void";
			try
			{
				if (IsMatching(command, view))
				{
					returnType = GetViewCollectionClassName(view.Name);
				}
				else if (command.CommandResults != null && command.CommandResults.Count > 0)
				{
					returnType = nonMatchingReturnType.ToString();
				}
			}
			catch(Exception ec)
			{
				System.Diagnostics.Debug.WriteLine("!!ERROR!!: Could not get return type from " + command.Name);	
			}	
			
			return returnType;	
		}

		public string GetCustomVariableName(string paramName, SchemaExplorer.CommandSchema command)
		{
			int c = 1;
			try
			{
				for(;c < command.Database.Commands.Count; c++)
				{
					CommandSchema tmp = command.Database.Commands[c];
					
					if (tmp.Name == command.Name)
						break;
				}
			} catch{}
			
			return string.Format("sp{1}_{0}", GetPropertyName(paramName), c);
		}
		
		#endregion 
      	
		#region Execute sql file

		public void ExecuteSqlInFile(string pathToScriptFile, string connectionString ) 
		{
			SqlConnection connection;

			StreamReader _reader			= null;

			string sql	= "";

			if( false == System.IO.File.Exists( pathToScriptFile )) 
			{
				throw new Exception("File " + pathToScriptFile + " does not exists");
			}
			using( Stream stream = System.IO.File.OpenRead( pathToScriptFile ) ) 
			{
				_reader = new StreamReader( stream );

				connection = new SqlConnection(connectionString);

				SqlCommand	command = new SqlCommand();

				connection.Open();
				command.Connection = connection;
				command.CommandType	= System.Data.CommandType.Text;

				while( null != (sql = ReadNextStatementFromStream( _reader ) )) 
				{
					command.CommandText = sql;

					command.ExecuteNonQuery();
				}

				_reader.Close();
			}
			connection.Close();			
		}


		private static string ReadNextStatementFromStream( StreamReader _reader ) 
		{			
			StringBuilder sb = new StringBuilder();

			string lineOfText;

			while(true) 
			{
				lineOfText = _reader.ReadLine();
				if( lineOfText == null ) 
				{

					if( sb.Length > 0 ) 
					{
						return sb.ToString();
					}
					else 
					{
						return null;
					}
				}

				if( lineOfText.TrimEnd().ToUpper() == "GO" ) 
				{
					break;
				}
			
				sb.Append(lineOfText + Environment.NewLine);
			}

			return sb.ToString();
		}

		#endregion
		
		#region Children Collections
		
		/////////////////////////////////////////////////////////////////////////////////////
		/// Begin Children Collection 
		/////////////////////////////////////////////////////////////////////////////////////
		
		///<summary>
		///  An ArrayList of all the child collections for this table.
		///</summary>
		private System.Collections.ArrayList _collections = new System.Collections.ArrayList();
		
		///<summary>
		///  An ArrayList of all the properties rendered.  
		///  Eliminate Dupes through common junction tables and fk relationships
		///</summary>
		private System.Collections.Hashtable relationshipDictionary = new System.Collections.Hashtable();
		
		///<summary>
		///	Returns an array list of Child Collections of the object
		///</summary>
		public System.Collections.Hashtable GetChildrenCollections0(SchemaExplorer.TableSchema table, SchemaExplorer.TableSchemaCollection tables) 
		{
			//System.Diagnostics.Debugger.Break();
			
			///  An ArrayList of all the child collections for this table.
			System.Collections.Hashtable _collections = new System.Collections.Hashtable();
		
			CurrentTable = table.Name;
			
			//Check Cache
			if( relationshipDictionary[table.Name] == null )
			{
				relationshipDictionary[table.Name] = _collections;
			}
			else 
			{
				return relationshipDictionary[table.Name] as System.Collections.Hashtable;
			}
	
			//Provides Informatoin about the foreign keys
			SchemaExplorer.TableKeySchemaCollection fkeys = table.ForeignKeys;
			
			//Provides information about the indexes contained in the table. 
			IndexSchemaCollection indexes = table.Indexes;
			
			// Begin -- Fix for TableSchema.PrimaryKeys issue 2006-09-21 mwerner
			// Fix to generate code for recursive relations for a table
			
			// All keys that relate to this table
			TableKeySchemaCollection primaryKeyCollection = new TableKeySchemaCollection();
			primaryKeyCollection.AddRange(table.PrimaryKeys);
			
			// Add missing item to primaryKeyCollection 			
			foreach(TableKeySchema keyschema in fkeys)
			{
				if (keyschema.ForeignKeyTable.Equals(table) && keyschema.PrimaryKeyTable.Equals(table))
				{
					bool found = false;
					
					foreach(TableKeySchema primaryKey in primaryKeyCollection)
					{
						if (keyschema.Equals(primaryKey))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						primaryKeyCollection.Add(keyschema);
					}					
				}
			}
			
			// End -- Fix for TableSchema.PrimaryKeys issue 2006-09-21 mwerner
			
			
			//for each relationship
			foreach(TableKeySchema keyschema in primaryKeyCollection)
			{
				
				// add the relationship only if the linked table is part of the selected tables (ie: omit tables without primary key)
				if (!tables.Contains(keyschema.ForeignKeyTable.Owner, keyschema.ForeignKeyTable.Name))
				{	//BYDAN-RECURSIVO				
					//continue;
				}
				
				if (IsRelationOneToOne(keyschema))
				{
					//System.Windows.Forms.MessageBox.Show("1 a 1");
					CollectionInfo collectionInfo = new CollectionInfo();

					#region Additional 1:1 meta-data properties
					collectionInfo.PkColNames = GetColumnNames(table.PrimaryKey.MemberColumns);
					collectionInfo.PrimaryTable = GetClassName(table);
					collectionInfo.SecondaryTable = GetClassName(keyschema.ForeignKeyTable);
					collectionInfo.SecondaryTablePkColNames = GetColumnNames(keyschema.ForeignKeyTable.PrimaryKey.MemberColumns);
					collectionInfo.CollectionRelationshipType = RelationshipType.OneToOne;			
					collectionInfo.CollectionName = GetCollectionPropertyName(collectionInfo.SecondaryTable);
					collectionInfo.CollectionTypeName = GetCollectionClassName(collectionInfo.SecondaryTable);
					collectionInfo.TableKey = keyschema;
					collectionInfo.CleanName = GetClassName(collectionInfo.SecondaryTable);
					collectionInfo.SecondaryTableSchema=keyschema.ForeignKeyTable;
					
					#endregion 

					//Key Name - Each collection should have a unique key namce
					collectionInfo.PkIdxName = keyschema.Name;
										
					// Method to fill this entity
					collectionInfo.GetByKeysName = "GetBy" + GetKeysName(keyschema.ForeignKeyMemberColumns);
					
					// Params to fill this entity
					collectionInfo.CallParams = GetFunctionRelationshipCallParameters(keyschema.PrimaryKeyMemberColumns);
					
					// Property String Name for a this relationship
					collectionInfo.PropertyName = GetClassName(collectionInfo.SecondaryTable);

					// Property String Name for a this relationship
					collectionInfo.PropertyNameUnique = GetClassName(collectionInfo.SecondaryTable);

					// Property Type for this relationship
					collectionInfo.TypeName = GetClassName(collectionInfo.SecondaryTable);
					
					// Field Variable String
					collectionInfo.FieldName = GetPrivateName(collectionInfo.SecondaryTable) + GetKeysName(keyschema.ForeignKeyMemberColumns);

					AddToCollection(_collections, collectionInfo);
				}
				//Add 1-N,N-1 relations
				else
				{
					CollectionInfo collectionInfo = new CollectionInfo();
					//System.Windows.Forms.MessageBox.Show("1 a M");
					
					#region Additional 1:N meta-data properties
					
					collectionInfo.PkColNames = GetColumnNames(table.PrimaryKey.MemberColumns);
					collectionInfo.PrimaryTable = GetClassName(table);
					collectionInfo.SecondaryTable = GetClassName(keyschema.ForeignKeyTable);
					collectionInfo.SecondaryTableSchema = keyschema.ForeignKeyTable;							
					collectionInfo.SecondaryTablePkColNames = GetColumnNames(keyschema.ForeignKeyTable.PrimaryKey.MemberColumns);
					collectionInfo.CollectionRelationshipType = RelationshipType.OneToMany;
					collectionInfo.CollectionName = GetCollectionPropertyName(collectionInfo.SecondaryTable);
					collectionInfo.TableKey = keyschema;
					collectionInfo.CleanName = GetClassName(collectionInfo.SecondaryTable); 
					collectionInfo.CollectionTypeName = GetCollectionClassName(collectionInfo.SecondaryTable);
					
					#endregion 
					
					//Key Name - Each collection should have a unique key namce
					collectionInfo.PkIdxName = keyschema.Name;
					
					
					// Gets Method Calls
					if (IsForeignKeyCoveredByIndex(keyschema))
					{
						IndexSchema idx = GetIndexCoveringForeignKey(keyschema);
						
						// Method to fill this entity
						collectionInfo.GetByKeysName = "GetBy" + GetKeysName(idx.MemberColumns);

						// Params to fill this entity
						collectionInfo.CallParams = GetFunctionRelationshipCallParametersInKeyOrder(idx.MemberColumns, keyschema);
					}
					else
					{
						// Method to fill this entity
						collectionInfo.GetByKeysName = "GetBy" + GetKeysName(keyschema.ForeignKeyMemberColumns);
						
						// Params to fill this entity
						collectionInfo.CallParams = GetFunctionRelationshipCallParameters(keyschema.PrimaryKeyMemberColumns);
					}	

					// Usually the same as the property string
					collectionInfo.PropertyName = GetCollectionPropertyName(collectionInfo.SecondaryTable);

					// Usually the same as the property string
					collectionInfo.PropertyNameUnique = GetCollectionPropertyName(collectionInfo.SecondaryTable);

					// Usually the same as the property type
					collectionInfo.TypeName = GetCollectionClassName(collectionInfo.SecondaryTable);

					// Field Variable String
					collectionInfo.FieldName = GetPrivateName(collectionInfo.SecondaryTable) + GetKeysName(keyschema.ForeignKeyMemberColumns);

					AddToCollection(_collections, collectionInfo);
				}
			}

			//Add N-N relations
			foreach(SchemaExplorer.TableKeySchema key in primaryKeyCollection)
			{
				// Check that the key is related to a junction table and that this key relate a PK in this junction table
				
				//System.Windows.Forms.MessageBox.Show(table.Name+" M a M:"+key.ForeignKeyTable.Name);
				
				/*
				if(tables.Contains(key.ForeignKeyTable.Owner, key.ForeignKeyTable.Name))
				{
					System.Windows.Forms.MessageBox.Show(table.Name+" si 1");		
				}
				
				
				if(IsJunctionTable(key.ForeignKeyTable))
				{
					System.Windows.Forms.MessageBox.Show(table.Name+" si 2: IsJunctionTable:"+key.ForeignKeyTable.Name);		
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("No 2:"+key.ForeignKeyTable.Name);
				}
				*/
				//BYDAN_NETTIERS
				
				/*
				if(IsJunctionKey(key))
				{
					System.Windows.Forms.MessageBox.Show(table.Name+" si 3: IsJunctionKey:"+key.ForeignKeyTable.Name);		
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("No 3");
				}
				*/
				
				if ( tables.Contains(key.ForeignKeyTable.Owner, key.ForeignKeyTable.Name) &&  IsJunctionTable(key.ForeignKeyTable) && IsJunctionKey(key))
				{
					//System.Windows.Forms.MessageBox.Show("OK:  M a M");
					TableSchema junctionTable = key.ForeignKeyTable;
					
					// Search for the other(s) key(s) of the junction table' primary key
					foreach(TableKeySchema junctionTableKey in junctionTable.ForeignKeys)
					{				
						if ( tables.Contains(junctionTableKey.ForeignKeyTable.Owner, junctionTableKey.ForeignKeyTable.Name) && IsJunctionKey(junctionTableKey) && junctionTableKey.Name != key.Name )
						{
							//System.Windows.Forms.MessageBox.Show("OK2:  M a M");
							
							TableSchema secondaryTable = junctionTableKey.PrimaryKeyTable;
																			
							CollectionInfo collectionInfo = new CollectionInfo();
					
							#region Additional M:M meta-data 
							collectionInfo.PkColNames = GetColumnNames(table.PrimaryKey.MemberColumns);
							collectionInfo.PrimaryTable = GetClassName(table);
							collectionInfo.SecondaryTable = GetClassName(junctionTableKey.PrimaryKeyTable);
							collectionInfo.SecondaryTablePkColNames = GetColumnNames(junctionTableKey.PrimaryKeyTable.PrimaryKey.MemberColumns);
							collectionInfo.JunctionTableSchema = junctionTable;
							collectionInfo.SecondaryTableSchema = junctionTableKey.PrimaryKeyTable;
							collectionInfo.PrimaryTableSchema = table;
							collectionInfo.JunctionTable = GetClassName(junctionTable);
							collectionInfo.JunctionTablePkColNames = GetColumnNames(junctionTable.PrimaryKey.MemberColumns);
							collectionInfo.CollectionName = string.Format("{0}_From_{1}", GetCollectionPropertyName( collectionInfo.SecondaryTable), GetClassName(collectionInfo.JunctionTable)); //GetManyToManyName(GetCollectionClassName( collectionInfo.SecondaryTable), collectionInfo.JunctionTable);
							collectionInfo.CollectionTypeName = GetCollectionClassName( collectionInfo.SecondaryTable);
							collectionInfo.CollectionRelationshipType = RelationshipType.ManyToMany;
							collectionInfo.FkColNames = GetColumnNames(secondaryTable.PrimaryKey.MemberColumns);
							collectionInfo.TableKey = key;		
							collectionInfo.CleanName = string.Format(manyToManyFormat, GetClassName(collectionInfo.SecondaryTable), GetClassName(junctionTable.Name)); 
							#endregion 
							
							//Key Name - Each collection should have a unique key name
							collectionInfo.PkIdxName = junctionTableKey.Name;
							
							// Property Name
							collectionInfo.PropertyName = string.Format("{0}_From_{1}", GetCollectionPropertyName( collectionInfo.SecondaryTable), GetClassName(collectionInfo.JunctionTable)); 

							// Uninque Property Name, in case of conflict
							collectionInfo.PropertyNameUnique = string.Format("{0}_From_{1}", GetCollectionPropertyName( collectionInfo.SecondaryTable), GetClassName(collectionInfo.JunctionTable)); 

							// Field Variable String
							collectionInfo.FieldName = GetManyToManyName(key, GetCleanName(junctionTable.Name)).Substring(5);
							
							// Property/Field Type Name
							collectionInfo.TypeName = GetCollectionClassName(collectionInfo.SecondaryTable);
							
							//Method Params
							collectionInfo.CallParams = GetFunctionRelationshipCallParameters(table.PrimaryKey.MemberColumns);
							
							//Method Name
							collectionInfo.GetByKeysName = GetManyToManyName(key, GetCleanName(junctionTable.Name));
							
							AddToCollection(_collections, collectionInfo);
						}
					}
				}
			}// end N-N relations
			
			return _collections; 
		}
		
		public System.Collections.Hashtable GetChildrenCollections(SchemaExplorer.TableSchema table, SchemaExplorer.TableSchemaCollection tables) 
		{
			//System.Diagnostics.Debugger.Break();
			
			///  An ArrayList of all the child collections for this table.
			System.Collections.Hashtable _collections = new System.Collections.Hashtable();
		
			CurrentTable = table.Name;
			
			//Check Cache
			if( relationshipDictionary[table.Name] == null )
			{
				relationshipDictionary[table.Name] = _collections;
			}
			else 
			{
				return relationshipDictionary[table.Name] as System.Collections.Hashtable;
			}
	
			//Provides Informatoin about the foreign keys
			SchemaExplorer.TableKeySchemaCollection fkeys = table.ForeignKeys;
			
			//Provides information about the indexes contained in the table. 
			IndexSchemaCollection indexes = table.Indexes;
			
			// Begin -- Fix for TableSchema.PrimaryKeys issue 2006-09-21 mwerner
			// Fix to generate code for recursive relations for a table
			
			// All keys that relate to this table
			TableKeySchemaCollection primaryKeyCollection = new TableKeySchemaCollection();
			primaryKeyCollection.AddRange(table.PrimaryKeys);
			
			// Add missing item to primaryKeyCollection 			
			foreach(TableKeySchema keyschema in fkeys)
			{
				if (keyschema.ForeignKeyTable.Equals(table) && keyschema.PrimaryKeyTable.Equals(table))
				{
					bool found = false;
					
					foreach(TableKeySchema primaryKey in primaryKeyCollection)
					{
						if (keyschema.Equals(primaryKey))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						primaryKeyCollection.Add(keyschema);
					}					
				}
			}
			
			// End -- Fix for TableSchema.PrimaryKeys issue 2006-09-21 mwerner
			
			
			//for each relationship
			foreach(TableKeySchema keyschema in primaryKeyCollection)
			{
				// add the relationship only if the linked table is part of the selected tables (ie: omit tables without primary key)
				if (!tables.Contains(keyschema.ForeignKeyTable.Owner, keyschema.ForeignKeyTable.Name))
				{					
					continue;
				}
				if (IsRelationOneToOne(keyschema))
				{
					//System.Windows.Forms.MessageBox.Show("1 a 1");
					CollectionInfo collectionInfo = new CollectionInfo();

					#region Additional 1:1 meta-data properties
					collectionInfo.PkColNames = GetColumnNames(table.PrimaryKey.MemberColumns);
					collectionInfo.PrimaryTable = GetClassName(table);
					collectionInfo.SecondaryTable = GetClassName(keyschema.ForeignKeyTable);
					collectionInfo.SecondaryTablePkColNames = GetColumnNames(keyschema.ForeignKeyTable.PrimaryKey.MemberColumns);
					collectionInfo.CollectionRelationshipType = RelationshipType.OneToOne;			
					collectionInfo.CollectionName = GetCollectionPropertyName(collectionInfo.SecondaryTable);
					collectionInfo.CollectionTypeName = GetCollectionClassName(collectionInfo.SecondaryTable);
					collectionInfo.TableKey = keyschema;
					collectionInfo.CleanName = GetClassName(collectionInfo.SecondaryTable);
					collectionInfo.SecondaryTableSchema=keyschema.ForeignKeyTable;
					#endregion 

					//Key Name - Each collection should have a unique key namce
					collectionInfo.PkIdxName = keyschema.Name;
										
					// Method to fill this entity
					collectionInfo.GetByKeysName = "GetBy" + GetKeysName(keyschema.ForeignKeyMemberColumns);
					
					// Params to fill this entity
					collectionInfo.CallParams = GetFunctionRelationshipCallParameters(keyschema.PrimaryKeyMemberColumns);
					
					// Property String Name for a this relationship
					collectionInfo.PropertyName = GetClassName(collectionInfo.SecondaryTable);

					// Property String Name for a this relationship
					collectionInfo.PropertyNameUnique = GetClassName(collectionInfo.SecondaryTable);

					// Property Type for this relationship
					collectionInfo.TypeName = GetClassName(collectionInfo.SecondaryTable);
					
					// Field Variable String
					collectionInfo.FieldName = GetPrivateName(collectionInfo.SecondaryTable) + GetKeysName(keyschema.ForeignKeyMemberColumns);

					AddToCollection(_collections, collectionInfo);
				}
				//Add 1-N,N-1 relations
				else
				{
					CollectionInfo collectionInfo = new CollectionInfo();
					//System.Windows.Forms.MessageBox.Show("1 a M");
					
					#region Additional 1:N meta-data properties
					
					collectionInfo.PkColNames = GetColumnNames(table.PrimaryKey.MemberColumns);
					collectionInfo.PrimaryTable = GetClassName(table);
					collectionInfo.SecondaryTable = GetClassName(keyschema.ForeignKeyTable);
					collectionInfo.SecondaryTableSchema = keyschema.ForeignKeyTable;
					collectionInfo.SecondaryTablePkColNames = GetColumnNames(keyschema.ForeignKeyTable.PrimaryKey.MemberColumns);
					collectionInfo.CollectionRelationshipType = RelationshipType.OneToMany;
					collectionInfo.CollectionName = GetCollectionPropertyName(collectionInfo.SecondaryTable);
					collectionInfo.TableKey = keyschema;
					collectionInfo.CleanName = GetClassName(collectionInfo.SecondaryTable); 
					collectionInfo.CollectionTypeName = GetCollectionClassName(collectionInfo.SecondaryTable);
					
					#endregion 
					
					//Key Name - Each collection should have a unique key namce
					collectionInfo.PkIdxName = keyschema.Name;
					
					
					// Gets Method Calls
					if (IsForeignKeyCoveredByIndex(keyschema))
					{
						IndexSchema idx = GetIndexCoveringForeignKey(keyschema);
						
						// Method to fill this entity
						collectionInfo.GetByKeysName = "GetBy" + GetKeysName(idx.MemberColumns);

						// Params to fill this entity
						collectionInfo.CallParams = GetFunctionRelationshipCallParametersInKeyOrder(idx.MemberColumns, keyschema);
					}
					else
					{
						// Method to fill this entity
						collectionInfo.GetByKeysName = "GetBy" + GetKeysName(keyschema.ForeignKeyMemberColumns);
						
						// Params to fill this entity
						collectionInfo.CallParams = GetFunctionRelationshipCallParameters(keyschema.PrimaryKeyMemberColumns);
					}	

					// Usually the same as the property string
					collectionInfo.PropertyName = GetCollectionPropertyName(collectionInfo.SecondaryTable);

					// Usually the same as the property string
					collectionInfo.PropertyNameUnique = GetCollectionPropertyName(collectionInfo.SecondaryTable);

					// Usually the same as the property type
					collectionInfo.TypeName = GetCollectionClassName(collectionInfo.SecondaryTable);

					// Field Variable String
					collectionInfo.FieldName = GetPrivateName(collectionInfo.SecondaryTable) + GetKeysName(keyschema.ForeignKeyMemberColumns);

					AddToCollection(_collections, collectionInfo);
				}
			}

			//Add N-N relations
			foreach(SchemaExplorer.TableKeySchema key in primaryKeyCollection)
			{
				// Check that the key is related to a junction table and that this key relate a PK in this junction table
				
				//System.Windows.Forms.MessageBox.Show(table.Name+" M a M:"+key.ForeignKeyTable.Name);
				
				/*
				if(tables.Contains(key.ForeignKeyTable.Owner, key.ForeignKeyTable.Name))
				{
					System.Windows.Forms.MessageBox.Show(table.Name+" si 1");		
				}
				
				
				if(IsJunctionTable(key.ForeignKeyTable))
				{
					System.Windows.Forms.MessageBox.Show(table.Name+" si 2: IsJunctionTable:"+key.ForeignKeyTable.Name);		
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("No 2:"+key.ForeignKeyTable.Name);
				}
				*/
				//BYDAN_NETTIERS
				
				/*
				if(IsJunctionKey(key))
				{
					System.Windows.Forms.MessageBox.Show(table.Name+" si 3: IsJunctionKey:"+key.ForeignKeyTable.Name);		
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("No 3");
				}
				*/
				
				
				
				if ( tables.Contains(key.ForeignKeyTable.Owner, key.ForeignKeyTable.Name) &&  IsJunctionTable(key.ForeignKeyTable) && IsJunctionKey(key))
				{
					//System.Windows.Forms.MessageBox.Show("OK:  M a M");
					TableSchema junctionTable = key.ForeignKeyTable;
					
					// Search for the other(s) key(s) of the junction table' primary key
					foreach(TableKeySchema junctionTableKey in junctionTable.ForeignKeys)
					{				
						if ( tables.Contains(junctionTableKey.ForeignKeyTable.Owner, junctionTableKey.ForeignKeyTable.Name) && IsJunctionKey(junctionTableKey) && junctionTableKey.Name != key.Name )
						{
							//BYDAN----->CUANDO  EN UNA TABLA EXISTE 2 FOREIGN KEY DE OTRA, SE PIENSA QUE EXISTE RELACION M-M QUE NO EXISTE
							if(GetClassName(table).Equals(GetClassName(junctionTableKey.PrimaryKeyTable))) {
								continue;
							}
							//System.Windows.Forms.MessageBox.Show("OK2:  M a M");
							
							TableSchema secondaryTable = junctionTableKey.PrimaryKeyTable;
																			
							CollectionInfo collectionInfo = new CollectionInfo();
					
							#region Additional M:M meta-data 
							collectionInfo.PkColNames = GetColumnNames(table.PrimaryKey.MemberColumns);
							collectionInfo.PrimaryTable = GetClassName(table);
							collectionInfo.SecondaryTable = GetClassName(junctionTableKey.PrimaryKeyTable);
							collectionInfo.SecondaryTablePkColNames = GetColumnNames(junctionTableKey.PrimaryKeyTable.PrimaryKey.MemberColumns);
							collectionInfo.JunctionTableSchema = junctionTable;
							collectionInfo.SecondaryTableSchema = junctionTableKey.PrimaryKeyTable;
							collectionInfo.PrimaryTableSchema = table;
							collectionInfo.JunctionTable = GetClassName(junctionTable);
							collectionInfo.JunctionTablePkColNames = GetColumnNames(junctionTable.PrimaryKey.MemberColumns);
							collectionInfo.CollectionName = string.Format("{0}_From_{1}", GetCollectionPropertyName( collectionInfo.SecondaryTable), GetClassName(collectionInfo.JunctionTable)); //GetManyToManyName(GetCollectionClassName( collectionInfo.SecondaryTable), collectionInfo.JunctionTable);
							collectionInfo.CollectionTypeName = GetCollectionClassName( collectionInfo.SecondaryTable);
							collectionInfo.CollectionRelationshipType = RelationshipType.ManyToMany;
							collectionInfo.FkColNames = GetColumnNames(secondaryTable.PrimaryKey.MemberColumns);
							collectionInfo.TableKey = key;		
							collectionInfo.CleanName = string.Format(manyToManyFormat, GetClassName(collectionInfo.SecondaryTable), GetClassName(junctionTable.Name)); 
							#endregion 
							
							//Key Name - Each collection should have a unique key name
							collectionInfo.PkIdxName = junctionTableKey.Name;
							
							// Property Name
							collectionInfo.PropertyName = string.Format("{0}_From_{1}", GetCollectionPropertyName( collectionInfo.SecondaryTable), GetClassName(collectionInfo.JunctionTable)); 

							// Uninque Property Name, in case of conflict
							collectionInfo.PropertyNameUnique = string.Format("{0}_From_{1}", GetCollectionPropertyName( collectionInfo.SecondaryTable), GetClassName(collectionInfo.JunctionTable)); 

							// Field Variable String
							collectionInfo.FieldName = GetManyToManyName(key, GetCleanName(junctionTable.Name)).Substring(5);
							
							// Property/Field Type Name
							collectionInfo.TypeName = GetCollectionClassName(collectionInfo.SecondaryTable);
							
							//Method Params
							collectionInfo.CallParams = GetFunctionRelationshipCallParameters(table.PrimaryKey.MemberColumns);
							
							//Method Name
							collectionInfo.GetByKeysName = GetManyToManyName(key, GetCleanName(junctionTable.Name));
							
							AddToCollection(_collections, collectionInfo);
						}
					}
				}
			}// end N-N relations
			
			return _collections; 
		}
		
		public void AddToCollection(System.Collections.Hashtable _collections, CollectionInfo collectionInfo)
		{
			if(_collections[collectionInfo.PropertyName] == null)
			{
				_collections[collectionInfo.PropertyName] = collectionInfo;
			}
			else
			{
				CollectionInfo tmp = (CollectionInfo)_collections[collectionInfo.PropertyName];
				tmp.PropertyNameUnique = collectionInfo.PropertyName + tmp.GetByKeysName.Substring(3);

				collectionInfo.PropertyName += collectionInfo.GetByKeysName.Substring(3);
				collectionInfo.PropertyNameUnique += collectionInfo.GetByKeysName.Substring(3);

				if (_collections[collectionInfo.PropertyNameUnique] != null)
				{
					collectionInfo.PropertyName += "From" + GetPropertyName(collectionInfo.PkIdxName);
					collectionInfo.PropertyNameUnique += "From" + GetPropertyName(collectionInfo.PkIdxName);
				}
				_collections[collectionInfo.PropertyName] = collectionInfo;
			}
		}
		#endregion 
		
		#region CollectionInfo class
		///<summary>
		///	Child relationship structure information and their <see cref="RelationshipType" />
		/// to store in the ChildCollections ArrayList
		///</summary>
		public class CollectionInfo 
		{
			public string CleanName;
			public string[] PkColNames;
			public string PkIdxName;
			public string[] FkColNames;
			public string FkIdxName;
			public string PrimaryTable;
			public string SecondaryTable;
			public string[] SecondaryTablePkColNames;
			public string JunctionTable;
			public string[] JunctionTablePkColNames;
			public TableSchema JunctionTableSchema;
			public TableSchema SecondaryTableSchema;
			public TableSchema PrimaryTableSchema;
			public string CollectionName = string.Empty;
			public string CollectionTypeName = string.Empty;
			public string CallParams = string.Empty;
			public string PropertyName = string.Empty;
			public string PropertyNameUnique = string.Empty;
			public string TypeName = string.Empty;
			public string FieldName = string.Empty;
			public string GetByKeysName = string.Empty;
			public RelationshipType CollectionRelationshipType;	
			public TableKeySchema TableKey = null;
		}
		#endregion
			
		#region Relationships
		
		/// <summary>
		/// Gets params for a method based on the columns
		/// </summary>
		public string GetFunctionRelationshipCallParameters(ColumnSchemaCollection columns)
		{
			
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < columns.Count; i++)
			{
				if (i > 0)
					output.Append(", ");
				output.AppendFormat("entity.{0}", GetPropertyName(columns[i].Name));
			}
			return output.ToString();
		}

		/// <summary>
		/// Orders the params for a method, based on the ordered column list.  It's useful when dealing with the IsForeignKeyCoveredByIndex method, which the 
		/// columns may be in different orders
		/// </summary>
		public string GetFunctionRelationshipCallParametersInKeyOrder(ColumnSchemaCollection orderedColumns, TableKeySchema keySchema)
		{
			ColumnSchemaCollection unorderedColumns = keySchema.ForeignKeyMemberColumns;
			ColumnSchemaCollection entityColumns = keySchema.PrimaryKeyMemberColumns;
			
			StringBuilder output = new StringBuilder();
			for (int j = 0; j < orderedColumns.Count; j++)
			{
				for (int i = 0; i < unorderedColumns.Count; i++)
				{
					if (orderedColumns[j].Name.ToLower() != unorderedColumns[i].Name.ToLower())
						continue;
						
					if (j > 0)
						output.Append(", ");
						
					output.AppendFormat("entity.{0}", GetPropertyName(entityColumns[i].Name));
				}
			}
			return output.ToString();
		}
		

	
		///<summary>
		/// Workaround for when a method in the DAL is using Indexes to create the method
		/// instead of the keys
		/// Sometimes when working with composite primary keys, the orders could be 
		/// different in the index than in the key.
		/// So it could be Col1 Col2 in TableKeySchema.ForeignKeyMemberColumns 
		/// But in Index.MemberColumns it could be Col2, Col1
		///</summary>
		public ColumnSchemaCollection GetCorrectColumnOrder(TableKeySchema key)
		{		
			if(IsForeignKeyCoveredByIndex(key))
			{
				bool found = true;
				foreach (IndexSchema idx in key.PrimaryKeyTable.Indexes)
				{
					foreach(ColumnSchema col in key.ForeignKeyMemberColumns)
					{
						if (!idx.MemberColumns.Contains(col.Name))
							found = false;
					}
					
					if (found)
					{
						return idx.MemberColumns;
					}
				}
			}
			
			return key.ForeignKeyMemberColumns;
		}

		/// <summary>
		/// Determines if the table key represents a identifying relationship.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>true if all of the child's foreign key members are part of the primary key.</returns>
		/// <remarks>
		/// An identifying relationship means that the child table cannot 
		/// be uniquely identified without the parent.
		/// </remarks>
		/// <exception cref="ArgumentNullException">key is null</exception>
		public bool IsIdentifyingRelationship(TableKeySchema key)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			PrimaryKeySchema childPrimaryKey = key.ForeignKeyTable.PrimaryKey;
			
			// cant be a identifying relationship if the child does not have a PK
			if ( childPrimaryKey.MemberColumns.Count == 0 )
				return false;

			for (int i = 0; i < key.ForeignKeyMemberColumns.Count; i++)
			{
				// see if the child table's PK has the FK member
				if (childPrimaryKey.MemberColumns[key.ForeignKeyMemberColumns[i].Name] == null)
					;//return false;
			}
			return true;
		}


		///<summary>
		/// returns true all primary key columns have is a foreign key relationship
		/// </summary>
		public bool IsJunctionTable(TableSchema table)
		{
			if (!HasPrimaryKey(table))
			{
				//Response.WriteLine(string.Format("IsJunctionTable: The table {0} doesn't have a primary key.", table.Name));
				return false;
			}
			if (table.PrimaryKey.MemberColumns.Count == 1)
			{
				//BYDAN_NETTIERS
				//return false;				
			}
						
			// junction table requires at least 2 FK
			if (table.ForeignKeys.Count < 2)
				return false;

			// we need 2 identifying relationships
			int identifyingRelationshipCount = 0;
			for (int i = 0; i < table.ForeignKeys.Count; i++)
			{
				if(table.ForeignKeys[i].PrimaryKeyTable.Equals(table))
				{
					//System.Windows.Forms.MessageBox.Show(table.Name);
					//System.Windows.Forms.MessageBox.Show(table.ForeignKeys[i].PrimaryKeyTable.Name);
					continue;
				}
				
				//BYDAN_NETTIERS	2da condicion no puede ser junction si foreing key es Oid
				if ( IsIdentifyingRelationship(table.ForeignKeys[i])&&table.ForeignKeys[i].ForeignKeyMemberColumns[0].Name!= strId )
					identifyingRelationshipCount++;
			}
			if ( identifyingRelationshipCount != 2 )
				return false;

			//BYDAN_NETTIERS			
			for (int i=0;i < table.PrimaryKey.MemberColumns.Count; i++){
				if (!table.PrimaryKey.MemberColumns[i].IsForeignKeyMember)
					;//return false;
			}
			return true;			
		}
		

		public bool IsRelationOneToOne(TableKeySchema keyschema) //, PrimaryKeySchema primaryKey)
		{
			bool result = true;
			
			// if this key do not contain
			if (keyschema.PrimaryKeyMemberColumns.Count != keyschema.PrimaryKeyTable.PrimaryKey.MemberColumns.Count)
				return false;
			
			// Each member must reference a unique key in the foreign table
			foreach(ColumnSchema column in keyschema.ForeignKeyMemberColumns)
			{
				bool columnIsUnique = false;

				// the only way to find the key in the foreign table is to loop through the indexes
				foreach(IndexSchema i in keyschema.ForeignKeyTable.Indexes)
				{
					//The index must be unique and the numer of columns columns
					//in the FK must match the number of columns in the index
					if((i.IsUnique || i.IsPrimaryKey) && (keyschema.ForeignKeyMemberColumns.Count == i.MemberColumns.Count))
					{
						//The index must contain the same column
						if(i.MemberColumns.Contains(column.Name) && (!IsJunctionTable(keyschema.ForeignKeyTable)))
						{
							columnIsUnique = true;
						}
					}
				}
				
				result = result && columnIsUnique;
			}
			
			return result;
		}
		
		

		
		public bool IsForeignKeyCoveredByIndex(TableKeySchema fKey)
		{
			bool isCovered = false;
				
			//If the Foreign key is also covered by an index, let the index 
			//processing handle the Get methods
			foreach(IndexSchema i in fKey.ForeignKeyTable.Indexes)
			{
				ColumnSchemaCollection fkCols = fKey.ForeignKeyMemberColumns;
				
				//First, the index must contain the same number of columns as the key
				if (fkCols.Count != i.MemberColumns.Count)
					continue;
					
				//Index must contain the same columns
				bool hasAllColumns = true;
				foreach(ColumnSchema column in fkCols)
				{
					if(!i.MemberColumns.Contains(column.Name))
					{
						hasAllColumns = false;
						break;
					}
				}
				
				if ( hasAllColumns )
				{
					//Index is a match - stop looking
					isCovered = true;
					break;
					
				}	
			}
			
			return isCovered;
		}
		
		
		public IndexSchema GetIndexCoveringForeignKey(TableKeySchema fKey)
		{
			bool isCovered = false;
				
			//If the Foreign key is also covered by an index, let the index 
			//processing handle the Get methods
			foreach(IndexSchema i in fKey.ForeignKeyTable.Indexes)
			{
				ColumnSchemaCollection fkCols = fKey.ForeignKeyMemberColumns;
				
				//First, the index must contain the same number of columns as the key
				if (fkCols.Count != i.MemberColumns.Count)
					continue;
					
				//Index must contain the same columns
				bool hasAllColumns = true;
				foreach(ColumnSchema column in fkCols)
				{
					if(!i.MemberColumns.Contains(column.Name))
					{
						hasAllColumns = false;
						break;
					}
				}
				
				if ( hasAllColumns )
				{
					//Index is a match - stop looking
					isCovered = true;
					return i;
				}	
			}
			
			return null;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public ColumnSchemaCollection GetRelationKeyColumns(TableKeySchemaCollection fkeys, IndexSchemaCollection indexes)
		{
			//Debugger.Break();
			for (int j=0; j < fkeys.Count; j++)
			{
				bool skipkey = false;
				foreach(IndexSchema i in indexes)
				{
					if(i.MemberColumns.Contains(fkeys[j].ForeignKeyMemberColumns[0]))
						skipkey = true;			
				}
				if(skipkey)
					continue;

				return fkeys[j].ForeignKeyMemberColumns;
			}
			return new ColumnSchemaCollection();
		}
		
		/// <summary>
		/// Gets the names of all the columns in the collection as a string array.
		/// </summary>
		/// <param name="columns"></param>
		/// <returns></returns>
		private string[] GetColumnNames(ColumnSchemaCollection columns)
		{
			string[] columnNames = new string[ columns.Count ];
			for (int i = 0; i < columns.Count; i++)
				columnNames[i] = GetPropertyName(columns[i].Name);
			return columnNames;
		}

		///<summary>
		/// Get's the constraint side of a column from a m:m relationship to it's corresponding 1:m relationship
		///</summary>
		public ColumnSchema GetCorrespondingRelationship(TableKeySchemaCollection fkeys, string columnName)
		{
			//System.Diagnostics.Debugger.Break();
			for (int j=0; j < fkeys.Count; j++)
			{
				for (int y=0; y < fkeys[j].ForeignKeyMemberColumns.Count; y++)
				{
					if (fkeys[j].ForeignKeyMemberColumns[y].Name.ToLower() 
							== columnName.ToLower())
						return fkeys[j].PrimaryKeyMemberColumns[y];
				}
			}
			return null;
		}


		private string _currentTable = string.Empty;
		
		///<summary>
		///  Store the most recent SourceTable of the templates,
		///  Used to clean up upon new SourceTable execution.  
		///</summary>
		[BrowsableAttribute(false)]
		public  string CurrentTable {
			get{return _currentTable;}
			set {_currentTable = value;}
		}
		
		///<summary>
		///  Store the most recent
		///  Used to keep track of which childcollections have been rendered
		///  Eliminates the Dupes.
		///</summary>
		[BrowsableAttribute(false)]
		public  System.Collections.Hashtable RelationshipDictionary {
			get{return relationshipDictionary;}
			set {relationshipDictionary = value;}
		}
		
		
		///<summary>
		/// Child Collection RelationshipType Enum
		///</summary>
		[BrowsableAttribute(false)]
		public enum RelationshipType{
			None = 0,
			OneToOne,
			OneToMany,
			ManyToOne,
			ManyToMany
		}
		
		#endregion Relationships
		
		#region GetParent/Child Tables
		///<summary>
		/// Get's the parent tables if any based on a child table.
		///</summary>
		public TableSchemaCollection GetParentTables(SchemaExplorer.TableSchema table)
		{
			TableSchemaCollection _tbParent= new TableSchemaCollection();
			if(CurrentTable != table.Name){
				CurrentTable = table.Name;
			}
			DatabaseSchema _dbCurrent;
			_dbCurrent=table.Database;
			
			foreach(TableSchema _tb in _dbCurrent.Tables){
				if(CurrentTable!=_tb.Name){
					foreach(ColumnSchema _col in _tb.PrimaryKey.MemberColumns){
						foreach(ColumnSchema col in table.Columns){
							if (col.Name == _col.Name){
								_tbParent.Add(_tb);
							}
						}                        
					}
				}
			}
			return _tbParent;
		}
			
		///<summary>
		///  Get's all the child tables based on a parent table
		///</summary>
		public TableSchemaCollection GetChildTables(SchemaExplorer.TableSchema table)
		{
			TableSchemaCollection _tbChild= new TableSchemaCollection();
				if(CurrentTable != table.Name){
					CurrentTable = table.Name;
				}
				DatabaseSchema _dbCurrent;
				_dbCurrent=table.Database;
				foreach(TableSchema _tb in _dbCurrent.Tables){
					if(CurrentTable!=_tb.Name){
						foreach(ColumnSchema _col in _tb.Columns){
							foreach(ColumnSchema primaryCol in table.PrimaryKey.MemberColumns){
								if (_col.Name == primaryCol.Name){
									_tbChild.Add(_tb);
								}
							}                       
						}
					}
				}
			return _tbChild;
		}
		#endregion 
	}

	#region Retry
	public enum SleepStyle
	{ 
		/// <summary>Each sleep will be the <i>n</i> milliseconds.</summary>
		Constant, 
		/// <summary>Each sleep will increase by <i>n</i>*<i>attempts</i> milliseconds.</summary>
		Linear, 
		/// <summary>Each sleep will increase exponential by <i>n</i>^<i>attempts</i> milliseconds.</summary>
		Exponential 
	}
	#endregion
	
	#region UnitTests
	
	public enum UnitTestStyle
	{
		/// <summary>No unit test should be included.</summary>
		None,
		/// <summary>NUnit tests should be generated.</summary>
		NUnit,
		/// <summary>VSTS test should be gerenated.</summary>
		VSTS
	}
	#endregion
	
	#region ComponentPatternType
	public enum ComponentPatternType
	{
		/// <summary>No Component Pattern Generation should be included.</summary>
		None,
		/// <summary>A Service Layer Pattern should be included.</summary>
		ServiceLayer,
		/// <summary>A Domain Model Pattern Generation should be included.</summary>
		DomainModel
	}
	#endregion
	
	#region DatabaseType
	public enum DatabaseType
	{
		/// <summary>No specific database type.</summary>
		None,
		/// <summary>SQL Server 2000.</summary>
		//SQLServer2000,
		/// <summary>SQL Server 2005.</summary>
		SQLServer2005
		/// <summary>Oracle 8i.</summary>
		//Oracle8i,
		/// <summary>Oracle 9i.</summary>
		//Oracle9i,
		/// <summary>Oracle 10g.</summary>
		//Oracle10g,
	}
	#endregion

	#region MethodNamesProperty
	
	[Serializable]
	//[TypeConverter(typeof(MethodNamesTypeConverter))]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PropertySerializer(typeof(XmlPropertySerializer))]
	public class MethodNamesProperty
	{
		public MethodNamesProperty() { }
		public MethodNamesProperty(string values)
		{
			ParseCore(values);
		}
		
		// used for testing
		private static readonly string _methodNameSuffix = "";
		
		private string _get = "Get" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a Get operation.")]
		public string Get
		{
			get { return _get; }
			set { if ( IsValid(value) ) _get = value.Trim(); }
		}
		
		private string _getAll = "GetAll" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a GetAll operation.")]
		public string GetAll
		{
			get { return _getAll; }
			set { if ( IsValid(value) ) _getAll = value.Trim(); }
		}
		
		private string _getPaged = "GetPaged" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a GetPaged operation.")]
		public string GetPaged
		{
			get { return _getPaged; }
			set { if ( IsValid(value) ) _getPaged = value.Trim(); }
		}
		
		private string _find = "Find" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a Find operation.")]
		public string Find
		{
			get { return _find; }
			set { if ( IsValid(value) ) _find = value.Trim(); }
		}
		
		private string _insert = "Insert" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a Insert operation.")]
		public string Insert
		{
			get { return _insert; }
			set { if ( IsValid(value) ) _insert = value.Trim(); }
		}
		
		private string _update = "Update" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a Update operation.")]
		public string Update
		{
			get { return _update; }
			set { if ( IsValid(value) ) _update = value.Trim(); }
		}
		
		private string _save = "Save" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a Save operation.")]
		public string Save
		{
			get { return _save; }
			set { if ( IsValid(value) ) _save = value.Trim(); }
		}
		
		private string _delete = "Delete" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a Delete operation.")]
		public string Delete
		{
			get { return _delete; }
			set { if ( IsValid(value) ) _delete = value.Trim(); }
		}
		
		private string _deepLoad = "DeepLoad" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a DeepLoad operation.")]
		public string DeepLoad
		{
			get { return _deepLoad; }
			set { if ( IsValid(value) ) _deepLoad = value.Trim(); }
		}
		
		private string _deepSave = "DeepSave" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a DeepSave operation.")]
		public string DeepSave
		{
			get { return _deepSave; }
			set { if ( IsValid(value) ) _deepSave = value.Trim(); }
		}
		
		private string _getTotalItems = "GetTotalItems" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a GetTotalItems operation.")]
		public string GetTotalItems
		{
			get { return _getTotalItems; }
			set { if ( IsValid(value) ) _getTotalItems = value.Trim(); }
		}
		
		private string _bulkInsert = "BulkInsert" + _methodNameSuffix;
		[NotifyParentProperty(true)]
		[Description("The name of the method used to perform a BulkInsert operation.")]
		public string BulkInsert
		{
			get { return _bulkInsert; }
			set { if ( IsValid(value) ) _bulkInsert = value.Trim(); }
		}
		
		private bool IsValid(string value)
		{
			return ( value != null && value.Trim().Length > 0 );
		}
		
		private void ParseCore(string value)
		{
			if ( value != null && value.Length > 0 )
			{
				string[] values = value.Split(new char[] { ',' });
				
				if ( values.Length > 0 )
					Get = values[0];
				if ( values.Length > 1 )
					GetAll = values[1];
				if ( values.Length > 2 )
					GetPaged = values[2];
				if ( values.Length > 3 )
					Find = values[3];
				if ( values.Length > 4 )
					Insert = values[4];
				if ( values.Length > 5 )
					Update = values[5];
				if ( values.Length > 6 )
					Save = values[6];
				if ( values.Length > 7 )
					Delete = values[7];
				if ( values.Length > 8 )
					DeepLoad = values[8];
				if ( values.Length > 9 )
					DeepSave = values[9];
				if ( values.Length > 10 )
					GetTotalItems = values[10];
				if ( values.Length > 11 )
					BulkInsert = values[11];
			}
		}
		
		public static MethodNamesProperty Parse(string value)
		{
			return new MethodNamesProperty(value);
		}
		
		public string ToStringList()
		{
			string[] names = new string[] {
				Get, GetAll, GetPaged, Find,
				Insert, Update, Save, Delete,
				DeepLoad, DeepSave, GetTotalItems,
				BulkInsert
			};
			
			return string.Join(",", names);
		}
		
		public override string ToString()
		{
			return "(Expand to edit...)";
		}
	}
	
	public class MethodNamesTypeConverter : ExpandableObjectConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
		{
			if ( t == typeof(string) )
			{
				return true;
			}
			else if ( t == typeof(XmlNode) )
			{
				return true;
			}
			
			return base.CanConvertFrom(context, t);
		}
		
		public override bool CanConvertTo(ITypeDescriptorContext context, Type t)
		{
			if ( t == typeof(XmlNode) )
			{
				return true;
			}
			
			return base.CanConvertTo(context, t);
		}
		
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
		{
			if ( value is string )
			{
				return MethodNamesProperty.Parse(value as string);
			}
			else if ( value is XmlNode )
			{
				XmlNode node = (XmlNode) value;
				XmlSerializer ser = new XmlSerializer(context.PropertyDescriptor.PropertyType);
				XmlNodeReader reader = new XmlNodeReader(node.FirstChild);
				return ser.Deserialize(reader);
			}
			
			return base.ConvertFrom(context, info, value);
		}
		
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type t)
		{
			if ( t == typeof(string) )
			{
				return ((MethodNamesProperty) value).ToStringList();
			}
			else if ( t == typeof(XmlNode) )
			{
				XmlSerializer ser = new XmlSerializer(t);
				MemoryStream stream = new MemoryStream();
				ser.Serialize(stream, value);
				stream.Position = 0;
				XmlDocument xml = new XmlDocument();
				xml.Load(stream);
				stream.Close();
				return xml.DocumentElement.FirstChild;
			}
			
			return base.ConvertTo(context, culture, value, t);
		}
		
	#endregion MethodNamesProperty
	


#endregion
	}
	
	
	#region ExtraCode
	
		public  class ExtraCode 	
		{
			public static string GetExtra1Servlet(TableSchema table)
			{
				String strExtra1Servlet="";
				
				if(table.Database.Name=="AulaVirtual"&&table.Name.Equals("Curso"))
				{
					strExtra1Servlet="\r\n\t\t\tif(session.getAttribute(\"AlumnoActual\")!=null)";
					strExtra1Servlet+="\r\n\t\t\t{";
					strExtra1Servlet+="\r\n\t\t\t\trequest.setAttribute(\"accionAdicional\", \"MOSTRAR_CURSOS_ALUMNOACTUAL\");";
					strExtra1Servlet+="\r\n\t\t\t}";
					
					//strPathFile="ExtraCode/AulaVirtual/Servlet/Extra1/Code.txt";					
					//strExtra1Servlet=ReadFile(strPathFile);
				}
				/*
			public static string ReadFile(String strPathFile)
			{
				String strExtra="";
				
				try
         		{
					FileStream s = new FileStream(strPathFile, FileMode.Open);
					StreamReader r = new StreamReader(s);
					string t;
					while ((t = r.ReadLine()) != null)
					{
						strExtra+=t;
					}
				}
				catch(IOException e)
				{
					Console.WriteLine("An IO exception has been thrown! con Extra Code");
					Console.WriteLine(e.ToString());
				}
				
				return strExtra;
			}
			*/
				return strExtra1Servlet;
			}
		}
	#endregion
}
