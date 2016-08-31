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
	public  class CommonCodeProyecto { //: CodeTemplate 
	
		#region Clase General Proyecto
		public static void GetInicializarVariablesFuncionalidadProyectoC(TableSchema table) {
			CommonCode.strTipoTablaFuncionalidad="";			
			CommonCode.strTipoBaseDatosFuncionalidad="";
			
			CommonCode.strTipoBaseDatosFuncionalidad=table.Database.Name;
						
			if(CommonCode.strTipoBaseDatosFuncionalidad.Equals(CommonCode.strProyectoErp)) {
				CommonCodeProyectoErp.GetInicializarVariablesFuncionalidadProyectoC(table);
			}
			
			//Trace.WriteLine("DB="+CommonCode.strTipoBaseDatosFuncionalidad);
			//Trace.WriteLine("TBL="+CommonCode.strTipoTablaFuncionalidad);
			
		}
		
		public static String GetConEventQueryDependColumnFromFuncionalidadProyectoC(ColumnSchema columnSchemaUpdate) {
			String sEventQueryDependColumn="";
			
			if(CommonCode.strTipoBaseDatosFuncionalidad.Equals(CommonCode.strProyectoErp)) {
				sEventQueryDependColumn=CommonCodeProyectoErp.GetConEventQueryDependColumnFromFuncionalidadProyectoC(columnSchemaUpdate);
			}						
			
			return sEventQueryDependColumn;
		}
		#endregion
	}
	
	public  class CommonCodeProyectoErp {
		
		#region Clase Proyecto Erp
		public static void GetInicializarVariablesFuncionalidadProyectoC(TableSchema table) {
						
			bool tieneBodega=false;
			bool tieneProducto=false;
			bool tieneUnidad=false;
				
			foreach(ColumnSchema column in table.Columns) {
				if(column.IsForeignKeyMember) {
					if(column.Name.Equals("idBodega")) {
						tieneBodega=true;
						
					} else if(column.Name.Equals("idProducto")) {
						tieneProducto=true;
							
					} else if(column.Name.Equals("idUnidad")) {
						tieneUnidad=true;
						
						
						//PUEDE SER QUE NO EXISTA ORDEN Y TOCA COMENTAR ESTO
						break;
					}												
				}
			}
				
			if(tieneBodega && tieneProducto && tieneUnidad) {
				CommonCode.strTipoTablaFuncionalidad="BODEGA_PRODUCTO_UNIDAD";
			}					
		}
		
		public static String GetConEventQueryDependColumnFromFuncionalidadProyectoC(ColumnSchema columnSchemaUpdate) {
			String sEventQueryDependColumn="";
			
			if(CommonCode.strTipoTablaFuncionalidad.Equals("BODEGA_PRODUCTO_UNIDAD")) {
				if(columnSchemaUpdate.Name.Equals("idProducto")) {
					sEventQueryDependColumn="\r\n\t\t\tsFinalQueryCombo=InventarioSql.GetQueryProductoFromBodega(bodegaLocal);";
				
				} else if(columnSchemaUpdate.Name.Equals("idUnidad")) {
					sEventQueryDependColumn="\r\n\t\t\tsFinalQueryCombo=InventarioSql.GetQueryUnidadFromBodegaYProducto(productoLocal,this."+CommonCode.GetNombreClaseObjetoC(columnSchemaUpdate.Table.ToString())+".getid_bodega());";
				}
			}									
			
			return sEventQueryDependColumn;
		}
		#endregion
	}
}