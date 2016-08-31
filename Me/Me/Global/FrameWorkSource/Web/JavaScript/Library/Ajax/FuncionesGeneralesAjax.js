function ValidarFormularioSeleccionado(tabla)  
{
	var retorno =true;
	var strId=document.frmMantenimiento.hdnIdActual.value;
	
	if(strId==""||strId==undefined)
	{
		retorno= false;
		createSimpleYahooDialogErrorValidacion('Validacion',"Debe seleccionar un"+tabla,document.frmMantenimiento.hdnIdActual);
	}
	return retorno;
}

function ProcesarMensaje(itemMensaje) 
{
if(itemMensaje[0]!=null)
{
	var mensajeusuario= itemMensaje[0].getElementsByTagName("mensajeusuario")[0].firstChild.nodeValue;
	var tipo= itemMensaje[0].getElementsByTagName("tipo")[0].firstChild.nodeValue;
	var nombremensaje= itemMensaje[0].getElementsByTagName("nombremensaje")[0].firstChild.nodeValue;
	var esopcional= itemMensaje[0].getElementsByTagName("esopcional")[0].firstChild.nodeValue;
	var titulo= itemMensaje[0].getElementsByTagName("titulo")[0].firstChild.nodeValue;
	
	var mensajetecnico; 
	
	if(itemMensaje[0].getElementsByTagName("mensajetecnico")[0].firstChild.nodeValue!="NONE")
	{
			mensajetecnico=itemMensaje[0].getElementsByTagName("mensajetecnico")[0].firstChild.nodeValue;
	}
	
	
	if(mensajeusuario!="NONE")
	{
		if(nombremensaje=="noexistencia"&&esopcional=="true"&&mostrarmensajenoexistencia)
		{
			createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_INFO);
			return;
		}
		if(nombremensaje=="nuevo"&&esopcional=="true"&&mostrarmensajenuevo)
		{
			createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_INFO);
			return;
		}
		if(nombremensaje=="actualizar"&&esopcional=="true"&&mostrarmensajeactualizar)
		{
			createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_INFO);
			return;
		}
		if(nombremensaje=="eliminar"&&esopcional=="true"&&mostrarmensajeeliminar)
		{
			createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_INFO);
			return;
		}
		if(nombremensaje=="deepload"&&esopcional=="true"&&mostrarmensajecargar)
		{
			createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_INFO);
			return;
		}
		if(nombremensaje=="excepcion")
		{
			createSimpleYahooDialogInfo(titulo,mensajeusuario+ mensajetecnico,YAHOO.widget.SimpleDialog.ICON_BLOCK);
			return;
		}
		
		createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_INFO);
			return;
	}
}
}




var campo;

function ValidarCombo(arrData,value)
{
	var mensaje= "";
	var encontrado=getIdCombo(arrData,value);
	
	if(encontrado=="")
	{
	mensaje="Error: Seleccione un elemento valido";
	}
	return mensaje;
}

function getIdCombo(arrData,value)
{

var id= "";

for (var I = 0 ; I < arrData.length ; I++)
{  
	if(arrData[I].label==value)
	{
	id=arrData[I].targetId;
	encontrado=true;
	}
}
   return id;
}

function createSimpleYahooDialogErrorValidacionMensajes(header,text) 
{
	if(text==""||text==undefined)
	{
		return;
	}
	
	var cfg = {
        width: "300px",
        fixedcenter: true,
        header: header,
        text: text,
        draggable: true,
        close: true,
        visible: true,
        modal: false,
        icon: YAHOO.widget.SimpleDialog.ICON_BLOCK,
        constraintoviewport: true,
        buttons: [ 
        { label:"Yes" },
        { label:'No', isDefault:true }
        ]
    };
	
	cfg.buttons = createDialogTempButtons();
	var dlg =  new YAHOO.widget.SimpleDialog("dialogTemp" , cfg);
	
	dlg.setHeader(cfg.header);
    dlg.render(document.body); 
	dlg.show(); 

}

