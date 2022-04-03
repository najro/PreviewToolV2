# Preview Tool for figures


##  Description of file structure


|Files           |Description
|----------------|------------------------------- 
|[HtmlView/Scripts/Highcharts/9.3.1](/HtmlView/Scripts/Highcharts/9.3.1)|Contains offline version of all highcharts JavaScript files used on regjeringen.no. Files are referenced from |[Preview/Template/view-template.htm](/Preview/Template/view-template.htm)
|[HtmlView/Sample/sample.htm](/HtmlView/Sample/sample.htm)|Contains a simple html page displaying a Highchart figure and svg image. Open up in a browser to check it out. Reference to latest Highcharts JavaScript files online.
|[HtmlView/View/start.htm](/HtmlView/View/default.htm)|Landingpage displayed in view, to the right in tool using webview.
|[HtmlView/Template/view-template.htm](/HtmlView/Template/view-template.htm)|Html template for dynamic building up an html page per figure. The template is loaded and used to create a new html page matching the name of clickable items. Depending of what file formats the clickable items exist in, it will display a preview of figure as highchart (with help of json), svg, jpg or png. The file will be created under the [Preview](/Preview) folder. Any .htm files under [Preview](/Preview) folder will be deleted when the app is started.
|[HtmlView/Styles/view.css](/HtmlView/Styles/view.css)|Styling used in [view-template.htm](/HtmlView/Template/view-template.htm)


##  Technology used
- Visual Studio 2019 or Visual Studio 2022
- Forms Development (C#, .Net Framework 4.7.2)
- Version 9.3.1 of Highcharts locally downloaded
- Microsoft Edge WebView2 component to display Highcharts figures. 
	- https://docs.microsoft.com/en-us/microsoft-edge/webview2/
	- https://docs.microsoft.com/en-us/microsoft-edge/webview2/how-to/machine-setup
	- https://developer.microsoft.com/en-us/microsoft-edge/webview2/


## Additional information
Regarding Warning in Visual Studio for **microsoft.vclibs.desktop**
https://developercommunity.visualstudio.com/t/the-referenced-component-microsoftvclibs-could-not/849433