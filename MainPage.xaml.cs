namespace MovieSurvey;

public partial class MainPage : ContentPage
{
	DateTime defaultDate = new DateTime(2025, 1, 1);
	double defaultRating = 5;
	double defaultFilms = 2;

	public MainPage()
	{
		InitializeComponent();
	}

	void OnFormChanged(object? sender, EventArgs e)
	{
		ClearButton.IsEnabled =
			!string.IsNullOrEmpty(NameEntry.Text) ||
			!string.IsNullOrEmpty(EmailEntry.Text) ||
			!string.IsNullOrEmpty(CommentEditor.Text) ||
			GenrePicker.SelectedIndex >= 0 ||
			GetSelectedRadio() != null ||
			SeriesCheck.IsChecked ||
			AnimeCheck.IsChecked ||
			NewsSwitch.IsToggled ||
			VisitDatePicker.Date != defaultDate ||
			RatingSlider.Value != defaultRating ||
			FilmsStepper.Value != defaultFilms;
	}

	RadioButton? GetSelectedRadio()
	{
		if (RadioRare.IsChecked) return RadioRare;
		if (RadioOften.IsChecked) return RadioOften;
		if (RadioDaily.IsChecked) return RadioDaily;
		return null;
	}

	async void OnShowClicked(object? sender, EventArgs e)
	{
		RadioButton? radio = GetSelectedRadio();

		if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
			GenrePicker.SelectedIndex < 0 ||
			radio == null ||
			(NewsSwitch.IsToggled && string.IsNullOrWhiteSpace(EmailEntry.Text)))
		{
			await DisplayAlert("Ошибка", "Заполните имя, жанр, частоту просмотра (и e-mail, если включены новости)", "OK");
			return;
		}

		string more = "";
		if (SeriesCheck.IsChecked) more += "сериалы ";
		if (AnimeCheck.IsChecked) more += "аниме ";
		if (more == "") more = "ничего";

		ResultLabel.Text =
			"Имя: " + NameEntry.Text + "\n" +
			"E-mail: " + (NewsSwitch.IsToggled ? EmailEntry.Text : "нет") + "\n" +
			"Новости: " + (NewsSwitch.IsToggled ? "да" : "нет") + "\n" +
			"Жанр: " + GenrePicker.Items[GenrePicker.SelectedIndex] + "\n" +
			"Частота: " + radio.Value + "\n" +
			"Ещё смотрит: " + more + "\n" +
			"Последний поход в кино: " + VisitDatePicker.Date.ToString("dd.MM.yyyy") + "\n" +
			"Оценка: " + RatingSlider.Value.ToString("F0") + "\n" +
			"Фильмов в месяц: " + FilmsStepper.Value.ToString("F0") + "\n" +
			"Любимый фильм: " + CommentEditor.Text;
		ResultLabel.IsVisible = true;
	}

	void OnClearClicked(object? sender, EventArgs e)
	{
		NameEntry.Text = "";
		EmailEntry.Text = "";
		NewsSwitch.IsToggled = false;
		GenrePicker.SelectedIndex = -1;
		RadioRare.IsChecked = false;
		RadioOften.IsChecked = false;
		RadioDaily.IsChecked = false;
		SeriesCheck.IsChecked = false;
		AnimeCheck.IsChecked = false;
		VisitDatePicker.Date = defaultDate;
		RatingSlider.Value = defaultRating;
		FilmsStepper.Value = defaultFilms;
		CommentEditor.Text = "";

		ResultLabel.Text = "";
		ResultLabel.IsVisible = false;
		ClearButton.IsEnabled = false;
	}
}
