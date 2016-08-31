function check_mail( addressField,req ) {
	mensaje="";
	
	if(req=='s')
	{
		if(addressField.value=="")
		{
			mensaje="Error: Campo requerido" ;
		}
	}
	
	if(addressField.value!="")
	{
	    if ( noAtSign ( addressField.value ) )
	    	mensaje="Error: El mail no contiene un '@'" ;
	    else if ( nothingBeforeAt ( addressField.value ) )
	    	mensaje="Error:El email no contiene al menos un caracter antes del '@'" ;
	    else if ( noLeftBracket ( addressField.value ) )
	    	mensaje= "Error: The E-Mail address contains a right square bracket ']',\nbut no corresponding left square bracket '['" ;
	    else if ( noRightBracket ( addressField.value ) )
	    	mensaje="Error: The E-Mail address contains a left square bracket '[',\nbut no corresponding right square bracket ']'" ;
	    else if ( noValidPeriod ( addressField.value ) )
	    	mensaje="Error: El mail deberia contener un punto ('.') " ;
	    else if ( noValidSuffix ( addressField.value ) )
	    	mensaje="Error: El mail deberia contener dos o tres caracteres sufijos" ;
	}   
   
    return mensaje;
}

function linkCheckValidation ( formField ) {
    if ( checkValidation ( formField ) == true ) {
        alert ( 'E-Mail Address Validates OK' );
    }

    return ( false );
}

function stringEmpty ( address ) {
    // CHECK THAT THE STRING IS NOT EMPTY
    if ( address.length < 1 ) {
        return ( true );
    } else {
        return ( false );
    }
}

function noAtSign ( address ) {
    // CHECK THAT THERE IS AN '@' CHARACTER IN THE STRING
    if ( address.indexOf ( '@', 0 ) == -1 ) {
        return ( true )
    } else {
        return ( false );
    }
}

function nothingBeforeAt ( address ) {
    // CHECK THERE IS AT LEAST ONE CHARACTER BEFORE THE '@' CHARACTER
    if ( address.indexOf ( '@', 0 ) < 1 ) {
        return ( true )
    } else {
        return ( false );
    }
}

function noLeftBracket ( address ) {
    // IF EMAIL ADDRESS IN FORM 'user@[255,255,255,0]', THEN CHECK FOR LEFT BRACKET
    if ( address.indexOf ( '[', 0 ) == -1 && address.charAt ( address.length - 1 ) == ']' ) {
        return ( true )
    } else {
        return ( false );
    }
}

function noRightBracket ( address ) {
    // IF EMAIL ADDRESS IN FORM 'user@[255,255,255,0]', THEN CHECK FOR RIGHT BRACKET
    if ( address.indexOf ( '[', 0 ) > -1 && address.charAt ( address.length - 1 ) != ']' ) {
        return ( true );
    } else {
        return ( false );
    }
}

function noValidPeriod ( address ) {
    // IF EMAIL ADDRESS IN FORM 'user@[255,255,255,0]', THEN WE ARE NOT INTERESTED
    if ( address.indexOf ( '@', 0 ) > 1 && address.charAt ( address.length - 1 ) == ']' )
        return ( false );

    // CHECK THAT THERE IS AT LEAST ONE PERIOD IN THE STRING
    if ( address.indexOf ( '.', 0 ) == -1 )
        return ( true );

    return ( false );
}

function noValidSuffix ( address ) {
    // IF EMAIL ADDRESS IN FORM 'user@[255,255,255,0]', THEN WE ARE NOT INTERESTED
    if ( address.indexOf ( '@', 0 ) > 1 && address.charAt ( address.length - 1 ) == ']' )
        return ( false );

    // CHECK THAT THERE IS A TWO OR THREE CHARACTER SUFFIX AFTER THE LAST PERIOD
    var len = address.length;
    var pos = address.lastIndexOf ( '.', len - 1 ) + 1;
    if ( ( len - pos ) < 2 || ( len - pos ) > 3 ) {
        return ( true );
    } else {
        return ( false );
    }
}

