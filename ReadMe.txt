Sample Mvc Application -
Environment- 
Front End- .Net  Framework 4.7(Visual Studio 2017)
Back End- Mdf file 

A. Following screens are implemented
	1. User Screen
	2. Role Screen	
		2.1 Menus will be added from backend and Menus will be assigned to Roles using the screen 
B. Data communication is achieved by ajax calls
	1. Implemented functionality to save the Searched values in session for reuse (refer: GetSearchSession, SetSearchSession,ClearSearchSession)
		Note: If the menu is clicked the Searched values in session are cleared
C. Session concept is implemented(refer: SessionHelper)
D. Cookies are used to stored the user info in client browser
	1. Cookie 1 is used to store the authentication information(refer: GetAuthCookieName)
	2. Cookie 2 is used for 'Remember Me' functionality(refer: GetCookieName)
E. Used jquery.datatable, fontawesome,twbs-pagination-master
F. Implemented concept of encrypting the paramters in the url(refer :RouteDataToEncrytedString)
G. Implemented AuthorizeAttribute for authentication(refer: CustomAuthorizationAttr)
H. Implelemted IPrincipal interface(refer: CustomPrincipal)
H. Implemented WebViewPage concept(refer: CustomWebViewPage)
I. Used CustomHelpers(refer: CustomHelpers)
J. Used Unobstruvie for client side functionality
K. Implemented Factory pattern for DAL

 