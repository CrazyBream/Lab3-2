using Core.Entities;

namespace BLL.Services
{
    public interface IValidationService
    {
        void ValidateStudent(Student student);
        void ValidateMсdonaldsWorker(MсdonaldsWorker worker);
        void ValidateManager(Manager manager);
        bool ValidateFileName(string filename);
        bool ValidateFileFormat(string format);
    }
}