namespace TextGrunt.Services
{
    public interface IDialogService
    {
        string GetUserTextInput(string header, string question, string initialResponse);

        string GetUserFileOpenInput();

        string GetUserFileSaveInput();

        void ShowError(string text);

        bool ShowOkCancel(string text);

        void ShowAbout();

        void ShowOptions();
    }
}