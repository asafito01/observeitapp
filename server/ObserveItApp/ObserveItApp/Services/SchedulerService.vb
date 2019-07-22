Imports ObserveItApp

Public Class SchedulerService
	Implements ISchedulerService

	Public Const ActiveHoursStart As Integer = 8
	Public Const ActiveHoursEnd As Integer = 17

	Private Scheduler As SchedulerModel
	Private Shared ReadOnly LockObject As New Object

	Public Sub New(_Scheduler As SchedulerModel)
		' Getting the persistent data - Later on can be replaced by other provider, such as DB, XML, Cache or any other data source
		' For this example we will use a singleton injected list of products who will be our source
		If _Scheduler Is Nothing Then Throw New ArgumentNullException(NameOf(_Scheduler))
		Scheduler = _Scheduler
	End Sub

	Public Function GetSchedulerActiveHours() As Integer() Implements ISchedulerService.GetSchedulerActiveHours
		Return {ActiveHoursStart, ActiveHoursEnd}
	End Function

	Public Function GetAppointments() As List(Of AppointmentModel) Implements ISchedulerService.GetAppointments
		Return Scheduler.Appointments
	End Function

	Public Function CreateAppointment(appointmentData As AppointmentModel) As AppointmentModel Implements ISchedulerService.CreateAppointment
		Try
			SyncLock LockObject
				Dim newAppointmentData As New AppointmentModel

				' Check if the active hours are correct
				newAppointmentData.StartHour = appointmentData.StartHour
				newAppointmentData.EndHour = appointmentData.EndHour
				If newAppointmentData.StartHour >= newAppointmentData.EndHour Then
					Return Nothing
				End If

				' Check if a user already exists, if not add it, otherwise get it. If it is nothing, then return error (nothing in our case)
				If String.IsNullOrEmpty(appointmentData.User.Name) Then Return Nothing
				Dim foundUser As UserModel = Scheduler.Users.FirstOrDefault(Function(x) x.Name = appointmentData.User.Name)
				If foundUser Is Nothing Then
					foundUser = New UserModel() With {.Name = appointmentData.User.Name}
					Scheduler.Users.Add(foundUser)
				End If
				newAppointmentData.User = foundUser ' Set the user to the appointment 

				Scheduler.Appointments.Add(newAppointmentData)
				Return newAppointmentData
			End SyncLock
		Catch ex As Exception
			Return Nothing
		End Try
	End Function
End Class
