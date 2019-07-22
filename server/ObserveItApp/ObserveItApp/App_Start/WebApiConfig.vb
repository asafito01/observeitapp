Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports Unity
Imports Unity.AspNet.WebApi
Imports Unity.Lifetime

Public Module WebApiConfig
	Public Sub Register(ByVal config As HttpConfiguration)
		' Dependency injection resolver using Unity for WebApi
		Dim container = New UnityContainer

		' Inject persistent data
		container.RegisterInstance(Of SchedulerModel)(New SchedulerModel(8, 17), New ContainerControlledLifetimeManager())

		' Inject services
		container.RegisterType(Of ISchedulerService, SchedulerService)(New ContainerControlledLifetimeManager())

		config.DependencyResolver = New UnityDependencyResolver(container)

		' Web API configuration and services
		' Web API routes
		config.MapHttpAttributeRoutes()

		config.Formatters.XmlFormatter.SupportedMediaTypes.Clear()
		config.Formatters.JsonFormatter.Indent = True
	End Sub
End Module
