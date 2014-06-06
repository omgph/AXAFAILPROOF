// JavaScript Document
<!--//
function changetmpl(){	
		window.location.href = 'system.php?LM=modules.setting&id='+document.getElementById("id").value+'&tmpl='+document.getElementById("tmpl").value;
		}
		
function changelinktmpl(){
	window.location.href = 'system.php?LM=links.setting&g='+document.getElementById("g").value+'&id='+document.getElementById("id").value+'&tmpl='+document.getElementById("tmpl").value;
	}		
	
function Ajax_load_page(url, target) {
  document.getElementById(target).innerHTML = '';
  if (window.XMLHttpRequest) {
    req = new XMLHttpRequest();
  } else if (window.ActiveXObject) {
    req = new ActiveXObject("Microsoft.XMLHTTP");
  }
  if (req != undefined) {
    req.onreadystatechange = function() {Ajax_load_page_Done(url, target);};
    req.open("GET", url, true);
    req.send("");
  }
}  

function Ajax_load_page_Done(url, target) {
  if (req.readyState == 4) { // only if req is "loaded"
    if (req.status == 200) { // only if "OK"
      document.getElementById(target).innerHTML = req.responseText;
    } else {
      document.getElementById(target).innerHTML=" AHAH Error:\n"+ req.status + "\n" +req.statusText;
    }
  }
}



function Ajax_page(name, div) {
    Ajax_load_page(name,div);
    return false;
}

//	AJAX FUNCTION TO GET VALUE FROM PAGE INTO STRING
function Ajax_load_value(url) {
if (window.XMLHttpRequest) {
    req = new XMLHttpRequest();
  } else if (window.ActiveXObject) {
    req = new ActiveXObject("Microsoft.XMLHTTP");
  }
  req.open("GET", url, false);
  req.send("");
  return req.responseText;	
}  





