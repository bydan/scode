package ByDan.Framework.Business.Logic;

import ByDan.Framework.Utils.ParameterDbType;
import ByDan.Framework.Utils.ParameterType;
import ByDan.Framework.Utils.ParameterTypeOperator;
import ByDan.Framework.Utils.ParameterTypeSign;

public class ParameterSelectionGeneral {
	
	 private ParameterType parameterType;
	 private String parameterInitialValue;
	 private String parameterFinalValue;
	 private ParameterTypeSign parameterInitialTypeSign;
	 private ParameterTypeSign parameterFinalTypeSign;
	 
	 private String columnName;
	 private ParameterTypeOperator nextOperator;
	 private boolean isEqual;
	 
	 public ParameterSelectionGeneral()
		{
			
		}
	 
	 public ParameterSelectionGeneral(ParameterType newParameterType,String newColumnName,
			 ParameterTypeSign newParameterInitialTypeSign,String newParameterInitialValue,
			 ParameterTypeSign newParameterFinalTypeSign,String newParameterFinalValue,ParameterTypeOperator newNextOperator)
		{
		 parameterType=newParameterType;
		 parameterInitialValue=newParameterInitialValue;
		 parameterFinalValue=newParameterFinalValue;
		 parameterInitialTypeSign=newParameterInitialTypeSign;
		 parameterFinalTypeSign=newParameterFinalTypeSign;
		 
		 columnName=newColumnName;
		 nextOperator=newNextOperator;
		 isEqual=false;
		}
	 
	 public void setParameterSelectionGeneral(ParameterType newParameterType,String newColumnName,
			 ParameterTypeSign newParameterInitialTypeSign,String newParameterInitialValue,
			 ParameterTypeSign newParameterFinalTypeSign,String newParameterFinalValue,ParameterTypeOperator newNextOperator)
		{
		 parameterType=newParameterType;
		 parameterInitialValue=newParameterInitialValue;
		 parameterFinalValue=newParameterFinalValue;
		 parameterInitialTypeSign=newParameterInitialTypeSign;
		 parameterFinalTypeSign=newParameterFinalTypeSign;
		 
		 columnName=newColumnName;
		 nextOperator=newNextOperator;
		 isEqual=false;
		}
	 public ParameterSelectionGeneral(ParameterType newParameterType,
			 String newParameterInitialValue,String newColumnName,ParameterTypeOperator newNextOperator)
		{
		    parameterType=newParameterType;
			parameterInitialValue=newParameterInitialValue;
			parameterInitialTypeSign=ParameterTypeSign.IGUAL;				 
			columnName=newColumnName;
			nextOperator=newNextOperator;
			isEqual=true;	
		}
	 
	 public void setParameterSelectionGeneralEqual(ParameterType newParameterType,
			 String newParameterInitialValue,String newColumnName,ParameterTypeOperator newNextOperator) {
			
		 	parameterType=newParameterType;
			parameterInitialValue=newParameterInitialValue;
			parameterInitialTypeSign=ParameterTypeSign.IGUAL;				 
			columnName=newColumnName;
			nextOperator=newNextOperator;
			isEqual=true;		
		}
	 
	 public void setParameterSelectionGeneralMayor(ParameterType newParameterType,
			 String newParameterInitialValue,String newColumnName,ParameterTypeOperator newNextOperator) {
			
		 	parameterType=newParameterType;
			parameterInitialValue=newParameterInitialValue;
			parameterInitialTypeSign=ParameterTypeSign.MAYOR;				 
			columnName=newColumnName;
			nextOperator=newNextOperator;
			isEqual=true;		
		}
	 
	 public void setParameterSelectionGeneralMayorIgual(ParameterType newParameterType,
			 String newParameterInitialValue,String newColumnName,ParameterTypeOperator newNextOperator) {
			
		 	parameterType=newParameterType;
			parameterInitialValue=newParameterInitialValue;
			parameterInitialTypeSign=ParameterTypeSign.MAYORIGUAL;				 
			columnName=newColumnName;
			nextOperator=newNextOperator;
			isEqual=true;		
		}
	 
	 public void setParameterSelectionGeneralMenor(ParameterType newParameterType,
			 String newParameterInitialValue,String newColumnName,ParameterTypeOperator newNextOperator) {
			
		 	parameterType=newParameterType;
			parameterInitialValue=newParameterInitialValue;
			parameterInitialTypeSign=ParameterTypeSign.MENOR;				 
			columnName=newColumnName;
			nextOperator=newNextOperator;
			isEqual=true;		
		}
	 public void setParameterSelectionGeneralMenorIgual(ParameterType newParameterType,
			 String newParameterInitialValue,String newColumnName,ParameterTypeOperator newNextOperator) {
			
		 	parameterType=newParameterType;
			parameterInitialValue=newParameterInitialValue;
			parameterInitialTypeSign=ParameterTypeSign.MENORIGUAL;				 
			columnName=newColumnName;
			nextOperator=newNextOperator;
			isEqual=true;		
		}
	 
