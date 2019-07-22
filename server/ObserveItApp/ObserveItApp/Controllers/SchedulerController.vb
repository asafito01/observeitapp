Imports System.Net
Imports System.Web.Http

<RoutePrefix("api/v1/scheduler")>
Public Class SchedulerController
	Inherits ApiController

	Private ReadOnly SchedulerService As ISchedulerService

	Public Sub New(_SchedulerService As ISchedulerService)
		' Initiate flights service
		If _SchedulerService Is Nothing Then Throw New ArgumentNullException(NameOf(_SchedulerService))
		SchedulerService = _SchedulerService
	End Sub

	''' <summary>
	'''  Gets all the scheduler data
	''' </summary>
	''' <returns></returns>
	<HttpGet>
	<Route("")>
	Public Function GetAll() As IHttpActionResult
		' Get appointments
		Dim appointmentList As List(Of AppointmentModel) = SchedulerService.GetAppointments()
		If appointmentList Is Nothing Then Return NotFound()

		' Create the final data
		Dim schedulerRows As New List(Of SchedulerRowViewModel)

		' Get the schedule hours allowed
		Dim activeHours() As Integer = SchedulerService.GetSchedulerActiveHours
		For hour As Integer = activeHours(0) + 1 To activeHours(1)
			Dim startHour = New TimeSpan(hour - 1, 0, 0)
			Dim endHour = New TimeSpan(hour, 0, 0)

			' Set a scheduler row to insert the data
			Dim scheduleRow As New SchedulerRowViewModel
			scheduleRow.StartHour = startHour
			scheduleRow.EndHour = endHour.Subtract(New TimeSpan(0, 1, 0))

			' Loop all the appointments available in these range of hours and add the user if not added already
			For Each appointmentData In appointmentList.Where(Function(x) (startHour >= x.StartHour And startHour < x.EndHour)).ToList
				If Not scheduleRow.Participants.Any(Function(x) x = appointmentData.User.Name) Then
					scheduleRow.Participants.Add(appointmentData.User.Name)
				End If
			Next

			' Add it to the rows
			schedulerRows.Add(scheduleRow)
		Next
		Return Ok(schedulerRows)
	End Function

	''' <summary>
	''' Creates a new appointment
	''' </summary>
	''' <param name="appointmentData"></param>
	''' <returns></returns>
	<HttpPost>
	<Route("")>
	Public Function PostAppointment(<FromBody> appointmentData As AppointmentModel) As IHttpActionResult
		Dim newAppointmentData As AppointmentModel = SchedulerService.CreateAppointment(appointmentData)
		If newAppointmentData Is Nothing Then Return BadRequest("Could not insert")
		Return Ok(newAppointmentData)
	End Function

End Class
