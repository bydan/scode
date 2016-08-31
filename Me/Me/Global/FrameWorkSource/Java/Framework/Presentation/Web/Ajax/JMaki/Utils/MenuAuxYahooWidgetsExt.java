package ByDan.Framework.Presentation.Web.Ajax.JMaki.Utils;
import java.util.ArrayList;

public class MenuAuxYahooWidgetsExt {
	 String startMenu="menu: [";
	 String startMenuLabel="{label:'";
	 String startMenuUrl="url:'";
	 String startMenuStyle="style:{";
	 String beetwenItem=",";
	 String endItem="'";
	 String endMenuUrl="'}";
	 String endMenuLabel="'";
	 String endMenu="]";
	 String style="";
	 String label="";
	 String url="";
	 static String endKey="}";
	 static String startKey="{";
	
	 Boolean esUltimo=false;
	 Boolean esInicio=false;
	 
	ArrayList<MenuAuxYahooWidgetsExt> auxMenusYahoo= new ArrayList<MenuAuxYahooWidgetsExt>();
	
	public Boolean getEsUltimo() {
		return esUltimo;
	}
	public void setEsUltimo(Boolean esUltimo) {
		this.esUltimo = esUltimo;
	}
	
	public String getLabel() {
		return label;
	}
	public void setLabel(String label) {
		this.label = label;
	}
	public  String getStyle() {
		return style;
	}
	public  void setStyle(String style) {
		style = style;
	}
	public ArrayList<MenuAuxYahooWidgetsExt> getAuxMenusYahoo() {
		return auxMenusYahoo;
	}
	public void setAuxMenusYahoo(ArrayList<MenuAuxYahooWidgetsExt> auxMenusYahoo) {
		this.auxMenusYahoo = auxMenusYahoo;
	}
	
	public void buildMenu(StringBuilder tableAjax) {
		
		Integer count=1;
		if(esInicio)
		{
			tableAjax.append(startMenu);				
		}
		
		tableAjax.append(startMenuLabel);
		tableAjax.append(label);
		tableAjax.append(endItem);
		
		if(auxMenusYahoo.size()>0)
		{
			tableAjax.append(",");
			for(MenuAuxYahooWidgetsExt aux:auxMenusYahoo)
			{
				
				if(count.equals(1))
				{
					aux.setEsInicio(true); 
					
				}
				
				if(count.equals(auxMenusYahoo.size()))
				{
					aux.setEsUltimo(true); 
				}
				aux.buildMenu(tableAjax);
				count++;
			}
			tableAjax.append(endKey);
		}
		else{
			
			
			if(url==null)
			{
				url="";
			}
				tableAjax.append(beetwenItem);
				tableAjax.append(startMenuUrl);
				tableAjax.append(url);
				tableAjax.append(endItem);
				
				tableAjax.append(beetwenItem);
				tableAjax.append(startMenu);
				tableAjax.append(endMenu);
				
				if(!style.equals(""))
				{
					tableAjax.append(beetwenItem);
					tableAjax.append(startMenuStyle);
					tableAjax.append(style);
					tableAjax.append(endKey);
					
				}
				tableAjax.append(endKey);
			
		}
		
		
		
		if(!esUltimo)
		{
			tableAjax.append(",");
		}
		else{
			tableAjax.append(endMenu);
			
		}
		
				
		
	}
	public String getUrl() {
		return url;
	}
	public void setUrl(String url) {
		this.url = url;
	}
	public Boolean getEsInicio() {
		return esInicio;
	}
	public void setEsInicio(Boolean esInicio) {
		this.esInicio = esInicio;
	}
}
