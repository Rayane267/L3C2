#!/bin/bash

createDesktop()
{
	rm -f install.tmp
	if [ $1 = "." ]; then
		a=`pwd`
	else
		a=$1
	fi
	if [ -e "$HOME/.local/bin/pause-cafe" ]; then
		rm -f "$HOME/.local/bin/pause-cafe"; fi

	echo -e "#!/bin/bash

path=$a"> "$HOME/.local/bin/pause-cafe"
	echo 'if [[ -z $1 ]]; then
	exec "$path/PauseCafe/pause_cafe.x86_64"
elif [[ $1 = "-u" ]]; then
	choice=n
	while [ ! -z "$choice" ]
	do
		read -p "Do you really want to delete Coffee break ? (Y/n) " choice
		if [ "$choice" = n ]; then
			echo "Quit..."
			exit
		elif [ "$choice" = y ]; then
			break
		fi
	done
	echo "Uninstalling in progress..."
	rm -rf "$path/PauseCafe/"
	rm -f "$HOME/.local/share/applications/pause_cafe.desktop"
	rm -f "$HOME/.local/bin/pause-cafe"
	echo "Done."
elif [[ $1 = "-h" ]]; then
	echo -e "\nUse -u to uninstall :
	pause-cafe -u\n"
fi' >> "$HOME/.local/bin/pause-cafe"
	chmod 711 "$HOME/.local/bin/pause-cafe"

	echo "Creating a desktop shortcut..."
	echo -e "[Desktop Entry]
Type=Application
Name=Pause Café
GenericName=Pause Café
Icon=$a/PauseCafe/icon.png
Exec=$a/PauseCafe/pause_cafe.x86_64
Terminal=false  #ouvrir ou non un terminal lors de l'exécution du programme (false ou true)
StartupNotify=true  #notification de démarrage ou non (false ou true)
Categories=Game" > "$HOME/.local/share/applications/pause_cafe.desktop"
}

if [ -z $1 ]
then
	cont='a'
	echo -e 'Pause Café will be installed in: ~/.local/share/PauseCafe\nQuit and use "./installer.sh path/to/install" to choose custom path'
	while [ ! -z "$cont" ]
	do
		read -p 'Continue ? (Y/n) ' cont
		if [ "$cont" = n ]; then
			echo "Quit..."
			exit
		elif [ "$cont" = y ]; then
			break
		fi
	done

	if [ ! -e ~/.local/share/Pause_Cafe ]; then
		mkdir -m 777 ~/.local/share/Pause_Cafe; fi
	echo -e "\nCopying files..."
	nohup unzip -d ~/.local/share/Pause_Cafe pause_cafe.zip > install.tmp 2>&1 & 
	createDesktop "~/.local/share"
else
	if [ ! -e "$1/PauseCafe" ]; then
		mkdir -m 777 "$1/PauseCafe"; fi
		echo -e "\nCopying files..."
	nohup unzip -d "$1/PauseCafe" pause_cafe.zip > install.tmp 2>&1 &
	createDesktop "$1"
fi

echo -e "\nLaunch the application with 'pause-cafe' command.
Or graphically in your search bar.
See 'pause-cafe -h' for more option\n"