- [Create Rpi image](#create-rpi-image)
- [Rpi setup data](#rpi-setup-data)
- [Check OS](#check-os)
- [Disable Bluetooth (optional)](#disable-bluetooth-optional)
- [Setup static IP](#setup-static-ip)
- [Configure ssh for use with Teamcity](#configure-ssh-for-use-with-teamcity)
- [Install latest dotnet](#install-latest-dotnet)
- [Update OS,  Install visual studio  code](#update-os--install-visual-studio--code)
- [Configure /var/www for deployments](#configure-varwww-for-deployments)


#  Create Rpi image

- Use Raspberry pi imager
- Select 64 bit (arm64) Raspberry Pi OS (64 bit) (see images below)
- Select wifi and ssh 
- set user: frits
- set wifi router to home router with passphrase
- set pwd : rasp##  where  ## is  pi number 
- set locale: US and  UTF-8 
- set time-zone: EU and  Amsterdam
- document  SSID and set IP adress for SSID in router

![raspberry Pi imager](./rpi-imager.png "splash screen")

![OS selection](./os-selection.png)

# Rpi setup data

| number| type| SSID| User| Pwd | installed | .NET SDK |
|--:|--|--|--|--|--|--|
| 01| 3B| RPI01| frits| rasp01 | 03-04-32023 | 7.0.202 |
| 02| 3B| RPI02| frits| rasp02 | 03-04-32023 | 7.0.202 |
| 03| 3B| RPI03| frits| rasp03 | 03-04-32023 | 7.0.202 |
| 04| 3B| RPI04| frits| rasp04 | 03-04-32023 | 7.0.202 |

---
# Check OS

Open a terminal and check if result from next  command conatains `aarch64`.
```bash
uname -a
```

# Disable Bluetooth (optional)
Run the following commands and reboot to see result in taskbar.

```bash
# open config 
sudo nano /boot/config.txt

# following lines  to end of file, save and exit
# disable bluetooth
dtoverlay=disable-bt
```

# Setup static IP 
 Following table list desired static IP addresses.

| number|  SSID| IP|
|--:|--|--|
| 01|  RPI01| 192.168.2.45| 
| 02|  RPI02| 192.168.2.47| 
| 03|  RPI03| 192.168.2.49| 
| 04|  RPI04| 192.168.2.51| 

 Run the following commands to set up static IP fo RPI:

```bash
# Retrieve current defined router
ip r | grep default
```

Make a note of the first IP mentioned in this string, is router current IP adres

```bash
# Retrieve IP of current DNS server
sudo nano /etc/resolv.conf
```

Make a note of the IP next to “nameserver“. This defines the name server in our next few steps.

```bash
# Modify the “dhcpcd.conf” configuration file
sudo nano /etc/dhcpcd.conf
```
- add to bottom and save

``` 
interface <NETWORK>
static ip_address=<STATICIP>/24
static routers=<ROUTERIP>
static domain_name_servers=<DNSIP>
```
where:
| template| value|
| -- | -- |
 | `<NETWORK>`  | `eth0` or `wlan0` |
 | `<STATICIP>` | desired IP adress of rpi |
 | `<ROUTERIP>` | earlier retrieved router IP |
 | `<DNSIP>`    | earlier retreived DNS nameserver IP |

- and finally

```bash
# restart the pi
sudo reboot
# check if RPI IP is set 
hostname -I
```

# Configure ssh for use with Teamcity

- first upload the SSH private key to right project in Teamcity

```powershell
http://localhost:9090/admin/editProject.html?projectId=Bluetrain&tab=ssh-manager
```

- login to RPI using SSH
- create folder `mkdir home/.ssh`
- on windows powershell prompt
- navigate to .ssh folder:  C:Users\Frits Ankersmit\\.ssh  
  
```
type .\bluetrain-key-rsa.pub | ssh {USER}@{IP-ADRESS} "cat >> .ssh/authorized_keys"
```

Be sure to replace {USER } and {IP_ADRESS} with the actual values. Answer Yes when prompted.
The teamcity deploy build now uses SSH and key to automatically deploy builds.    

# Install latest dotnet

- Go to  `https://dotnet.microsoft.com/en-us/download/dotnet/6.0`
- Select linux arm64  download
- Navigate to  `$HOME/Downloads`
- Run following commands

```bash
mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-6.0.100-rc.1.21458.32-linux-arm.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet
```

- make permanent through:

```bash 
# Run nano editor to edit the .bashrc
nano ~/.bashrc
 
# Copy the commands below to the .bashrc
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet
 
# Run this command so the terminal session will use the new settings
source ~/.bashrc
```
- check by typing

```bash
dotnet --info
```

# Update OS,  Install visual studio  code 

```bash
sudo apt update
sudo apt autoremove
sudo apt install code
```

# Configure /var/www for deployments

To give  Teamcity Deploy the right permissions tpo deploy to  /var/www/bluetrain run the following commands  

```bash
# crate  the deploymnent dirs
sudo mkdir /var/www
sudo mkdir /var/www/bluetrain

# replace some user with the default pi user
sudo usermod -a -G www-data <some_user>

# set the correct permissions on /var/www.
sudo chgrp -R www-data /var/www
sudo chmod -R g+w /var/www

# make sure that all new files and directories created under /var/www are owned by the www-data group.
sudo find /var/www -type d -exec chmod 2775 {} \;  

# Find all files in /var/www and add read and write permission for owner and group:
sudo find /var/www -type f -exec chmod ug+rw {} \;

# You might have to log out and log back in to be able to make changes if you're editing permission for your own account
```




