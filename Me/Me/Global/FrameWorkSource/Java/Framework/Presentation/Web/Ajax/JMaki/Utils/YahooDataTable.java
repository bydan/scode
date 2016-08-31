package ByDan.Framework.Presentation.Web.Ajax.JMaki.Utils;

import java.util.ArrayList;

import ByDan.Seguridad.Business.Entities.Usuario;

public class YahooDataTable {
	static String startColumns="{columns:[";
	static String startColumn="{ 'title' : '";
	static String beetwenColumn=",";
	static String endTitle="',";	
	static String endColumn="}";	
	static String endColumns="]}";
	
	static String startFiles="[";
	static String startFile="[";
	static String beetwenFile=",";
	static String endFile="]";
	static String beetwenFiles=",";
	static String endFiles="]";
		
	public static void StartCreateColumns(StringBuilder tableAjax) 
	{	
		tableAjax.append(startColumns);
	}
	
	public static void CreateColumn(StringBuilder tableAjax,String column,String characteristic,Boolean withBeetwen) 
	{	
		tableAjax.append(startColumn);
		tableAjax.append(column);
		tableAjax.append(endTitle);
		tableAjax.append(characteristic);
		tableAjax.append(endColumn);
		
		if(withBeetwen)
		{
			tableAjax.append(beetwenColumn);
		}
	}	
	
	public static void EndCreateColums(StringBuilder tableAjax) 
	{	
		tableAjax.append(endColumns);
	}
	
	public static void StartCreateFiles(StringBuilder tableAjax) 
	{	
		tableAjax.append(startFiles);
	}
	
	public static void CreateFile(StringBuilder tableAjax,String[] columns,Boolean withBeetwen) 
	{	
		Integer count=1;
		
		tableAjax.append(startFile);
		
		for(String column:columns)
		{
			tableAjax.append(column);
			if(columns.length!=count)
			{
				tableAjax.append(beetwenFile);
			}
			count++;
		}
		tableAjax.append(endFile);
				
		if(withBeetwen)
		{
			tableAjax.append(beetwenFiles);
		}
	}	
	
	public static void EndCreateFiles(StringBuilder tableAjax) 
	{	
		tableAjax.append(endFiles);
	}
	
	
	
	
	public static String TraerUsuariosArgs(ArrayList<Usuario> usuarios) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		
		StartCreateColumns(tableAjax) ;		
		CreateColumn(tableAjax,"NOMBRE","width : 200, locked:false",true);
		CreateColumn(tableAjax,"CLAVE","width : 200, locked:false",false);
		EndCreateColums(tableAjax) ;
		
		
		
		return tableAjax.toString();
	}
	
	public static String TraerUsuariosValue(ArrayList<Usuario> usuarios) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		
		Integer count=1;
		
		StartCreateFiles(tableAjax) ;	
		
		String columnsFile[]=new String[2];
		
		for(Usuario usuario:usuarios)
		{
			//columnsFile[0]="'"+usuario.getNombre()+"'";
			//columnsFile[1]="'"+usuario.getClave()+"'";
					
			 
			if(usuarios.size()!=count)
			{
				CreateFile(tableAjax,columnsFile,true);
			}
			else
			{
				CreateFile(tableAjax,columnsFile,false);
				
			}
			count++;
		}		
		EndCreateFiles(tableAjax) ;	
		
		return tableAjax.toString();
	}
}
