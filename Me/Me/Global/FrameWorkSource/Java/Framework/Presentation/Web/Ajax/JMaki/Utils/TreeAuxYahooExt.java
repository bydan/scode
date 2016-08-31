package ByDan.Framework.Presentation.Web.Ajax.JMaki.Utils;

import java.util.ArrayList;

public class TreeAuxYahooExt {
	static String startChildren="children : [";
	String onClickUrl="";
	 Boolean expanded=false;
	 static  String startTreeTitle="{title : '";
	 static  String startTreeUrl="url:'";
	 static  String beetwenItem=",";
	 static  String endItem="'";
	 static  String endTreeTitle="'";
	 static  String endChildren="]";
	 String title="";
	 static String endKey="}";
	 static String startKey="{";
	
	 Boolean esUltimo=false;
	 Boolean esInicio=false;
	 
	ArrayList<TreeAuxYahooExt> auxTreesYahoo= new ArrayList<TreeAuxYahooExt>();
	
	public Boolean getEsUltimo() {
		return esUltimo;
	}
	public void setEsUltimo(Boolean esUltimo) {
		this.esUltimo = esUltimo;
	}
	
	
	public ArrayList<TreeAuxYahooExt> getAuxTreesYahoo() {
		return auxTreesYahoo;
	}
	public void setAuxTreesYahoo(ArrayList<TreeAuxYahooExt> auxTreesYahoo) {
		this.auxTreesYahoo = auxTreesYahoo;
	}
	
	public void buildTree(StringBuilder tableAjax) {
		
		
		
		tableAjax.append(startTreeTitle);
		tableAjax.append(title);
		tableAjax.append(endItem);
		
		if(onClickUrl!=""){
			tableAjax.append(",");
			tableAjax.append("onclick : {url:'");
			tableAjax.append(onClickUrl);
			tableAjax.append(endItem);
			tableAjax.append(endKey);
		}
		
		/*Integer count=1;
		
		if(auxTreesYahoo!=null&& auxTreesYahoo.size()>0)
		{
			tableAjax.append(",");
			tableAjax.append(startChildren);	
			
			for(TreeAuxYahooExt aux:auxTreesYahoo)
			{	
				if(count.equals(auxTreesYahoo.size())){
					aux.setEsUltimo(true);	
				}
				aux.buildTree(tableAjax);
				
				count++;
			}
			
			
			tableAjax.append(endChildren);
			tableAjax.append(endKey);
			
			//tableAjax.append(endKey);
			//tableAjax.append(endItem);
		}
		else{
			
			tableAjax.append(endKey);
			
		}*/
		
		
		
		
		if(!esUltimo)
		{
			tableAjax.append(",");
		}
		
		
		
				
		
	}
	
	public String getOnClickUrl() {
		return onClickUrl;
	}
	public void setOnClickUrl(String onClickUrl) {
		this.onClickUrl = onClickUrl;
	}
	public Boolean getEsInicio() {
		return esInicio;
	}
	public void setEsInicio(Boolean esInicio) {
		this.esInicio = esInicio;
	}
	public Boolean getExpanded() {
		return expanded;
	}
	public void setExpanded(Boolean expanded) {
		this.expanded = expanded;
	}
	public String getTitle() {
		return title;
	}
	public void setTitle(String title) {
		this.title = title;
	}
	public static String getEndKey() {
		return endKey;
	}
	public static void setEndKey(String endKey) {
		TreeAuxYahooExt.endKey = endKey;
	}
	public static String getStartKey() {
		return startKey;
	}
	public static void setStartKey(String startKey) {
		TreeAuxYahooExt.startKey = startKey;
	}
	public String getBeetwenItem() {
		return beetwenItem;
	}
	public static void setBeetwenItem(String newbeetwenItem) {
		beetwenItem = newbeetwenItem;
	}
	public static String getEndChildren() {
		return endChildren;
	}
	public static void setEndChildren(String newendChildren) {
		endChildren = newendChildren;
	}
	public static String getEndItem() {
		return endItem;
	}
	public static void setEndItem(String newendItem) {
		endItem = newendItem;
	}
	public String getEndTreeTitle() {
		return endTreeTitle;
	}
	public static void setEndTreeTitle(String newendTreeTitle) {
		endTreeTitle = newendTreeTitle;
	}
	public static String getStartChildren() {
		return startChildren;
	}
	public static void setStartChildren(String newstartChildren) {
		startChildren = newstartChildren;
	}
	public static String getStartTreeTitle() {
		return startTreeTitle;
	}
	public static void setStartTreeTitle(String newstartTreeTitle) {
		startTreeTitle = newstartTreeTitle;
	}
	public String getStartTreeUrl() {
		return startTreeUrl;
	}
	public static void setStartTreeUrl(String newstartTreeUrl) {
		startTreeUrl = newstartTreeUrl;
	}
}
