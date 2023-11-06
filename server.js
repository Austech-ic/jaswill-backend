const { exec } = require('child_process');

exec('dotnet CMS_appBackend.dll', (err, stdout, stderr) => {
  if (err) {
    console.error(err);
    return;
  }
  console.log(stdout);
});
