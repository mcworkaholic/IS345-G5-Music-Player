## While in the specific directory:
> **Note:**
> For this project, all built .exe 's will be placed by me in the "Utilities" folder, and you should use the virtual environment workflow specific to your use case.

### install dependencies:

```sh
pip install -r requirements.txt
 ```

### build to .exe :

```sh
pyinstaller --onefile music_dir_finder.py
 ```