	public String getColumnName() {
		return columnName;
	}

	public void setColumnName(String columnName) {
		this.columnName = columnName;
	}

	public boolean getIsEqual() {
		return isEqual;
	}

	public void setEqual(boolean isEqual) {
		this.isEqual = isEqual;
	}

	public ParameterTypeOperator getNextOperator() {
		return nextOperator;
	}

	public String getNextOperator(ParameterDbType dbType) {
		String strNextOperator="";
		if(dbType.equals(ParameterDbType.MYSQL))
		{
			if(nextOperator.equals(ParameterTypeOperator.AND))
			{
				strNextOperator=" and ";
			}
			if(nextOperator.equals(ParameterTypeOperator.OR))
			{
				strNextOperator=" or ";
			}
			if(nextOperator.equals(ParameterTypeOperator.NONE))
			{
				strNextOperator="";
			}
			
		}
		return strNextOperator;
	}
	
	public void setNextOperator(ParameterTypeOperator nextOperator) {
		this.nextOperator = nextOperator;
	}

	public ParameterTypeSign getParameterFinalTypeSign() {
		return parameterFinalTypeSign;
	}

	public String getParameterInitialTypeSign(ParameterDbType dbType) {
		String strTypeSign="";
		strTypeSign=getParameterTypeSign(dbType,parameterInitialTypeSign);
		return strTypeSign;
	}
	public String getParameterFinalTypeSign(ParameterDbType dbType) 
	{
		String strTypeSign="";
		strTypeSign=getParameterTypeSign(dbType,parameterFinalTypeSign);
		return strTypeSign;	
	}
	
	public String getParameterTypeSign(ParameterDbType dbType,ParameterTypeSign parameterTypeSign) 
	{
		String strTypeSign="";
		if(dbType.equals(ParameterDbType.MYSQL))
		{
			if(parameterTypeSign.equals(ParameterTypeSign.DONTLIKE))
			{
				strTypeSign=" not like ";
			}
			if(parameterTypeSign.equals(ParameterTypeSign.IGUAL))
			{
				strTypeSign=" = ";
			}
			if(parameterTypeSign.equals(ParameterTypeSign.LIKE))
			{
				strTypeSign=" like ";
			}
			if(parameterTypeSign.equals(ParameterTypeSign.MAYOR))
			{
				strTypeSign=" > ";
			}
			if(parameterTypeSign.equals(ParameterTypeSign.MAYORIGUAL))
			{
				strTypeSign=" >= ";
			}
			if(parameterTypeSign.equals(ParameterTypeSign.MENOR))
			{
				strTypeSign=" < ";
			}
			if(parameterTypeSign.equals(ParameterTypeSign.MENORIGUAL))
			{
				strTypeSign=" <= ";
			}

		}
		return strTypeSign;	
	}
	
	public void setParameterFinalTypeSign(ParameterTypeSign parameterFinalTypeSign) {
		this.parameterFinalTypeSign = parameterFinalTypeSign;
	}

	public String getParameterFinalValue() {
		return parameterFinalValue;
	}

	public void setParameterFinalValue(String parameterFinalValue) {
		this.parameterFinalValue = parameterFinalValue;
	}

	public ParameterTypeSign getParameterInitialTypeSign() {
		return parameterInitialTypeSign;
	}

	public void setParameterInitialTypeSign(
			ParameterTypeSign parameterInitialTypeSign) {
		this.parameterInitialTypeSign = parameterInitialTypeSign;
	}

	public String getParameterInitialValue() {
		return parameterInitialValue;
	}

	public void setParameterInitialValue(String parameterInitialValue) {
		this.parameterInitialValue = parameterInitialValue;
	}

	public ParameterSelectionGeneral
	(ParameterType newParameterType,String newParameterInitialValue,
	String newParameterFinalValue,ParameterTypeSign newParameterInitialTypeSign,ParameterTypeSign newParameterFinalTypeSign,
	String newColumnName,ParameterTypeOperator newNextOperator,boolean newIsEqual,String newQueryFinal)
	{	
	parameterType=newParameterType;
	parameterInitialValue=newParameterInitialValue;
	parameterFinalValue=newParameterFinalValue;
	parameterInitialTypeSign=newParameterInitialTypeSign;
	parameterFinalTypeSign=newParameterFinalTypeSign;
		 
	columnName=newColumnName;
	nextOperator=newNextOperator;
	isEqual=newIsEqual;			 
	}
	
	
		
	public ParameterType getParameterType() {
		return parameterType;
	}

	public void setParameterType(ParameterType newParameterType) 
	{
		this.parameterType = newParameterType;
	}
		
   
  
  }
