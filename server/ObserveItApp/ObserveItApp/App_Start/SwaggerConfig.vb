Imports System.Web.Http
Imports WebActivatorEx
Imports Swashbuckle.Application
Imports ObserveItApp.ObserveItAppApi

<Assembly: PreApplicationStartMethod(GetType(SwaggerConfig), "Register")>
Namespace ObserveItAppApi
	Public Class SwaggerConfig
		Public Shared Sub Register()
			Dim thisAssembly = GetType(SwaggerConfig).Assembly
			GlobalConfiguration.Configuration.EnableSwagger(Sub(c)
																c.SingleApiVersion("v1", "ObserveItApp")
															End Sub).EnableSwaggerUi(Sub(c)
																					 End Sub)
		End Sub
	End Class
End Namespace
