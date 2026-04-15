const API_BASE_URL = "https://localhost:7267/api"; 
const HUB_URL = "https://localhost:7267/jobHub";


const connection = new signalR.HubConnectionBuilder()
    .withUrl(HUB_URL)
    .withAutomaticReconnect()
    .build();


connection.on("ReceiveJobUpdate", (notification) => {
    
    if (notification.newStatus !== "Created" && notification.newStatus !== "New") {
        showNotification(notification);
    }
    //
    const currentFilter = document.getElementById("statusFilter").value;
    loadJobs(currentFilter);
});


async function startSignalR() {
    const statusDiv = document.getElementById("connection-status");
    try {
        await connection.start();
        statusDiv.textContent = "Online";
        statusDiv.className = "status-online";
    } catch (err) {
        statusDiv.textContent = "Offline";
        statusDiv.className = "status-offline";
        setTimeout(startSignalR, 5000); 
    }
}


async function loadJobs(status = "") {
    try {
        let url = `${API_BASE_URL}/jobs`;
        if (status) {
            url += `?status=${status}`; 
        }

        const response = await fetch(url);
        const jobs = await response.json();
        const tableBody = document.getElementById("job-table-body");
        
        tableBody.innerHTML = jobs.map(job => {
            const id = job.jobId;
            const title = job.jobTitle;
            const customer = job.customerName || "N/A";
            const currentStatus = job.jobStatus || "Pending";
            const date = job.scheduledDate ? new Date(job.scheduledDate).toLocaleDateString() : "TBD";
            const statusClass = currentStatus.toLowerCase();

           
            return `
                <tr class="clickable-row" onclick="window.location.href='job-details.html?id=${id}'">
                    <td><strong>#${id}</strong></td>
                    <td>${customer}</td>
                    <td>${title}</td>
                    <td><span style="color:var(--text-muted)">${date}</span></td>
                    <td><span class="status-badge ${statusClass}">${currentStatus}</span></td>
                </tr>
            `;
        }).join('');
    } catch (err) {
        console.error("Failed to load jobs", err);
    }
}


document.getElementById("statusFilter").addEventListener("change", (e) => {
    loadJobs(e.target.value);
});


function showNotification(data) {
    const container = document.getElementById("notification-container");
    const toast = document.createElement("div");
    toast.className = "toast";
    

    const timeString = data.timeStamp ? new Date(data.timeStamp).toLocaleString() : new Date().toLocaleString();
    

    const shortDesc = data.jobDescription && data.jobDescription.length > 50 
        ? data.jobDescription.substring(0, 50) + "..." 
        : (data.jobDescription || "No description available");

    toast.innerHTML = `
        <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 8px;">
            <span style="font-weight:700; color:var(--primary); font-size: 0.9rem;">🔔 Job Updated</span>
            <small style="color:var(--text-muted); font-size: 0.7rem;">${timeString}</small>
        </div>
        <div style="font-size:0.85rem; line-height: 1.5;">
            <div style="margin-bottom: 4px;">
                <strong>Job ID:</strong> #${data.jobId} <br>
                <strong>Customer:</strong> ${data.customerName}
            </div>
            <div style="margin-bottom: 4px;">
                <strong>Status:</strong> 
                <span class="status-badge ${data.newStatus.toLowerCase()}" style="padding: 2px 8px; font-size: 0.75rem;">
                    ${data.newStatus}
                </span>
            </div>
            <div style="color: var(--text-muted); font-style: italic; border-top: 1px solid #eee; pt-5px; margin-top: 5px;">
                "${shortDesc}"
            </div>
        </div>
    `;

    container.appendChild(toast);

  
    setTimeout(() => {
        toast.style.opacity = "0";
        toast.style.transform = "translateX(20px)";
        toast.style.transition = "all 0.5s ease";
        setTimeout(() => toast.remove(), 500);
    }, 6000); 
}


const modal = document.getElementById("job-modal");
document.getElementById("open-modal-btn").onclick = () => modal.style.display = "flex";
document.getElementById("close-modal-btn").onclick = () => modal.style.display = "none";

document.getElementById("add-job-form").onsubmit = async (e) => {
    e.preventDefault();

    const newJob = {
        jobTitle: document.getElementById("jobTitle").value,
        customerId: parseInt(document.getElementById("customerId").value),
        jobDescription: document.getElementById("jobDescription").value,
        scheduledDate: new Date().toISOString()
    };

    try {
        const response = await fetch(`${API_BASE_URL}/jobs`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newJob)
        });

        if (response.ok) {
            modal.style.display = "none";
            document.getElementById("add-job-form").reset();
          
            loadJobs(document.getElementById("statusFilter").value);
        } else {
            const errorData = await response.json();
            console.error("Server Error:", errorData);
            alert("Error adding job. Check console.");
        }
    } catch (err) {
        console.error("Fetch Error:", err);
    }
};


startSignalR();
loadJobs();