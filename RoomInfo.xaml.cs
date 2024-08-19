using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AC
{
    public partial class RoomInfo : ContentPage
    {
        private LessonService _lessonService;

        public RoomInfo()
        {
            InitializeComponent();
            _lessonService = new LessonService();
        }
        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScanWindow());
        }
        private async void OnStatisticsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistics());
        }

        private async void OnDesktopClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Desktop());
        }

        private async void OnLessonTapped(object sender, ItemTappedEventArgs e)
        {
            var lesson = e.Item as Lesson; // ��������������, ��� � ��� ���� ����� Lesson

            if (lesson != null)
            {
                string result = await DisplayPromptAsync("������� PIN-���", "������� �������������� PIN-���", "�����������", "������", keyboard: Keyboard.Numeric);

                if (result == "1234")
                {
                    await DisplayAlert("�����", "�� ������� ����������!", "OK");
                    await Navigation.PushAsync(new LessonInfo());
                }
                else
                {
                    await DisplayAlert("������", "PIN-��� ��������", "OK");
                }
            }
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var lessons = await _lessonService.GetLessonsAsync();
            lessonsListView.ItemsSource = lessons.Where(lesson => lesson.Room == roomNameEntry.Text); // ������ �� �������
        }

        private async void OnNewLessonButtonClicked(object sender, EventArgs e)
        {
            // ������� �� �������� ��� �������� ������ �����
            await Navigation.PushAsync(new NewLesson());
        }
    }
}
