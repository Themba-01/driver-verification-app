document.addEventListener('DOMContentLoaded', () => {
    const video = document.getElementById('camera');
    const canvas = document.getElementById('snapshot');
    const context = canvas.getContext('2d');
    const takeSnapshotButton = document.getElementById('takeSnapshot');
    const verificationForm = document.getElementById('verificationForm');
    const selfieInput = document.getElementById('selfie');

    // Access camera
    navigator.mediaDevices.getUserMedia({ video: true })
        .then(stream => {
            video.srcObject = stream;
        })
        .catch(error => {
            console.error('Error accessing camera:', error);
        });

    // Take a snapshot
    takeSnapshotButton.addEventListener('click', () => {
        context.drawImage(video, 0, 0, canvas.width, canvas.height);
        canvas.style.display = 'block'; // Show the canvas
        const dataURL = canvas.toDataURL('image/jpeg');
        selfieInput.files = dataURLToFile(dataURL); // Set the file input
    });

    // Handle form submission
    verificationForm.addEventListener('submit', async (event) => {
        event.preventDefault();
        const formData = new FormData(verificationForm);

        try {
            const response = await fetch('https://localhost:5001/api/verification', {
                method: 'POST',
                body: formData
            });
            const result = await response.text();
            alert(result);
        } catch (error) {
            console.error('Error submitting form:', error);
        }
    });

    function dataURLToFile(dataURL) {
        const byteString = atob(dataURL.split(',')[1]);
        const mimeString = dataURL.split(',')[0].split(':')[1].split(';')[0];
        const ab = new ArrayBuffer(byteString.length);
        const ia = new Uint8Array(ab);
        for (let i = 0; i < byteString.length; i++) {
            ia[i] = byteString.charCodeAt(i);
        }
        const file = new Blob([ab], { type: mimeString });
        file.name = 'selfie.jpg'; // Set a default file name
        return file;
    }
});
