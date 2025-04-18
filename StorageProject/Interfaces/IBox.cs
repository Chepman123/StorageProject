namespace StorageProject.Interfaces
{
    public interface IBox
    {
        void ShowInfo();
        void ChangeInfo(string objectsName, int count);
        void MoveBox(int id_section);
    }
}