//	SUBMIT FORM VIA AJAX
   function makePOSTRequest(url, parameters) {
      http_request = false;
      if (window.XMLHttpRequest) { // Mozilla, Safari,...
         http_request = new XMLHttpRequest();
         if (http_request.overrideMimeType) {
         	// set type accordingly to anticipated content type
            //http_request.overrideMimeType('text/xml');
            http_request.overrideMimeType('text/html');
         }
      } else if (window.ActiveXObject) { // IE
         try {
            http_request = new ActiveXObject("Msxml2.XMLHTTP");
         } catch (e) {
            try {
               http_request = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {}
         }
      }
      if (!http_request) {
         alert('Cannot create XMLHTTP instance');
         return false;
      }

      http_request.onreadystatechange = alertContents;
      http_request.open('POST', url, true);
      http_request.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
      http_request.setRequestHeader("Content-length", parameters.length);
      http_request.setRequestHeader("Connection", "close");
      http_request.send(parameters);
   }

   function alertContents() {
      if (http_request.readyState == 4) {
         if (http_request.status == 200) {
            //alert(http_request.responseText);
            result = http_request.responseText;
       //     document.getElementById('myspan').innerHTML = result;            
         } else {
            alert('There was a problem with the request.');
         }
      }
   }
   
   function get(obj,objid) {
	   var message=document.getElementById("message"+objid).value;
      var poststr = "message=" + encodeURI( document.getElementById("message"+objid).value )
	  + "&toid=" + encodeURI( document.getElementById("toid"+objid).value )
	  + "&touser=" + encodeURI( document.getElementById("touser"+objid).value )
	  + "&fromuser=" + encodeURI( document.getElementById("fromuser"+objid).value )
	  + "&chatid=" + encodeURI( document.getElementById("chatid"+objid).value )
	  + "&fromid=" + encodeURI( document.getElementById("fromid"+objid).value );
      if(message)
	  makePOSTRequest('writer.php', poststr);
	  send(objid);
   }

	function Set_Cookie( name, value, expires, path, domain, secure )
	{
		// set time, it's in milliseconds
		var today = new Date();
		today.setTime( today.getTime() );
		
		/*
		if the expires variable is set, make the correct
		expires time, the current script below will set
		it for x number of days, to make it for hours,
		delete * 24, for minutes, delete * 60 * 24
		*/
		if ( expires )
		{
		expires = expires * 1000 * 60 * 60 * 24;
		}
		var expires_date = new Date( today.getTime() + (expires) );
		
		document.cookie = name + "=" +escape( value ) +
		( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) +
		( ( path ) ? ";path=" + path : "" ) +
		( ( domain ) ? ";domain=" + domain : "" ) +
		( ( secure ) ? ";secure" : "" );
	}

	// this fixes an issue with the old method, ambiguous values
	// with this test document.cookie.indexOf( name + "=" );
	function Get_Cookie( check_name ) {
		// first we'll split this cookie up into name/value pairs
		// note: document.cookie only returns name=value, not the other components
		var a_all_cookies = document.cookie.split( ';' );
		var a_temp_cookie = '';
		var cookie_name = '';
		var cookie_value = '';
		var b_cookie_found = false; // set boolean t/f default f
	
		for ( i = 0; i < a_all_cookies.length; i++ )
		{
			// now we'll split apart each name=value pair
			a_temp_cookie = a_all_cookies[i].split( '=' );
	
	
			// and trim left/right whitespace while we're at it
			cookie_name = a_temp_cookie[0].replace(/^\s+|\s+$/g, '');
	
			// if the extracted name matches passed check_name
			if ( cookie_name == check_name )
			{
				b_cookie_found = true;
				// we need to handle case where cookie has no value but exists (no = sign, that is):
				if ( a_temp_cookie.length > 1 )
				{
					cookie_value = unescape( a_temp_cookie[1].replace(/^\s+|\s+$/g, '') );
				}
				// note that in cases where cookie is initialized but no value, null is returned
				return cookie_value;
				break;
			}
			a_temp_cookie = null;
			cookie_name = '';
		}
		if ( !b_cookie_found )
		{
			return null;
		}
	}

	// this deletes the cookie when called
	function Delete_Cookie( name, path, domain ) {
		if ( Get_Cookie( name ) ) document.cookie = name + "=" +
		( ( path ) ? ";path=" + path : "") +
		( ( domain ) ? ";domain=" + domain : "" ) +
		";expires=Thu, 01-Jan-1970 00:00:01 GMT";
	}
	
	// Delete Record Function
	function del(url){
		var con=confirm("Are you sure you want to delete this record?");
		if(con){
			window.location.href=url;
		}
	}
	
	 var controls_width_max = '208px';
	 var controls_width_min = '28px';
	 var browserName=navigator.appName; 
	
	 if (browserName=="Microsoft Internet Explorer")
	 {
	  controls_width_max = '248px';
	  controls_width_min = '68px';
	 }
	
	// Menu and Controls Collapse Function
	function controls_collapse(){
		var controls_div = document.getElementById("controls").style;
		if(controls_div.width == controls_width_min){
			controls_div.width = controls_width_max;
			Set_Cookie( 'controls', 1, '', '/','','');
		} else {
			controls_div.width = controls_width_min;
			Delete_Cookie( 'controls','/','' );
		}
	}
	
	// enable nav Icons
	function enable_nav_icon(id){
			var icon = document.getElementById(id);
			icon.src = icon.src.replace("d_","e_");
		}
	// disable nav Icons
	function disable_nav_icon(id){
			var icon = document.getElementById(id);
			icon.src = icon.src.replace("e_","d_");
		}
		
	function Set_Cookie( name, value, expires, path, domain, secure )
	{
		// set time, it's in milliseconds
		var today = new Date();
		today.setTime( today.getTime() );
		
		/*
		if the expires variable is set, make the correct
		expires time, the current script below will set
		it for x number of days, to make it for hours,
		delete * 24, for minutes, delete * 60 * 24
		*/
		if ( expires )
		{
			expires = expires * 1000 * 60 * 60 * 24;
		}
		var expires_date = new Date( today.getTime() + (expires) );
		
		document.cookie = name + "=" +escape( value ) +
		( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) +
		( ( path ) ? ";path=" + path : "" ) +
		( ( domain ) ? ";domain=" + domain : "" ) +
		( ( secure ) ? ";secure" : "" );
	}

	// this fixes an issue with the old method, ambiguous values
	// with this test document.cookie.indexOf( name + "=" );
	function Get_Cookie( check_name ) {
		// first we'll split this cookie up into name/value pairs
		// note: document.cookie only returns name=value, not the other components
		var a_all_cookies = document.cookie.split( ';' );
		var a_temp_cookie = '';
		var cookie_name = '';
		var cookie_value = '';
		var b_cookie_found = false; // set boolean t/f default f
	
		for ( i = 0; i < a_all_cookies.length; i++ )
		{
			// now we'll split apart each name=value pair
			a_temp_cookie = a_all_cookies[i].split( '=' );
	
	
			// and trim left/right whitespace while we're at it
			cookie_name = a_temp_cookie[0].replace(/^\s+|\s+$/g, '');
	
			// if the extracted name matches passed check_name
			if ( cookie_name == check_name )
			{
				b_cookie_found = true;
				// we need to handle case where cookie has no value but exists (no = sign, that is):
				if ( a_temp_cookie.length > 1 )
				{
					cookie_value = unescape( a_temp_cookie[1].replace(/^\s+|\s+$/g, '') );
				}
				// note that in cases where cookie is initialized but no value, null is returned
				return cookie_value;
				break;
			}
			a_temp_cookie = null;
			cookie_name = '';
		}
		if ( !b_cookie_found )
		{
			return null;
		}
	}

	// this deletes the cookie when called
	function Delete_Cookie( name, path, domain ) {
		if ( Get_Cookie( name ) ) document.cookie = name + "=" +
		( ( path ) ? ";path=" + path : "") +
		( ( domain ) ? ";domain=" + domain : "" ) +
		";expires=Thu, 01-Jan-1970 00:00:01 GMT";
	}
	
	function hidemeta(id){
			var div = document.getElementById(id).style;
			var arrow = document.getElementById('arrow');
			if(div.display=='none'){
				div.display='block';
				arrow.src = '@Url.Content("~/Areas/Admin/Content/images/down.png';
			}else{
				div.display='none';
				arrow.src = '@Url.Content("~/Areas/Admin/Content/images/right.png'
			}
		}
	
	function filters(){
		document.filter.submit();
	}
	
	function filter_category(secid,catid){
		var recid = document.getElementById(secid).value;
		Ajax_load_page('module.php?LM=articles.category&id='+recid,catid);
	} 
//-->