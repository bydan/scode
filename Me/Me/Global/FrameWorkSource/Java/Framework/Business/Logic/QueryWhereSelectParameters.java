package ByDan.Framework.Business.Logic;

import java.util.ArrayList;

import ByDan.Framework.Utils.ParameterDbType;
import ByDan.Framework.Utils.ParameterType;

public class QueryWhereSelectParameters {
private String initialQuery;
private String finalQuery;
private boolean isAll;
private ParameterDbType dbType;

private	ArrayList<ParameterSelectionGeneral> parametersSelectionGeneral;

public QueryWhereSelectParameters(ParameterDbType newDbType,String newFinalQuery)
{ 
	finalQuery=newFinalQuery;
	dbType=newDbType;
	parametersSelectionGeneral=new ArrayList<ParameterSelectionGeneral>();
}

public QueryWhereSelectParameters(ParameterDbType newDbType,String newInitialQuery,String newFinalQuery)
{ 
	initialQuery=newInitialQuery;
	finalQuery=newFinalQuery;
	dbType=newDbType;
	parametersSelectionGeneral=new ArrayList<ParameterSelectionGeneral>();
}

public String getInitialQuery() {
	return initialQuery;
}

public void setInitialQuery(String initialQuery) {
	this.initialQuery = initialQuery;
}

public void setAll(boolean isAll) {
	this.isAll = isAll;
}

public QueryWhereSelectParameters(String newFinalQuery)
{ 
	finalQuery=newFinalQuery;
	isAll=true;
	
}
public QueryWhereSelectParameters()
{ 
	finalQuery="";
	isAll=true;
	
}
public ParameterDbType getDbType()
{ 
	 return dbType; 
}

public void setDbType(ParameterDbType newDbType) 
{ 
   dbType = newDbType; 
}

public boolean getIsAll() {
	return isAll;
}

public void setIsAll(boolean isAll) {
	this.isAll = isAll;
}

public String getFinalQuery() {
	return finalQuery;
}

public void setFinalQuery(String finalQuery) {
	this.finalQuery = finalQuery;
}

public ArrayList<ParameterSelectionGeneral> getParametersSelectionGeneral() {
	return parametersSelectionGeneral;
}

public void setParametersSelectionGeneral(
		ArrayList<ParameterSelectionGeneral> parametersSelectionGeneral) {
	this.parametersSelectionGeneral = parametersSelectionGeneral;
}
public void addParameter(
		ParameterSelectionGeneral parameterSelectionGeneral) {
	this.parametersSelectionGeneral.add(parameterSelectionGeneral);
}
public String getQueryWhereSelectParameters() {
	String query="";
	if(!isAll){
		for(ParameterSelectionGeneral parameterSelectionGeneral:parametersSelectionGeneral)
		{
			if(dbType.equals(ParameterDbType.MYSQL))
			{
			
			query+=addMYSQLParameterQuery(parameterSelectionGeneral);
			}
		}
	}
	return query;
}

public String addMYSQLParameterQuery(ParameterSelectionGeneral parameterSelectionGeneral) {
	String queryParameter="";
	queryParameter+= " ("+parameterSelectionGeneral.getColumnName();
	
	if(parameterSelectionGeneral.getIsEqual())
	{
			
		if(parameterSelectionGeneral.getParameterType().equals(ParameterType.BIGDECIMAL)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.FLOAT)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.DOUBLE)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.BOOLEAN)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.DATE)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.INT)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.LONG)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.TIME)||
		   parameterSelectionGeneral.getParameterType().equals(ParameterType.TIMESTAMP))
		{
			queryParameter+=" = ";
			queryParameter+= parameterSelectionGeneral.getParameterInitialValue();
			queryParameter+=" ) "+parameterSelectionGeneral.getNextOperator(ParameterDbType.MYSQL);
			
		}
		
		if(parameterSelectionGeneral.getParameterType().equals(ParameterType.STRING))
		{
			queryParameter+=" like '";
			queryParameter+= parameterSelectionGeneral.getParameterInitialValue();
			queryParameter+="'";
			queryParameter+=" ) "+parameterSelectionGeneral.getNextOperator(ParameterDbType.MYSQL);
			
		}
		
			
	}
	else
	{
		if(parameterSelectionGeneral.getParameterType().equals(ParameterType.BIGDECIMAL)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.FLOAT)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.DOUBLE)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.BOOLEAN)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.DATE)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.INT)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.LONG)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.TIME)||
				   parameterSelectionGeneral.getParameterType().equals(ParameterType.TIMESTAMP))
				{
					queryParameter+=parameterSelectionGeneral.getParameterInitialTypeSign(ParameterDbType.MYSQL);
					queryParameter+=" "+parameterSelectionGeneral.getParameterInitialValue();		
					queryParameter+=" and "+ parameterSelectionGeneral.getColumnName();
					queryParameter+=" "+parameterSelectionGeneral.getParameterFinalTypeSign(ParameterDbType.MYSQL);
					queryParameter+=" "+parameterSelectionGeneral.getParameterFinalValue()+" )";						
					queryParameter+=" "+parameterSelectionGeneral.getNextOperator(ParameterDbType.MYSQL);
					
				}
				
				if(parameterSelectionGeneral.getParameterType().equals(ParameterType.STRING))
				{
					queryParameter+=parameterSelectionGeneral.getParameterInitialTypeSign(ParameterDbType.MYSQL);
					queryParameter+=" '"+parameterSelectionGeneral.getParameterInitialValue()+"'";		
					queryParameter+=" and "+ parameterSelectionGeneral.getColumnName();
					queryParameter+=" "+parameterSelectionGeneral.getParameterFinalTypeSign(ParameterDbType.MYSQL);
					queryParameter+=" '"+parameterSelectionGeneral.getParameterFinalValue()+"' )";							
					queryParameter+=" "+parameterSelectionGeneral.getNextOperator(ParameterDbType.MYSQL);
				
				}
	}
	
	return queryParameter;
}
}