function createSimpleYahooDialogErrorValidacionMensajes2(id,header,text) 
{
	if(text==""||text==undefined)
	{
		return;
	}
	
	var cfg = {
        width: "300px",
        fixedcenter: true,
        header: header,
        text: text,
        draggable: true,
        close: true,
        visible: true,
        modal: false,
        icon: YAHOO.widget.SimpleDialog.ICON_BLOCK,
        constraintoviewport: true,
        buttons: [ 
        { label:"Yes" },
        { label:'No', isDefault:true }
        ]
    };
	
	cfg.buttons = createDialogTempButtons();
	var dlg =  new YAHOO.widget.SimpleDialog("dialogTemp"+id , cfg);
	
	dlg.setHeader(cfg.header);
    dlg.render(document.body); 
	dlg.show(); 

}

function createSimpleYahooDialogErrorValidacion(header,text,newCampo) 
{
	if(text==""||text==undefined)
	{
		return;
	}
	campo=newCampo;
	var cfg = {
        width: "300px",
        fixedcenter: true,
        header: header,
        text: text,
        draggable: true,
        close: true,
        visible: true,
        modal: false,
        icon: YAHOO.widget.SimpleDialog.ICON_BLOCK,
        constraintoviewport: true,
        buttons: [ 
        { label:"Yes" },
        { label:'No', isDefault:true }
        ]
    };
	
	cfg.buttons = createDialogTempButtonsValidacion();
	var dlg =  new YAHOO.widget.SimpleDialog("dialogTemp" , cfg);
	
	dlg.setHeader(cfg.header);
    dlg.render(document.body); 
	dlg.show(); 

}

function createSimpleYahooDialogErrorValidacion2(id,header,text,newCampo) 
{
	
	if(text==""||text==undefined)
	{
		return;
	}
	campo=newCampo;
	var cfg = {
        width: "300px",
        fixedcenter: true,
        header: header,
        text: text,
        draggable: true,
        close: true,
        visible: true,
        modal: false,
        icon: YAHOO.widget.SimpleDialog.ICON_BLOCK,
        constraintoviewport: true,
        buttons: [ 
        { label:"Yes" },
        { label:'No', isDefault:true }
        ]
    };
	
	cfg.buttons = createDialogTempButtonsValidacion();
	var dlg =  new YAHOO.widget.SimpleDialog("dialogTemp"+id , cfg);
	
	dlg.setHeader(cfg.header);
    dlg.render(document.body); 
	dlg.show(); 

}

function createDialogTempButtonsValidacion() 
{
        var ybs = [];	 
        var yb = {};
        yb.text = 'OK';
        yb.handler = onClickDialogTempValidacion;            
        ybs.push(yb); 
        return ybs;
}

function onClickDialogTempValidacion(evt) {      
		this.hide();
	
		if(campo!=null)
		{ 
			//campo.select();
			//campo.focus();
		
		}
		 
			
    };  
	


function onClickDialogTemp(evt) {      
        this.hide();
    };  
	
function createDialogTempButtons() 
{
        var ybs = [];	 
        var yb = {};
        yb.text = 'OK';
        yb.handler = onClickDialogTemp;            
        ybs.push(yb); 
        return ybs;
}

function createSimpleYahooDialogInfo(header,text) 
{
	var cfg = {
        width: "300px",
        fixedcenter: true,
        header: header,
        text: text,
        draggable: true,
        close: true,
        visible: true,
        modal: false,
        icon: YAHOO.widget.SimpleDialog.ICON_INFO,
        constraintoviewport: true,
        buttons: [ 
        { label:"Yes" },
        { label:'No', isDefault:true }
        ]
    };
	
	cfg.buttons = createDialogTempButtons();
	var dlg =  new YAHOO.widget.SimpleDialog("dialogTemp" , cfg);
	
	dlg.setHeader(cfg.header);
    dlg.render(document.body); 
	dlg.show(); 

}

function createSimpleYahooDialogInfo(header,text,icon) 
{
	var cfg = {
        width: "300px",
        fixedcenter: true,
        header: header,
        text: text,
        draggable: true,
        close: true,
        visible: true,
        modal: false,
        icon: icon,
        constraintoviewport: true,
        buttons: [ 
        { label:"Yes" },
        { label:'No', isDefault:true }
        ]
    };
	
	cfg.buttons = createDialogTempButtons();
	var dlg =  new YAHOO.widget.SimpleDialog("dialogTemp" , cfg);
	
	dlg.setHeader(cfg.header);
    dlg.render(document.body); 
	dlg.show(); 

}



