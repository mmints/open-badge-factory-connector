# Open Badge Factory Connector (C#)
An API connector for [Open Badge Factory](https://openbadgefactory.com) using C# (.NET Core v2). 2016, Marco Brack coded a connector in JavaScript ([Uni GitLab](https://gitlab.uni-koblenz.de/iwm/obf-connector)).
Marco also wrote a generator of certificate/key-pairs ([GitHub](https://github.com/turbopope/obf-certificate-request)).

## Generate a certificate for OBF
### Requirements:
* Linux or _Windows 10 subsystem for Linux_ (WSL)
* libfile-slurp-perl
* libcrypt-openssl-rsa-perl
* OpenSSL

### Installation:

The following code example have been made on Ubuntu 18.04 (WSL). ``Git`` and ``openssl`` are already installed. [Here](https://docs.microsoft.com/en-us/windows/wsl/install-win10) is a guide for installing WSL. 

Install Ruby and dependencies:    
```bash
sudo apt update
sudo apt install ruby libfile-slurp-perl libcrypt-openssl-rsa-perl
```

Clone [Marcos repository](https://github.com/turbopope/obf-certificate-request) where ever you want:
```bash
git clone https://github.com/turbopope/obf-certificate-request.git
```

### Generating key/certificate pair:
1. Got to https://openbadgefactory.com/c/client/my/edit2/apikey and generate a certificate signing request token.
2. Navigate to your local copy of Marcos repository and past the toke into ``config/api-key``
3. Generate certificate by invoking ``./get-certificate``
Your private key will be written to ``obf.key`` and your certificate will be written to ``STDOUT`` so you can pipe it anywhere you want e.g.:

```bash
./get-certificate >> certificate.pem
```
To keep it as simple as possible you should save the key and certificate in the root of the repository.

You can test your certificate with the ping script. Provide it with the path to your certificate as an argument: ``./ping [CERTIFICATE]``

### Create combined certificate (.pfx) for usage in C#/Windows:
The command below demonstrate an example of how to create a .pfx/.p12 file in the command line using OpenSSL. Call this command in the root of Marcos repository.
```bash
openssl pkcs12 -export -out certificate.pfx -inkey obf.key -in certificate.pem
```
You can set a password but it is also possible to leave it empty.
### Copy certificate to Windows main system (if your using WSL)
Navigate to marcos repository and call following command: 
```bash
cp certificate.pfx /mnt/c/<PATH OF YOUR WINDOWS SYSTEM>/
```
**Example:**
```bash
cp certificate.pfx /mnt/c/Users/Mark/IMW/
```
If you navigate via Windows file explorer to this destination, you'll find the certificate there. Now you can install it in the key store by doubleclicking it.