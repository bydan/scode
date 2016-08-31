package ByDan.Framework.Business.Entities;

import ByDan.Framework.Utils.ParameterDbType;
import ByDan.Framework.Utils.ParametersMaintenance;
import ByDan.Framework.Utils.ParametersType;
import java.sql.*;

abstract public class GeneralEntity {

	private Long id;
	private Boolean isActive;
	private Boolean isExpired;
	private Timestamp versionRow;
	private boolean isNew;
	private boolean isChanged;
	private boolean isDeleted;
	
	protected ParametersMaintenance parametersMaintenance;
	
	protected int orderParameter;
	protected String queryInsert;
	protected String queryUpdate;
	protected String queryDelete;
	protected String querySelect;
	
	public Boolean getIsExpired() {
		return isExpired;
	}

	public void setIsExpired(Boolean isExpired) {
		this.isExpired = isExpired;
	}

	public int getOrderParameter() {
		return orderParameter;
	}

	public void setOrder(int newOrderParameter) {
		this.orderParameter = newOrderParameter;
	}
	
		
	public void BuildParametersMaintenance(ParameterDbType parameterDbType,ParametersType parametersType) {
		
	}
	public String getQuerySelect() {
		return querySelect;
	}

	public void setQuerySelect(String newQuerySelect) {
		this.querySelect = newQuerySelect;
	}
	
	public String getQueryDelete() {
		return queryDelete;
	}

	public void setQueryDelete(String newQueryDelete) {
		this.queryDelete = newQueryDelete;
	}

	public String getQueryInsert() {
		return queryInsert;
	}

	public void setQueryInsert(String newQueryInsert) {
		this.queryInsert = newQueryInsert;
	}

	public String getQueryUpdate() {
		return queryUpdate;
	}

	public void setQueryUpdate(String newQueryUpdate) {
		this.queryUpdate = newQueryUpdate;
	}
	
	public GeneralEntity()
	{
		id=0L;
		isNew=true;
		isActive=true;
		isChanged=false;
		isDeleted=false;
		isExpired=false;	
		versionRow=null;
	}
	
	public static String getColumnNameId()
	{
		return "id";
	}
	
	public static String getColumnNameIsActive()
	{
		return "isActive";
	}
	
	public static String getColumnNameIsExpired()
	{
		return "isExpired";
	}
	
	public static String getColumnNameVersionRow()
	{
		return "versionRow";
	}
	
	public boolean getIsDeleted()
	{
		return isDeleted;
	}
	
	public void setIsDeleted(boolean newIsDeleted)
	{
		isDeleted=newIsDeleted;
	}
	
	public void setIsNew(boolean newIsNew)
	{
		isNew=newIsNew;
	}
	
	public boolean getIsNew()
	{
		return isNew;
	}
		
	public boolean getIsChanged()
	{
		return isChanged;
	}
	
	public void setIsChanged(boolean newIsChanged)
	{
		isChanged=newIsChanged;
	}
	
	public void setId(Long newId)
	{
		if(id!=newId)
		{
			isChanged=true;
		}
		
		id=newId;
	}
			
	public Long getId()
	{
		return id;
	}
	
	public void setIsActive(Boolean newIsActive){
		
	if(isActive!=newIsActive){
			isChanged=true;
		}
		
		isActive=newIsActive;
	}
	
	public Boolean getIsActive(){
		return isActive;
	}
	
	public void setVersionRow(Timestamp newVersionRow){
		if(versionRow!=newVersionRow){
			isChanged=true;
		}
		
		versionRow=newVersionRow;
	}
	public Timestamp getVersionRow(){
		return versionRow;
	}

	public void setParametersMaintenance(ParametersMaintenance parametersMaintenance) {
		this.parametersMaintenance = parametersMaintenance;
	}
	public ParametersMaintenance getParametersMaintenance() {
		return this.parametersMaintenance;
	}
	

	
}
