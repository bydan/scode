package ByDan.Framework.Presentation.Web.Ajax.JMaki.Utils;

import java.util.ArrayList;

import ByDan.Seguridad.Business.Entities.Usuario;

public class YahooTabbedView {
	static String startFiles="{tabs:[";
	static String startFile="{label:";
	static String beetwenFile=", content: ";
	static String endFile="}";
	static String beetwenFiles=",";
	static String endFiles="]}";
		
		
	public static void StartCreateFiles(StringBuilder tableAjax) 
	{	
		tableAjax.append(startFiles);
	}
	
	public static void CreateFile(StringBuilder tableAjax,String columnLabel,String columnContent,Boolean withBeetwen) 
	{	
		tableAjax.append(startFile);
		
		
		tableAjax.append("'"+columnLabel+"'");					
		tableAjax.append(beetwenFile);
		tableAjax.append("'"+columnContent+"'");	
		
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
		
		Integer count=1;
		
		StartCreateFiles(tableAjax) ;	
		
		
		for(Usuario usuario:usuarios)
		{
					
			 
			if(usuarios.size()!=count)
			{
				//CreateFile(tableAjax,usuario.getNombre(),usuario.getClave(),true);
			}
			else
			{
				//CreateFile(tableAjax,usuario.getNombre(),usuario.getClave(),false);
				
			}
			count++;
		}		
		EndCreateFiles(tableAjax) ;	
		
		return tableAjax.toString();
	}
}
