if(GetEsReporteParametroFromPropertiesC(column) || !GetShowTableColumnFromPropertiesC(column) 
		|| column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
	{
		return "";
	}
	
	

public String GetParameterBindingClase(ColumnSchema column)
{
	String sBindingColumnTable="";
	
	if(GetEsReporteParametroFromPropertiesC(column) || !GetShowTableColumnFromPropertiesC(column) 
		|| column.Name==strIsActive||column.Name==strIsExpired||column.Name==strVersionRow)
	{
		return "";
	}
		
	String sPrefijo=String.Empty;
	String sPrefijoTabla=GetPrefijoTablaC().ToLower();
	String sPrefijoTipo =GetPrefijoTipoC(column);

	sPrefijo=sPrefijoTabla+sPrefijoTipo;
	
	String sNombre = GetNombreColumnaClaseC(column);
	sPrefijo+=sNombre;
	
	String sTitulo=GetWebNombreTituloColumnFromPropertiesC(column);
		
	String param =  GetTipoColumnaClaseC(column);
	
	//return param+" "+sPrefijo+";";
	
	String sTabColumnaOculta=String.Empty;
	String sIfColumnaOculta=String.Empty;
	String sIfColumnaOcultaFin=String.Empty;
	String strSufijoListenerTotalizar="";
	
	if(column.Name!=strId)
	{
		if(!column.IsForeignKeyMember) {
			sBindingColumnTable="\r\n\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+"=jTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+sPrefijo+"}\"),\""+sPrefijo+"\");\r\n";
			
			if(ValidacionCampoTotalizarC(column)) {
				strSufijoListenerTotalizar="Valor";
			}
			
			//sBindingColumnTable="\r\n\t\tjTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+sPrefijo+"}\"));\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnName(\""+sTitulo+"\");\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditable(true);\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnClass("+param+".class);\r\n";			
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".addBindingListener(new BindingListener"+GetNombreClaseC(TablaBase.ToString())+"Tabla());\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".addPropertyChangeListener(new Property"+strSufijoListenerTotalizar+"ChangeListener"+GetNombreClaseC(TablaBase.ToString())+"());\r\n";
			
			if(EsDateColumn(column)) {
				sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setRenderer(new DateRenderer());\r\n";
				sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditor(new DateEditorRenderer());\r\n";
				//sBindingColumnTable+="\t\t//columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setConverter(new DateConverterFromDate<Date,String>());\r\n";//".setConverter(new DateConverter<String,Date>());\r\n";
			
			} else if(EsBitColumn(column)) {
				sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setRenderer(new BooleanRenderer());\r\n";
				sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditor(new BooleanEditorRenderer());\r\n";
				//sBindingColumnTable+="\t\t//columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setConverter(new DateConverterFromDate<Date,String>());\r\n";//".setConverter(new DateConverter<String,Date>());\r\n";
			}
		} else {
			//PRIMERA VERSION DE FOREIGN KEY DONDE CODIGO ES IGUAL AL IF ANTERIOR
			/*
			sBindingColumnTable="\r\n\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+"=jTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+sPrefijo+"}\"));\r\n";
			
			//sBindingColumnTable="\r\n\t\tjTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+sPrefijo+"}\"));\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnName(\""+sTitulo+"\");\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditable(false);\r\n";
			sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnClass("+param+".class);\r\n";
			*/
			String sParametro="";
			
			//ES LA COLUMNA DESCRIPCION DEL FOREIGN KEY POR ESO EL TIPO ES STRING
			if(EsColumnaVariableGlobalDataBaseFromPropertiesC(column) 
				|| EsColumnaVariableModuloGlobalC(column)
				|| GetNoInsertEditColumnFromPropertiesC(column)) {
				sTabColumnaOculta="\t";
				sIfColumnaOculta="\r\n\t"+sTabColumnaOculta+"if(Constantes.ISDEVELOPING) {\r\n";
				sIfColumnaOcultaFin="\t"+sTabColumnaOculta+"}";	
			}			
				sBindingColumnTable+=sIfColumnaOculta;
				
				//COMENTAR Y DESCOMENTAR PARA HABILITAR O NO TieneColumnaComboTablaNormalC(column)
				if(!(ConSwingCombosTabla && TieneColumnaComboTablaNormalC(column))) {
					sBindingColumnTable+="\r\n\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+"=jTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+GetPrefijoRelacionC().ToLower()+GetNombreClaseObjetoC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+strDescripcion+"}\"),\""+GetPrefijoRelacionC().ToLower()+GetNombreClaseObjetoC("dbo."+GetNombreClaseRelacionadaFromColumn(column))+strDescripcion+"\");\r\n";
					
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnName(\""+sTitulo+"\");\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditable(false);\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnClass(String.class);\r\n";			
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".addBindingListener(new BindingListener"+GetNombreClaseC(TablaBase.ToString())+"Tabla());\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".addPropertyChangeListener(new PropertyChangeListener"+GetNombreClaseC(TablaBase.ToString())+"());\r\n";
				
				} else {
					sBindingColumnTable+="\r\n\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+"=jTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+GetNombreCompletoColumnaClaseC(column)+"}\"),\""+GetNombreCompletoColumnaClaseC(column)+"\");\r\n";
					
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnName(\""+sTitulo+"\");\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditable(true);\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnClass(Long.class);\r\n";			
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".addBindingListener(new BindingListener"+GetNombreClaseC(TablaBase.ToString())+"Tabla());\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".addPropertyChangeListener(new PropertyChangeListener"+GetNombreClaseC(TablaBase.ToString())+"());\r\n";	
					
					
					
					
					
					if(!TieneColumnaComboTablaNormalC(column) && blnConSwingCombosDinamicosTabla) {
						sParametro="this.jComboBox"+GetNombreCompletoColumnaClaseC(column)+GetNombreClaseC(TablaBase.ToString())+"ParaTabla";	
					} else {
						sParametro="this."+GetNombreCompletoClaseRelacionadaFromColumn(column).ToLower()+"sForeignKey";
					}					
					
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setRenderer(new "+GetNombreCompletoClaseRelacionadaFromColumn(column)+"TableCell("+sParametro+"));\r\n";
					sBindingColumnTable+="\t\t"+sTabColumnaOculta+"columnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditor(new "+GetNombreCompletoClaseRelacionadaFromColumn(column)+"TableCell("+sParametro+"));\r\n";//this."+GetNombreCompletoClaseRelacionadaFromColumn(column).ToLower()+"sForeignKey
					
				}
				
				sBindingColumnTable+=sIfColumnaOcultaFin;
		}
	}
	else
	{
		//OJO:ID DEBE SER EDITABLE SINO NO PERMITE CLICK BOTON
		sBindingColumnTable="\r\n\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+"=jTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+strId+"}\"),\""+strId+"\");\r\n";
		
		//sBindingColumnTable="\r\n\t\tjTableBinding"+GetNombreClaseC(TablaBase.ToString())+".addColumnBinding(ELProperty.create(\"${"+strId+"}\"));\r\n";
		sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnName(\""+strIdGetSet.ToUpper()+"\");\r\n";
		sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setEditable(true);\r\n";
		sBindingColumnTable+="\t\tcolumnBinding"+GetNombreClaseC(TablaBase.ToString())+".setColumnClass("+param+".class);\r\n";
	}
	
	return sBindingColumnTable;
}