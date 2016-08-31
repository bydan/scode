package ByDan.Framework.Presentation.Web.Ajax.JMaki.Utils;

import java.util.ArrayList;

import ByDan.Seguridad.Business.Entities.Usuario;


public class DojoTable {
	
	static String startColumns="{columns:[";
	static String startColumn="{ 'title' : '";
	static String beetwenColumn=",";
	static String endColumn="'}";	
	static String endColumns="]";
	
	static String startFiles=",rows:[";
	static String startFile="[";
	static String beetwenFile=",";
	static String endFile="]";
	static String beetwenFiles=",";
	static String endFiles="]}";
		
	public static void StartCreateColumns(StringBuilder tableAjax) 
	{	
		tableAjax.append(startColumns);
	}
	
	public static void CreateColumn(StringBuilder tableAjax,String column,Boolean withBeetwen) 
	{	
		tableAjax.append(startColumn);
		tableAjax.append(column);
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
			tableAjax.append("'");
			tableAjax.append(column);
			tableAjax.append("'");
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
	
	
	
	
	public static String TraerUsuarios(ArrayList<Usuario> usuarios) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		
		StartCreateColumns(tableAjax) ;		
		CreateColumn(tableAjax,"NOMBRE",true);
		CreateColumn(tableAjax,"CLAVE",false);
		EndCreateColums(tableAjax) ;
		
		Integer count=1;
		
		StartCreateFiles(tableAjax) ;	
		
		String columnsFile[]=new String[2];
		
		for(Usuario usuario:usuarios)
		{
			//columnsFile[0]=usuario.getNombre();
			//columnsFile[1]=usuario.getClave();
					
			 
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
