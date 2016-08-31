<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
		
<script type="text/javascript" language="javascript">
/*window.opener.close()*/

var isExpanded=true;

 function getElementByIds() 
{
	var symbol="";
	var frameSetCols=document.getElementById('columnsFrameSet');
	
	if(isExpanded==true)
	{
		//document.all.columnsFrameSet.cols="45,*";
		//document.body.cols("45,*");
		frameSetCols.cols="0,*";
		isExpanded=false;
		symbol="expand.gif";
	}
	else
	{
		//document.all.columnsFrameSet.cols="200,*";
		//document.body.cols("200,*");
		frameSetCols.cols="200,*";
		isExpanded=true;
		symbol="collapse.gif";
	}
	
	return symbol;
}   
</script>
<frameset id="columnsFrameSet" name="columnsFrameSet" rows="*" cols="200,*" frameborder="NO" border="0" framespacing="0">
  <frame src="FinIzquierdaArbol.jsp" name="leftFrame" scrolling="YES" resize>
  <frameset rows="200,*" frameborder="NO" border="0" framespacing="0">
    <frame src="FinCableceraMenu.jsp" name="topFrame" scrolling="NO" resize>
    <frame src="FinContenido.jsp" name="mainFrame">
  </frameset>
</frameset>
<noframes><body>
</body></noframes>
</html>
