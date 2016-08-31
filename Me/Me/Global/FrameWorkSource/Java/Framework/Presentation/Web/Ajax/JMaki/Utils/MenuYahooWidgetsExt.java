package ByDan.Framework.Presentation.Web.Ajax.JMaki.Utils;

import java.util.ArrayList;

import ByDan.Seguridad.Business.Entities.Usuario;
import ByDan.Seguridad.Business.Entities.Funcionalidad;

public class MenuYahooWidgetsExt {
	static String startKey="{";
	static String endKey="}";
	MenuAuxYahooWidgetsExt auxMenuYahoo;
	
	public static void StartCreateMenu(StringBuilder tableAjax) 
	{	
		tableAjax.append(startKey);
	}
	
		
	public static void EndCreateMenu(StringBuilder tableAjax) 
	{	
		tableAjax.append(endKey);
	}
		
	public static String TraerPaginas(ArrayList<Funcionalidad> paginas) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		Integer count=1;
		
		StartCreateMenu(tableAjax) ;	
		
		for(Funcionalidad pagina:paginas)
		{
			
			MenuAuxYahooWidgetsExt auxMenuYahoo=new MenuAuxYahooWidgetsExt();	
			
			if(count.equals(1))
			{
				auxMenuYahoo.setEsInicio(true); 
			}
			
			auxMenuYahoo.setLabel(pagina.getField_strNombre()); 
			auxMenuYahoo.setUrl(pagina.getField_strPath()); 
			
			if(count.equals(paginas.size()))
			{
				auxMenuYahoo.setEsUltimo(true); 
			}
			auxMenuYahoo.buildMenu(tableAjax);
			
			count++;
		}		
		
		EndCreateMenu(tableAjax) ;	
		
		return tableAjax.toString();
	}
	
	public static String TraerUsuarios(ArrayList<Usuario> usuarios) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		Integer count=1;
		
		StartCreateMenu(tableAjax) ;	
		
		for(Usuario usuario:usuarios)
		{
			
			MenuAuxYahooWidgetsExt auxMenuYahoo=new MenuAuxYahooWidgetsExt();	
			
			if(count.equals(1))
			{
				auxMenuYahoo.setEsInicio(true); 
			}
			
			//auxMenuYahoo.setLabel(usuario.getNombre()); 
			//auxMenuYahoo.setUrl(usuario.getClave()); 
			
			if(count.equals(usuarios.size()))
			{
				auxMenuYahoo.setEsUltimo(true); 
			}
			auxMenuYahoo.buildMenu(tableAjax);
			
			count++;
		}		
		
		EndCreateMenu(tableAjax) ;	
		
		return tableAjax.toString();
	}
	
	public static String TraerUsuariosHijos(ArrayList<Usuario> usuarios) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		Integer count=1;
		
		StartCreateMenu(tableAjax) ;	
		
		ArrayList<MenuAuxYahooWidgetsExt> auxMenusYahoo= new ArrayList<MenuAuxYahooWidgetsExt>();
		
		MenuAuxYahooWidgetsExt auxMenuYahoo=new MenuAuxYahooWidgetsExt();		 
		auxMenuYahoo.setLabel("test"); 
		auxMenuYahoo.setUrl("test"); 
		auxMenuYahoo.setEsInicio(true); 
		auxMenuYahoo.setEsUltimo(true); 
		
		for(Usuario usuario:usuarios)
		{
			
			MenuAuxYahooWidgetsExt auxMenuYahoo1=new MenuAuxYahooWidgetsExt();		 
				
			
			//auxMenuYahoo1.setLabel(usuario.getNombre()); 
			//auxMenuYahoo1.setUrl(usuario.getClave()); 
			
			
			
			auxMenusYahoo.add(auxMenuYahoo1);
			
			
			
			count++;
		}		
		auxMenuYahoo.setAuxMenusYahoo(auxMenusYahoo);
		auxMenuYahoo.buildMenu(tableAjax);
		
		EndCreateMenu(tableAjax) ;	
		
		return tableAjax.toString();
	}
	
	public static String TraerPaginasPadresHijos(ArrayList<Funcionalidad> paginas) 
	{	
		StringBuilder tableAjax=new StringBuilder();
		Integer count=1;
		
		StartCreateMenu(tableAjax) ;	
		
		ArrayList<MenuAuxYahooWidgetsExt> auxMenusYahoo= new ArrayList<MenuAuxYahooWidgetsExt>();
		
		MenuAuxYahooWidgetsExt auxMenuYahoo=new MenuAuxYahooWidgetsExt();		 
		auxMenuYahoo.setLabel("test"); 
		auxMenuYahoo.setUrl("test"); 
		auxMenuYahoo.setEsInicio(true); 
		auxMenuYahoo.setEsUltimo(true); 
		
		for(Funcionalidad pagina:paginas)
		{
			
			MenuAuxYahooWidgetsExt auxMenuYahoo1=new MenuAuxYahooWidgetsExt();		 			
			
			auxMenuYahoo1.setLabel(pagina.getField_strNombre()); 
			auxMenuYahoo1.setUrl(pagina.getField_strPath()); 						
			auxMenusYahoo.add(auxMenuYahoo1);
			count++;
		}		
		auxMenuYahoo.setAuxMenusYahoo(auxMenusYahoo);
		auxMenuYahoo.buildMenu(tableAjax);
		
		EndCreateMenu(tableAjax) ;	
		
		return tableAjax.toString();
	}
}
