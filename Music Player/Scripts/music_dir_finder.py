import argparse
import os
import glob

def find_music_directories(begin_directory):
    """
    Returns a list of directories containing music files under the given root directory.
    """
    music_directories = []
    for dirpath, dirnames, filenames in os.walk(begin_directory):
        for extension in ('*.mp3', '*.flac'):
            music_files = glob.glob(os.path.join(dirpath, extension))
            if len(music_files) > 0:
                music_directories.append(dirpath)
                break
    return music_directories

def main():
    # Create the parser
    parser = argparse.ArgumentParser(description='Find directories containing music files.')
    parser.add_argument('root_directory', metavar='ROOT_DIRECTORY', type=str,
                        help='the root directory to search for music files')

    # Parse the arguments
    args = parser.parse_args()

    # Call the function
    music_directories = find_music_directories(args.root_directory)

    # Print the results
    if len(music_directories) == 0:
        print('No directories containing audio found under {}'.format(args.root_directory))
    else:
        print('found directories containing audio under {}:'.format(args.root_directory))
        print('------------------------------------------------------------------------------------------------------------------------')
        for directory in music_directories:
            print(directory)
            print('------------------------------------------------------------------------------------------------------------------------')

if __name__ == '__main__':
    main()