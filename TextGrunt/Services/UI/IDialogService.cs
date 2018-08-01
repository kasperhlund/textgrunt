namespace TextGrunt.Services
{
    public interface IDialogService
    {
        string GetUserTextInput(string header, string question, string initialResponse, bool isMultiline = true);

        string GetUserFileOpenInput(string filter);

        string GetUserFileSaveInput(string filter);

        void ShowError(string text);

        void ShowSearch();

        bool ShowOkCancel(string text);

        void ShowAbout();

        void ShowOptions();
    }
}